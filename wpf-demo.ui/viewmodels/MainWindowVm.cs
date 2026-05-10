using wpf.ui.constants;
using wpf.ui.model.payloads;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using Logging;
using Utils.Tasking;
using WpfUtils.Cache;

namespace wpf.ui.viewmodels
{
    public class MainWindowVm
    {
        private readonly IHttpDataService _httpDataService;
        public MainWindowVm(IHttpDataService httpDataService, MqService mqService)
        {
            _httpDataService = httpDataService;

            LoginAsync().SafeFireAndForget(OnLoginSuccess, OnLoginError);
        }

        private async Task<LoginData?> LoginAsync()
        {
            var stringContent = new HttpDataContent("system/auth/login");
            stringContent.AddContent("username", "admin");
            stringContent.AddContent("password", "admin123");
            stringContent.AddContent("rememberMe", "true");
            return await _httpDataService.PostDataAsync<LoginData>(stringContent);
        }

        private void OnLoginSuccess(LoginData? response)
        {
            if (response == null)
            {
                return;
            }

            AppPropertyHelper.AddProperty(Consts.App_LoginData, response);
        }

        private void OnLoginError(Exception ex)
        {
            Log4Logger.Logger.Error("Login Error", ex);
        }
    }
}
