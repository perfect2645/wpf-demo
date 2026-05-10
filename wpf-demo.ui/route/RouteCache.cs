using wpf.ui.constants;
using wpf.ui.service.http;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.viewmodels.diagrams.broadcast;
using wpf.ui.viewmodels.diagrams.electric;
using wpf.ui.viewmodels.diagrams.electromechanical;
using wpf.ui.viewmodels.diagrams.envControl;
using wpf.ui.viewmodels.diagrams.envControl.authorization;
using wpf.ui.viewmodels.diagrams.envControl.mode;
using wpf.ui.viewmodels.diagrams.envControl.schedule;
using wpf.ui.viewmodels.diagrams.envControl.sensor;
using wpf.ui.viewmodels.diagrams.fireAlarm;
using wpf.ui.viewmodels.diagrams.safetyDoor;
using wpf.ui.viewmodels.diagrams.ticketSales;
using wpf.ui.viewmodels.panels.menubar;
using wpf.ui.viewmodels.panels.station;
using Ioc;
using System.Collections.ObjectModel;
using UserComponent.Core.menu;
using UserComponent.Core.station;

namespace wpf.ui.route
{
    public static class RouteCache
    {
        public static IList<MenuButtonBaseVm>? MenuList { get; private set; }
        public static IList<StationBaseVm>? StationList {  get; private set; }

        public static readonly Dictionary<string, DiagramBase> DiagramMapping = new();
        public static readonly Dictionary<string, PrimaryDiagram> PrimaryDiagramMapping = new();

        public static readonly Dictionary<string, string> DiagramImgMapping = new Dictionary<string, string>
        {
            {"轨电位复归", ""},
        };

        #region Init

        static RouteCache()
        {
            InitMenu();
            InitStations();
            InitDiagramMapping();
        }

        private static void InitDiagramMapping()
        {
            DiagramMapping.Add("电力-遥测量显示", new RemoteMeasurementVm());
            DiagramMapping.Add(Consts.Electric_PrimarySystem, AppContainer.GetKeyedService<DiagramBase>(Consts.Electric_PrimarySystem)!);
            DiagramMapping.Add("电力-全线接线图", new FullLineVm { PopupSettings = new() });
            DiagramMapping.Add("电力-全线10kV接线图", new FullLine10kvVm { PopupSettings = new() });

            DiagramMapping.Add(Consts.Electric_FullWiring, AppContainer.GetKeyedService<DiagramBase>(Consts.Electric_FullWiring)!);
            DiagramMapping.Add(Consts.Electric_750On, AppContainer.GetKeyedService<DiagramBase>(Consts.Electric_750On)!);
            DiagramMapping.Add(Consts.Electric_750Off, AppContainer.GetKeyedService<DiagramBase>(Consts.Electric_750Off)!);
            DiagramMapping.Add("电力-工况", new OperatingConditionVm());
            DiagramMapping.Add(Consts.Electric_PowerOn, (DiagramBase)new PowerOnOffVm(Consts.Electric_PowerOn));
            DiagramMapping.Add(Consts.Electric_PowerOff, (DiagramBase)new PowerOnOffVm(Consts.Electric_PowerOff));
            DiagramMapping.Add("电力-软压板", new SoftPlateVm());
            DiagramMapping.Add("电力-定值组", new SettingGroupVm());
            DiagramMapping.Add("电力-轨电位复归", new RailPotentialResetVm());
            DiagramMapping.Add("电力-闭锁", new LockoutVm());

            DiagramMapping.Add("环控-水系统", new WaterSystemVm());
            DiagramMapping.Add(Consts.Environment_BigSys, new BigSystemVm());
            DiagramMapping.Add("环控-小系统", new SmallSystemVm());
            DiagramMapping.Add("环控-隧道通风", new TunnelVentilationVm());
            DiagramMapping.Add("时间表-查看", new ScheduleVm());
            DiagramMapping.Add("时间表-车站运行", new TimeTableVm());
            DiagramMapping.Add("环控-传感器", new SensorVm());
            DiagramMapping.Add("环控-授权", new AuthorizationVm());
            DiagramMapping.Add("环控-模式", new FireVm());
            DiagramMapping.Add("模式-大/水", new BigVm());
            DiagramMapping.Add("模式-小系统", new SmallVm());
            DiagramMapping.Add("模式-电力", new ElectricVm());
            DiagramMapping.Add("模式-区间", new IntervalVm());
            DiagramMapping.Add("模式-车站阻塞", new StationBlockVm());
            DiagramMapping.Add("模式-南端照明", new SouthLightingVm());
            DiagramMapping.Add("模式-北端照明", new NorthLightingVm());

            DiagramMapping.Add("车站机电-照明", new LightingVm());
            DiagramMapping.Add("车站机电-电/扶梯", new EscalatorVm());
            DiagramMapping.Add("车站机电-给排水", new DrainageVm());

            DiagramMapping.Add("火灾报警-站台报警", new PlatformVm());
            DiagramMapping.Add("火灾报警-站厅报警", new HallVm());

            DiagramMapping.Add("广播-车站广播", new StationBroadcastVm());
            DiagramMapping.Add("广播-广播内容", new BroadcastContentVm());

            DiagramMapping.Add("安全门-安全门", new SafetyDoorVm());

            DiagramMapping.Add("售检票-自动收费系统", new AFCVm());
        }

        public static async ValueTask InitAsync() 
        {
            await ValueTask.CompletedTask;
        }

        #endregion Init

        #region Menu
        private static void InitMenu()
        {
            MenuList = new List<MenuButtonBaseVm>
            {
                new MenuButtonViewMode("1", "环控")
                {   
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("环控-水系统", "水系统") { ParentId= "1" },
                        new MenuButtonViewMode(Consts.Environment_BigSys, "大系统") { ParentId= "1" },
                        new MenuButtonViewMode("环控-小系统", "小系统") { ParentId= "1" },
                        new MenuButtonViewMode("环控-时间表", "时间表") 
                        { 
                            ParentId= "1",
                            SubItems = new ObservableCollection<MenuButtonBaseVm>
                            {
                                new MenuButtonViewMode("时间表-查看", "查看") { ParentId= "环控-时间表", IsVisible = false, Page = 2 },
                                new MenuButtonViewMode("时间表-车站运行", "车站运行") { ParentId= "环控-时间表", IsVisible = false, Page = 2 },
                                new MenuButtonViewMode("时间表-返回环控", "返回环控\n<<--菜单") { ParentId= "环控-时间表", IsVisible = false, Page = 2 }
                            }
                        },
                        new MenuButtonViewMode("环控-模式", "模式") 
                        { 
                            ParentId= "1",
                            SubItems = new ObservableCollection<MenuButtonBaseVm>
                            {
                                new MenuButtonViewMode("模式-大/水", "大/水") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-小系统", "小系统") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-电力", "电力") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-区间", "区间") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-车站阻塞", "车站阻塞") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-南端照明", "南端照明") { ParentId= "环控-模式" },
                                new MenuButtonViewMode("模式-北端照明", "北端照明") { ParentId= "环控-模式" },
                            }
                        },
                        new MenuButtonViewMode("环控-传感器", "传感器") { ParentId= "1" },
                        new MenuButtonViewMode("环控-隧道通风", "隧道通风") { ParentId= "1" },
                        new MenuButtonViewMode("环控-授权", "环控-授权") { ParentId= "1" },
                    }
                },
                new MenuButtonViewMode("2", "车站机电")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("车站机电-照明", "照明") { ParentId= "2" },
                        new MenuButtonViewMode("车站机电-电/扶梯", "电/扶梯") { ParentId= "2" },
                        new MenuButtonViewMode("车站机电-给排水", "给排水") { ParentId= "2" },
                    }
                },
                new MenuButtonViewMode("3", "火灾报警")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("火灾报警-站台报警", "站台\n报警") { ParentId= "3" },
                        new MenuButtonViewMode("火灾报警-站厅报警", "站厅\n报警") { ParentId= "3" },
                    }
                },
                new MenuButtonViewMode("4", "信号"),
                new MenuButtonViewMode("5", "电力")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("电力-全线接线图", "全线\n接线图") { ParentId= "5" },
                        new MenuButtonViewMode("电力-全线10kV接线图", "全线10kV\n接线图") { ParentId= "5" },
                        new MenuButtonViewMode(Consts.Electric_FullWiring, "全线\n接触网图") { ParentId= "5" },
                        new MenuButtonViewMode(Consts.Electric_750On, "750V送电") { ParentId= "5" },
                        new MenuButtonViewMode(Consts.Electric_750Off, "750V停电") { ParentId= "5" },
                        new MenuButtonViewMode("电力-轨电位复归", "轨电位复归\n") { ParentId= "5" },
                        new MenuButtonViewMode(Consts.Electric_BtnId, "车站电力\n-->>") { ParentId= "5" },
                        new MenuButtonViewMode(Consts.Electric_PrimarySystem, "车站一次\n系统图") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-工况", "工况") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-遥测量显示", "遥测量\n显示") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-软压板", "软压板") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-定值组", "定值组") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-送电", "送电") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-停电", "停电") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode("电力-闭锁", "闭锁") { ParentId= "5", Page = 2, IsVisible = false },
                        new MenuButtonViewMode(Consts.BackToFullLineBtnId, "返回全线\n<<--") { ParentId= "5", Page = 2, IsVisible = false },
                    }
                },
                new MenuButtonViewMode("6", "乘客信息"),
                new MenuButtonViewMode("7", "闭路电视"),
                new MenuButtonViewMode("8", "广播")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("广播-车站广播", "车站广播") { ParentId= "8" },
                        new MenuButtonViewMode("广播-广播内容", "广播内容") { ParentId= "8" },
                    }
                },
                new MenuButtonViewMode("9", "安全门")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("安全门-安全门", "安全门") { ParentId= "9" },
                    }
                },
                new MenuButtonViewMode("10", "售检票")
                {
                    SubItems = new ObservableCollection<MenuButtonBaseVm>
                    {
                        new MenuButtonViewMode("售检票-自动收费系统", "自动\n收费系统") { ParentId= "10" },
                    }
                },
            };
        }

        public static MenuButtonBaseVm? FindMenuById(IEnumerable<MenuButtonBaseVm>? menuList, string targetId)
        {
            if (menuList == null)
                return null;

            if (string.IsNullOrEmpty(targetId))
            {
                return null;
            }

            foreach (var menu in menuList)
            {
                if (menu.Id == targetId)
                    return menu;

                if (menu.SubItems != null)
                {
                    var foundInSubItems = FindMenuById(menu.SubItems, targetId);
                    if (foundInSubItems != null)
                        return foundInSubItems;
                }
            }
            return null;
        }

        #endregion Menu

        #region Station

        private static void InitStations()
        {
            StationList = new List<StationBaseVm>()
            {
                new StationViewModel(Consts.SJZ_S01, "宋家庄"),
                new StationViewModel("S02", "刘家窑"),
                new StationViewModel(Consts.PHY_S03, "蒲黄榆"),
                new StationViewModel(Consts.TTD_S04, "天坛东", "天坛东门"),
                new StationViewModel(Consts.CQK_S06, "磁器口"),
                new StationViewModel("崇文门", "崇文门"),
                new StationViewModel(Consts.DD_S08, "东单"),
                new StationViewModel(Consts.DSK_S09, "灯市口"),
                new StationViewModel(Consts.DS_S10, "东四"),
                new StationViewModel("张自忠", "张自忠"),
                new StationViewModel("北新桥", "北新桥"),
                new StationViewModel(Consts.YHG_S13, "雍和宫"),
                new StationViewModel(Consts.HPB_S14, "和平北"),
                new StationViewModel(Consts.HPX_S15, "和平西"),
                new StationViewModel(Consts.HXN_S16, "惠新南"),
                new StationViewModel(Consts.HXB_S17, "惠新北"),
                new StationViewModel(Consts.DTD_S18, "大屯东"),
                new StationViewModel(Consts.BYB_S19, "北苑北"),
                new StationViewModel(Consts.LSN_S21, "立水南"),
                new StationViewModel(Consts.LSQ_S22, "立水桥"),
                new StationViewModel(Consts.TTN_S24, "天通南"),
                new StationViewModel(Consts.TTY, "天通苑"),
                new StationViewModel(Consts.TTB_S25, "天通北") { ShowLine = false },
                new StationViewModel("车辆段", "车辆段") { ShowLine = false },
                new StationViewModel(Consts.ParkingLot, Consts.ParkingLot) { ShowLine = false },
            };
        }

        #endregion Station
    }
}
