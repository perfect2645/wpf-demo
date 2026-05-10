using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.viewmodels.diagrams.abstractions;
using Messaging.LocalMessages.channel;
using Microsoft.Extensions.DependencyInjection;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.diagrams.electric.primary
{
    public class TiantandongVm : PrimaryDiagram
    {
        public TiantandongVm(IHttpCommandDataService httpDataService,
            [FromKeyedServices(CommonConsts.Ioc_CircuitChannel)] IDataChannelService dataChannelService) : base(httpDataService, dataChannelService)
        {
            ViewId = Consts.TTD_S04;
        }
    }
}
