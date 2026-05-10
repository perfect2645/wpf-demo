using Messaging.Http.content;

namespace wpf.ui.service.http
{
    public interface IHttpCommandDataService
    {
        Task<T?> PostCommandDataAsync<T>(IHttpApiContent httpApiContent) where T : class;
        Task<T?> GetDataAsync<T>(IHttpApiContent httpApiContent) where T : class;
    }
}
