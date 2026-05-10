using wpf.ui.constants;
using wpf.ui.model.payloads;
using Messaging.Http.content;
using WpfUtils.Cache;

namespace wpf.ui.service.http
{
    public class HttpDataContent : HttpStringContent
    {
        public HttpDataContent(string url) : base(url)
        {
            SetDefault();
        }

        private void SetDefault()
        {
            var auth = AppPropertyHelper.TryGetProperty<LoginData>(Consts.App_LoginData, out var loginData);
            if (auth && !string.IsNullOrEmpty(loginData?.AccessToken))
            {
                AddHeader("Authorization", loginData.AccessToken);
            }
        }
    }
}
