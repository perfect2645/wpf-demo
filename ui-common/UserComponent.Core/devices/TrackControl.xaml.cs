using Messaging.LocalMessages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UserComponent.Core.devices
{
    /// <summary>
    /// TrackControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrackControl : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(TrackControl);

        public bool IsPowered
        {
            get { return (bool)GetValue(IsPoweredProperty); }
            set { SetValue(IsPoweredProperty, value); }
        }

        public static readonly DependencyProperty IsPoweredProperty =
            DependencyProperty.Register("IsPowered", typeof(bool), ControlType, new PropertyMetadata(IsPoweredChanged));

        public string StationId
        {
            get { return (string)GetValue(StationIdProperty); }
            set { SetValue(StationIdProperty, value); }
        }

        public static readonly DependencyProperty StationIdProperty =
            DependencyProperty.Register("StationId", typeof(string), ControlType);

        public string DeviceId
        {
            get { return (string)GetValue(DeviceIdProperty); }
            set { SetValue(DeviceIdProperty, value); }
        }

        public static readonly DependencyProperty DeviceIdProperty =
            DependencyProperty.Register("DeviceId", typeof(string), ControlType);

        public Brush PoweredBrush
        {
            get { return (Brush)GetValue(PoweredBrushProperty); }
            set { SetValue(PoweredBrushProperty, value); }
        }

        public static readonly DependencyProperty PoweredBrushProperty =
            DependencyProperty.Register("PoweredBrush", typeof(Brush), ControlType);

        public Brush PowerOffBrush
        {
            get { return (Brush)GetValue(PowerOffBrushProperty); }
            set { SetValue(PowerOffBrushProperty, value); }
        }

        public static readonly DependencyProperty PowerOffBrushProperty =
            DependencyProperty.Register("PowerOffBrush", typeof(Brush), ControlType, new PropertyMetadata(Brushes.Gray));

        public Brush CurrentBrush
        {
            get { return (Brush)GetValue(CurrentBrushProperty); }
            set { SetValue(CurrentBrushProperty, value); }
        }

        public static readonly DependencyProperty CurrentBrushProperty =
            DependencyProperty.Register("CurrentBrush", typeof(Brush), ControlType);

        public TransationMode TransationMode
        {
            get { return (TransationMode)GetValue(TransationModeProperty); }
            set { SetValue(TransationModeProperty, value); }
        }

        public static readonly DependencyProperty TransationModeProperty =
            DependencyProperty.Register("TransationMode", typeof(TransationMode), ControlType);

        #endregion Properties

        public TrackControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            (DataContext as TrackViewModel)?.Dispose();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CurrentBrush = PowerOffBrush;
            if (StationId == null)
            {
                throw new ArgumentNullException($"Device:[{DeviceId}] - StationId is null.");
            }
            if (string.IsNullOrEmpty(DeviceId))
            {
                throw new ArgumentNullException($"Station:[{StationId}] - DeviceId is empty.");
            }

            if (DataContext == null || DataContext is not TrackViewModel)
            {
                var vm = new TrackViewModel(StationId, DeviceId)
                {
                    TransationMode = TransationMode
                };
                DataContext = vm;
            }
            (DataContext as TrackViewModel)!.OnLoaded();
        }

        private static void IsPoweredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TrackControl control)
            {
                if ((bool)e.NewValue)
                {
                    control.CurrentBrush = control.PoweredBrush;
                }
                else
                {
                    control.CurrentBrush = control.PowerOffBrush;
                }
            }
        }
    }
}
