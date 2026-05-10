using wpf.ui.constants;
using wpf.ui.route;
using wpf.ui.service;
using wpf.ui.service.http;
using wpf.ui.service.mq;
using wpf.ui.viewmodels;
using wpf.ui.viewmodels.devices;
using wpf.ui.viewmodels.diagrams.abstractions;
using wpf.ui.viewmodels.diagrams.electric;
using wpf.ui.viewmodels.diagrams.electric.primary;
using wpf.ui.viewmodels.panels;
using wpf.ui.viewmodels.panels.station;
using Ioc;
using Logging;
using MaterialDesignThemes.Wpf;
using Messaging.Http.helper;
using Messaging.LocalMessages.channel;
using Messaging.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using PowerComponent.Core.Popup;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;
using UserComponent.Core.route;
using UserComponent.Core.station;
using Utils.Configurations;
using Utils.Tasking;
using WpfUtils.Cache;
using WpfUtils.Consts;

namespace wpf.ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += UnHandledExceptionHandler;
            ConfigServices();
            RouteCache.InitAsync().SafeFireAndForget(OnError);
            ImageCache.InitAsync().SafeFireAndForget(OnError);
        }

        private void OnError(Exception exception)
        {
            Log4Logger.Logger.Error("An error occurred during initialization.", exception);
        }

        private void ConfigServices()
        {
            SetupAppConfig();
            ConfigViews();
            ConfigMessaging();
            AppContainer.Build();
        }

        private void SetupAppConfig()
        {
            AppConfig.Init();

            AppContainer.ServiceCollection.Configure<MqSettings>(AppConfig.Configuration!.GetSection("ApiSettings:MQ:ConnectionFactory"));
        }

        private void ConfigMessaging()
        {
            AppContainer.ServiceCollection.AddHttpApiClient("HttpApiClient", options =>
            {
                var baseUrl = AppConfig.Configuration?["ApiSettings:BaseUrl"];
                if (baseUrl == null) 
                {
                    throw new ArgumentNullException("ApiSettings:BaseUrl is null.");
                }
                options.BaseAddress = new Uri(baseUrl);
                options.EnableRetry = true;
                options.MaxRetryCount = 3;
                options.RetryDelay = TimeSpan.FromSeconds(2);
            });
            AppContainer.ServiceCollection.AddTransient<IHttpDataService, HttpDataService>();
            AppContainer.ServiceCollection.AddTransient<IHttpCommandDataService, HttpCommandDataService>();
            AppContainer.ServiceCollection.AddKeyedSingleton<IDataChannelService, CircuitChannelService>(CommonConsts.Ioc_CircuitChannel);
            AppContainer.ServiceCollection.AddSingleton<MqService>();
            AppContainer.ServiceCollection.AddSingleton<IRabbitMqClient, RabbitMqClient>();
        }

        private static void ConfigViews()
        {
            AppContainer.ServiceCollection.AddSingleton<MainWindowVm>();
            AppContainer.ServiceCollection.AddSingleton<MainWindow>( options =>
            {
                var window = new MainWindow();
                var vm = AppContainer.ServiceProvider.GetRequiredService<MainWindowVm>();
                window.DataContext = vm;
                return window;
            });

            AppContainer.ServiceCollection.AddSingleton<IRouteService, RouteService>();
            AppContainer.ServiceCollection.AddTransient<MenuBarViewmodel>();
            AppContainer.ServiceCollection.AddKeyedTransient<IPopupService, DiagramPopupService>(Consts.Ioc_Diagram);
            AppContainer.ServiceCollection.AddKeyedTransient<IPopupService, DevicePopupService>(Consts.Ioc_Device);
            AppContainer.ServiceCollection.AddKeyedTransient<ICustomPopupService, CustomPopupService>(Consts.Ioc_CustomPopup);

            AppContainer.ServiceCollection.AddKeyedSingleton<DiagramBase, StationPrimarySystemVm>(Consts.Electric_PrimarySystem);
            AppContainer.ServiceCollection.AddKeyedSingleton<DiagramBase, FullLineWiringVm>(Consts.Electric_FullWiring);
            AppContainer.ServiceCollection.AddKeyedSingleton<DiagramBase, FullLineWiringPopupVm>(Consts.Electric_FullWiringPopup);
            AppContainer.ServiceCollection.AddKeyedSingleton<DiagramBase, Power750OnVm>(Consts.Electric_750On);
            AppContainer.ServiceCollection.AddKeyedSingleton<DiagramBase, Power750OffVm>(Consts.Electric_750Off);

            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, SongjiazhuangPrimaryVm>(Consts.SJZ_S01);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, TiantandongVm>(Consts.TTD_S04);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, LiujiayaoVm>(Consts.LJY_S02);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, PuhuangyuVm>(Consts.PHY_S03);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, CiqikouVm>(Consts.CQK_S06);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, ChongwenmenVm>(Consts.CWM_S07);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, DongdanVm>(Consts.DD_S08);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, DengshikouVm>(Consts.DSK_S09);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, DongsiVm>(Consts.DS_S10);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, ZhangzizhongVm>(Consts.ZZZ_S11);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, BeixinqiaoVm>(Consts.BXQ_S12);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, YonghegongVm>(Consts.YHG_S13);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, HepingbeiVm>(Consts.HPB_S14);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, HepingxiVm>(Consts.HPX_S15);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, HuixinnanVm>(Consts.HXN_S16);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, HuixinbeiVm>(Consts.HXB_S17);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, DatundongVm>(Consts.DTD_S18);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, BeiyuanbeiVm>(Consts.BYB_S19);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, LishuinanVm>(Consts.LSN_S21);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, LishuiqiaoVm>(Consts.LSQ_S22);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, TiantongnanVm>(Consts.TTN_S24);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, TiantongyuanVm>(Consts.TTY);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, TiantongbeiVm>(Consts.TTB_S25);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, CheliangduanVm>(Consts.CLD);
            AppContainer.ServiceCollection.AddKeyedSingleton<PrimaryDiagram, TingchechangVm>(Consts.ParkingLot);
            AppContainer.ServiceCollection.AddTransient<BreakerExecuteVm>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = AppContainer.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            MainWindow = mainWindow;
        }

        private void UnHandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Log4Logger.Logger.Error($"An unhandled exception occurred: {args.Exception?.Message}");
            args.Handled = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DispatcherUnhandledException -= UnHandledExceptionHandler;
            base.OnExit(e);
        }
    }

}
