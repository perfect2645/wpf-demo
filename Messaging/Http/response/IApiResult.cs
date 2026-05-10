namespace Messaging.Http.response
{
    public interface IApiResult<TPayload>
    {
        int Code { get; set; }
        string Message { get; set; }
        TPayload Data { get; set; }
    }
}
