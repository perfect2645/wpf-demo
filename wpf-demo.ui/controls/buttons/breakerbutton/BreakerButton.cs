using System.Windows;
using System.Windows.Controls;

namespace wpf.ui.controls.buttons
{
    public class BreakerButton : Button
    {
        public static readonly DependencyProperty BreakerStateProperty =
            DependencyProperty.Register("BreakerState", typeof(BreakerState), typeof(BreakerButton), new PropertyMetadata(BreakerState.Open));

        public BreakerState BreakerState
        {
            get => (BreakerState)GetValue(BreakerStateProperty);
            set => SetValue(BreakerStateProperty, value);
        }

        public static readonly DependencyProperty BreakerTypeProperty =
            DependencyProperty.Register("BreakerType", typeof(BreakerType), typeof(BreakerButton), new PropertyMetadata(BreakerType.Breaker));


        public BreakerType BreakerType
        {
            get => (BreakerType)GetValue(BreakerTypeProperty);
            set => SetValue(BreakerTypeProperty, value);
        }

        static BreakerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreakerButton),
                new FrameworkPropertyMetadata(typeof(BreakerButton)));
        }
    }
}
