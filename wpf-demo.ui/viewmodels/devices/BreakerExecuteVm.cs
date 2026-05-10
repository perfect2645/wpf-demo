using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.viewmodels.devices.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logging;
using Messaging.Http.Exceptions;
using System.Text.Json;
using Utils;
using Utils.Enumerable;
using Utils.Object;

namespace wpf.ui.viewmodels.devices
{
    public partial class BreakerExecuteVm : DeviceBase
    {

        private IHttpDataService _httpDataService;
        [ObservableProperty]
        private string? _stationName;
        [ObservableProperty]
        private string? _actionName;
        [ObservableProperty]
        private string? _stationId;
        [ObservableProperty]
        private string? _message;
        [ObservableProperty]
        private string? _targetStatus;

        public BreakerExecuteVm(IHttpDataService httpDataService)
        {
            _httpDataService = httpDataService;
        }

        private void UpdateButtonContent()
        {
            ActionName = "执行";
        }

        private bool CanBreakerActionExecute()
        {
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanBreakerActionExecute))]
        private async Task OnBreakerAction()
        {
            try
            {
                var isSuccess = await SendBreakerAction();
                await HandleBreakerActionResult(isSuccess);
            }
            catch (HttpException ex)
            {
                Log4Logger.Logger.Error($"Send breaker update failed.HttpException:{ex.ErrCode}", ex);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Send breaker update failed.", ex);
            }
            finally
            {
                UpdateButtonContent();
            }
        }

        private async Task HandleBreakerActionResult(bool isSuccess)
        {
            var actionName = ActionName;
            if (isSuccess)
            {
                for (int i = 3; i > 0; i--)
                {
                    ActionName = $"{actionName}成功({i})";
                    await Task.Delay(1000);
                }
                ClosePopupAction?.Invoke();
            }
            else
            {
                ActionName = $"{actionName}失败";
                await Task.Delay(3000);
            }
        }

        private async Task<bool> SendBreakerAction()
        {
            if (string.IsNullOrEmpty(StationId) || string.IsNullOrEmpty(DeviceId) || string.IsNullOrEmpty(Status)) 
            {
                return false;
            }
            var isPowered = Status == Consts.DeviceState_CS ? true : false;
            var content = new HttpDeviceContent(StationId, DeviceId, isPowered);
            var response = await _httpDataService.PostDataAsync(content);

            if (response == null)
            {
                return false;
            }

            var json = JsonDocument.Parse(response).RootElement;
            var responseDic = JsonAnalysis.JsonToDic(json);
            var code = responseDic.Get("code").NotNullString();
            var msg = responseDic.Get("msg").NotNullString();
            if (code == "0")
            {
                Message = string.Empty;
                Log4Logger.Logger.Info($"{DeviceId} Status [{isPowered}]->[{!isPowered}],{msg}");
                return true;
            }

            Message = msg;
            Log4Logger.Logger.Error(msg);
            return false;
        }
    }
}
