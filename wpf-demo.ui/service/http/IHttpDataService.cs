using Messaging.Http.content;

namespace wpf.ui.service.http
{
    public interface IHttpDataService
    {
        Task<string?> GetDataAsync(IHttpApiContent httpApiContent);
        Task<T?> GetDataAsync<T>(IHttpApiContent httpApiContent) where T : class;
        Task<string?> PostDataAsync(IHttpApiContent httpApiContent);
        Task<T?> PostDataAsync<T>(IHttpApiContent httpApiContent) where T : class;
    }
}
