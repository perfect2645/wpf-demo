using Messaging.Http.Exceptions;

namespace wpf.ui.service.http
{
    public class HttpDeviceContent : HttpDataContent
    {
        //POST http://localhost:38080/admin-api/psc/run/update/device-state
        private const string _url = "psc/run/update/device-state";

        private string _stationCode;
        private string _deviceCode;
        private bool _isPowered;


        public HttpDeviceContent(string stationCode, string deviceCode, bool isPowered) : base(_url)
        {
            _stationCode = stationCode;
            _deviceCode = deviceCode;
            _isPowered = isPowered;
            BuildContents();
        }

        private void BuildContents()
        {
            AddContent("stationCode", _stationCode);
            AddContent("deviceCode", _deviceCode);

            var oldState = _isPowered ? "CS" : "OP";
            var newState = _isPowered ? "OP" : "CS";
            AddContent("oldState", oldState);
            AddContent("newState", newState);
        }
    }
}
