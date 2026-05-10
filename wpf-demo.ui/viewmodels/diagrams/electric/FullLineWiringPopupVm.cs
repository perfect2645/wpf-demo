using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public class FullLineWiringPopupVm : FullLineWiringVm
    {
        public FullLineWiringPopupVm(IHttpCommandDataService httpDataService, 
            MqService mqService,
            [FromKeyedServices(Consts.Ioc_Diagram)] IPopupService popupService) : base(httpDataService, mqService, popupService)
        {
            IsTrackNotificationVisible = false;
            IsTrackNotificationEnabled = false;
        }
    }
}
