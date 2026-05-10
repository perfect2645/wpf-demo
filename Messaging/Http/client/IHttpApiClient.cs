
using Messaging.Http.content;

namespace Messaging.Http
{
    public interface IHttpApiClient
    {
        Task<string> GetStringAsync(IHttpApiContent content,
            CancellationToken? cancellationToken = null);
        Task<TResponse> GetAsync<TResponse>(IHttpApiContent content,
            CancellationToken? cancellationToken = null) where TResponse : class;

        Task<string?> PostAsync(IHttpApiContent content,
                CancellationToken? cancellationToken = null);
        Task<TResponse> PostAsync<TResponse>(IHttpApiContent content,
            CancellationToken? cancellationToken = null) where TResponse : class;
    }
}