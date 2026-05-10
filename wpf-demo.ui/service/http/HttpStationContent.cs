using Messaging.Http.Exceptions;

namespace wpf.ui.service.http
{
    public class HttpStationContent : HttpDataContent
    {
        //http://localhost:38080/admin-api/psc/run/query/station-asset?stationCode=S04
        private readonly string _stationId;
        private const string _url = "psc/run/query/station-asset";
        public HttpStationContent(string stationId) : base(_url)
        {
            _stationId = stationId;
            BuildUrl();
        }

        private void BuildUrl()
        {
            if (_stationId == null)
            {
                throw new HttpException($"Build station content failed, stationId is empty.", HttpStatus.RequestContentArgument);
            }

            RequestUrl = $"{RequestUrl}?stationCode={_stationId}";
        }
    }
}
