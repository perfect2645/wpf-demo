using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.viewmodels.diagrams.abstractions;
using Messaging.LocalMessages.channel;
using Microsoft.Extensions.DependencyInjection;
using WpfUtils.Consts;

namespace wpf.ui.viewmodels.diagrams.electric.primary
{
    public class HuixinnanVm : PrimaryDiagram
    {
        public HuixinnanVm(IHttpCommandDataService httpDataService,
            [FromKeyedServices(CommonConsts.Ioc_CircuitChannel)] IDataChannelService dataChannelService) : base(httpDataService, dataChannelService)
        {
            ViewId = Consts.HXN_S16;
        }
    }
}
