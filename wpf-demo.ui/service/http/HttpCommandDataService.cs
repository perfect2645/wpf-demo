using Messaging.Http.content;
using Messaging.Http.response;

namespace wpf.ui.service.http
{
    public class HttpCommandDataService : IHttpCommandDataService
    {
        private IHttpDataService _httpDataService;
        public HttpCommandDataService(IHttpDataService httpDataService) 
        {
            _httpDataService = httpDataService;
        }

        public async Task<T?> PostCommandDataAsync<T>(IHttpApiContent httpApiContent) where T : class
        {
            var response = await _httpDataService.PostDataAsync<ApiCommandResult<T>>(httpApiContent);
            return response?.Data?.Data;
        }

        public async Task<T?> GetDataAsync<T>(IHttpApiContent httpApiContent) where T : class
        {
            var response = await _httpDataService.GetDataAsync<ApiCommandResult<T>>(httpApiContent);
            return response?.Data?.Data;
        }
    }
}
