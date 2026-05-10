using wpf.ui.model.diagrams.electric;
using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using Logging;
using UserComponent.Core.station;
using Utils;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public class RailPotentialResetVm : DiagramBase
    {
        #region Properties

        public List<RailPotentialItem>? TopGridItems { get; set; }

        public List<RailPotentialItem>? BottomGridItems { get; set; }

        #endregion Properties

        public RailPotentialResetVm()
        {
            InitRailPotentialLists();
        }

        private void InitRailPotentialLists()
        {
            TopGridItems = new List<RailPotentialItem>
            {
                new ("停车场", RailPotentialState.Open),
                new ("宋家庄", RailPotentialState.Open),
                new ("刘家窑", RailPotentialState.Open),
                new ("蒲黄榆", RailPotentialState.Close),
                new ("天坛东", RailPotentialState.Open),
                new ("磁器口", RailPotentialState.Open),
                new ("崇文门", RailPotentialState.Open),
                new ("东单", RailPotentialState.Open),
                new ("灯市口", RailPotentialState.Open),
                new ("东四", RailPotentialState.Close),
                new ("张自忠", RailPotentialState.Open),
                new ("北新桥", RailPotentialState.Fault),
            };

            BottomGridItems = new List<RailPotentialItem>
            {
                new ("雍和宫", RailPotentialState.Open),
                new ("和平北", RailPotentialState.Open),
                new ("和平西", RailPotentialState.Open),
                new ("惠新南", RailPotentialState.Open),
                new ("惠新北", RailPotentialState.Open),
                new ("大屯东", RailPotentialState.Open),
                new ("北苑北", RailPotentialState.Open),
                new ("立水南", RailPotentialState.Open),
                new ("立水桥", RailPotentialState.Close),
                new ("天通南", RailPotentialState.Open),
                new ("天通苑", RailPotentialState.Close),
                new ("天通北", RailPotentialState.Open),
                new ("车辆段", RailPotentialState.Open),
            };
        }
    }
}
