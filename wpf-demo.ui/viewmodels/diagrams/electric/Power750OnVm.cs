using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class Power750OnVm : Power750BaseVm
    {
        public Power750OnVm([FromKeyedServices(Consts.Ioc_CustomPopup)] ICustomPopupService popupService,
            IHttpCommandDataService httpDataService,
            MqService mqService) : base(popupService, httpDataService, mqService)
        {
            IsPowerOnView = true;
        }
    }
}
