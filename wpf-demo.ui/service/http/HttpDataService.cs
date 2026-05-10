using Logging;
using Messaging.Http;
using Messaging.Http.content;
using Messaging.Http.response;

namespace wpf.ui.service.http
{
    public class HttpDataService : IHttpDataService
    {
        private IHttpApiClient _httpApiClient;
        public HttpDataService(IHttpApiClient httpApiClient)
        {
            _httpApiClient = httpApiClient;
        }

        public async Task<string?> GetDataAsync(IHttpApiContent httpApiContent)
        {
            var response = await _httpApiClient.GetStringAsync(httpApiContent);
            return response;
        }

        public async Task<T?> GetDataAsync<T>(IHttpApiContent httpApiContent) where T : class
        {
            var response = await _httpApiClient.GetAsync<T>(httpApiContent);
            return response;
        }

        public async Task<string?> PostDataAsync(IHttpApiContent httpApiContent)
        {
            var response = await _httpApiClient.PostAsync(httpApiContent);
            return response;
        }

        public async Task<T?> PostDataAsync<T>(IHttpApiContent httpApiContent) where T : class
        {
            var response = await _httpApiClient.PostAsync<ApiResult<T>>(httpApiContent);
            if (response == null)
            {
                return null;
            }

            if (response.Code != 0)
            {
                Log4Logger.Logger.Error($"Http post failed.url:[{httpApiContent.RequestUrl}], code=[{response.Code}],[{response.Message}]");
                return null;
            }

            if (response.Data == null)
            {
                Log4Logger.Logger.Warn($"Http post returns empty data:[{httpApiContent.RequestUrl}], [{response.Message}]");
                return null;
            }

            return response.Data;
        }


    }
}
