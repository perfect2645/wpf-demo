using wpf.ui.constants;
using wpf.ui.controls.buttons;
using wpf.ui.model.diagrams.electric;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public partial class PowerOnOffVm : DiagramBase
    {
        #region Properties

        [ObservableProperty]
        private string? _captionText;

        [ObservableProperty]
        private Brush? _captionForeground;

        [ObservableProperty]
        private ObservableCollection<PowerOnOffItem>? _itemSource;

        #endregion Properties

        public PowerOnOffVm(string ViewId) : base(ViewId)
        {
            InitViewSettings();
            InitPopupSettings();
            LoadData();
        }

        private void InitViewSettings()
        {
            if (ViewId == Consts.Electric_PowerOn)
            { 
                CaptionText = "750V送电顺控";
                CaptionForeground = Brushes.Red;
            } 
            else if (ViewId == Consts.Electric_PowerOff)
            {
                CaptionText = "750V停电顺控";
                CaptionForeground = Brushes.Blue;
            }
        }

        private void InitPopupSettings()
        {
            PopupSettings = PopupSettings ?? new PopupSettings
            {
                WindowStyle = WindowStyle.None,
                IsModal = false,
                Width = 600,
                Height = 800,
                Left = 0,
                Top = 100
            };
        }

        private void LoadData()
        {

            if (ViewId == Consts.Electric_PowerOn)
            {
                LoadDataForPowerOn();
            }
            else if (ViewId == Consts.Electric_PowerOff)
            {
                LoadDataForPowerOff();
            }
        }

        private void LoadDataForPowerOn()
        {
            var stationName = RouteService.SelectedStation?.Title;
            ItemSource = new ObservableCollection<PowerOnOffItem>
            {
                new($"{stationName}_65", "65", BreakerState.Close, BreakerType.Isolation,
                    null, null, null) {Background1 = Brushes.Transparent, BorderBrush1 = Brushes.Blue},
                new($"{stationName}_61", "61", BreakerState.Close, BreakerType.Isolation,
                    null, null, null),
                new($"{stationName}_60", "60", BreakerState.Close, BreakerType.Breaker,
                    null, null, null),
                new($"{stationName}_75", "75", BreakerState.Close, BreakerType.Isolation,
                    null, null, null) {Background1 = Brushes.Transparent, BorderBrush1 = Brushes.Blue},
                new($"{stationName}_71", "71", BreakerState.Close, BreakerType.Isolation,
                    null, null, null),
                new($"{stationName}_70", "70", BreakerState.Close, BreakerType.Breaker,
                    null, null, null),
                new($"{stationName}_10/14", "10/14", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, null),
                new($"{stationName}_20/24", "20/24", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, null),
                new($"{stationName}_30/34", "30/34", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, null),
                new($"{stationName}_40/44", "40/44", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, null),
                new($"{stationName}_50/54", "50/54", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, null),
                new($"{stationName}_90", "90", BreakerState.Open, BreakerType.Breaker,
                    null, null, null) {Background1 = Brushes.Yellow, BorderBrush1 = Brushes.Transparent},
                new($"{stationName}_80", "80", BreakerState.Close, BreakerType.Breaker,
                    null, null, null),
            };
        }
        private void LoadDataForPowerOff()
        {
            var stationName = RouteService.SelectedStation?.Title;
            ItemSource = new ObservableCollection<PowerOnOffItem>
            {
                new($"{stationName}_10/14", "10/14", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, "可控"),
                new($"{stationName}_20/24", "20/24", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, "可控"),
                new($"{stationName}_30/34", "30/34", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, "可控"),
                new($"{stationName}_40/44", "40/44", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, "可控"),
                new($"{stationName}_50/54", "50/54", BreakerState.Close, BreakerType.Breaker,
                    BreakerState.Open, BreakerType.Isolation, "可控"),
                new($"{stationName}_90", "90", BreakerState.Open, BreakerType.Breaker,
                    null, null, "不可控") {Background1 = Brushes.Yellow, BorderBrush1 = Brushes.Transparent},
                new($"{stationName}_80", "80", BreakerState.Close, BreakerType.Breaker,
                    null, null, "可控"),
                new($"{stationName}_60", "60", BreakerState.Close, BreakerType.Breaker,
                    null, null, "可控"),
                new($"{stationName}_61", "61", BreakerState.Close, BreakerType.Isolation,
                    null, null, "可控"),
                new($"{stationName}_65", "65", BreakerState.Close, BreakerType.Isolation,
                    null, null, "可控") {Background1 = Brushes.Transparent, BorderBrush1 = Brushes.Blue},
                new($"{stationName}_70", "70", BreakerState.Close, BreakerType.Breaker,
                    null, null, "可控"),
                new($"{stationName}_71", "71", BreakerState.Close, BreakerType.Isolation,
                    null, null, "可控"),
                new($"{stationName}_75", "75", BreakerState.Close, BreakerType.Isolation,
                    null, null, "可控") {Background1 = Brushes.Transparent, BorderBrush1 = Brushes.Blue}
            };
        }
    }
}
