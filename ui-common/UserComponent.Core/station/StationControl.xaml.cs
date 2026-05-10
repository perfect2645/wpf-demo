using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserComponent.Core.station
{
    /// <summary>
    /// StationControl.xaml 的交互逻辑
    /// </summary>
    public partial class StationControl : UserControl
    {
        public StationControl()
        {
            InitializeComponent();
        }
        #region Properties

        private static readonly Type ControlType = typeof(StationControl);

        #region Line

        public bool ShowLine
        {
            get { return (bool)GetValue(ShowLineProperty); }
            set { SetValue(ShowLineProperty, value); }
        }

        public static readonly DependencyProperty ShowLineProperty =
            DependencyProperty.Register("ShowLine", typeof(bool), ControlType, new PropertyMetadata(true));

        public int LineX1
        {
            get { return (int)GetValue(LineX1Property); }
            set { SetValue(LineX1Property, value); }
        }

        public static readonly DependencyProperty LineX1Property =
            DependencyProperty.Register("LineX1", typeof(int), ControlType, new PropertyMetadata(0));

        public int LineX2
        {
            get { return (int)GetValue(LineX2Property); }
            set { SetValue(LineX2Property, value); }
        }

        public static readonly DependencyProperty LineX2Property =
            DependencyProperty.Register("LineX2", typeof(int), ControlType, new PropertyMetadata(0));

        public int LineY1
        {
            get { return (int)GetValue(LineY1Property); }
            set { SetValue(LineY1Property, value); }
        }

        public static readonly DependencyProperty LineY1Property =
            DependencyProperty.Register("LineY1", typeof(int), ControlType, new PropertyMetadata(0));

        public int LineY2
        {
            get { return (int)GetValue(LineY2Property); }
            set { SetValue(LineY2Property, value); }
        }

        public static readonly DependencyProperty LineY2Property =
            DependencyProperty.Register("LineY2", typeof(int), ControlType, new PropertyMetadata(0));

        #endregion Line

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), ControlType);



        public double PaddingOffset
        {
            get { return (double)GetValue(PaddingOffsetProperty); }
            set { SetValue(PaddingOffsetProperty, value); }
        }

        public static readonly DependencyProperty PaddingOffsetProperty =
            DependencyProperty.Register("PaddingOffset", typeof(double), ControlType);

        #endregion Title

        #region Button

        public ControlTemplate NormalTemplate
        {
            get => (ControlTemplate)GetValue(NormalTemplateProperty);
            set => SetValue(NormalTemplateProperty, value);
        }
        public static readonly DependencyProperty NormalTemplateProperty =
            DependencyProperty.Register(
                "NormalTemplate",
                typeof(ControlTemplate),
                ControlType
            );

        public ControlTemplate PressedTemplate
        {
            get => (ControlTemplate)GetValue(PressedTemplateProperty);
            set => SetValue(PressedTemplateProperty, value);
        }
        public static readonly DependencyProperty PressedTemplateProperty =
            DependencyProperty.Register(
                "PressedTemplate",
                typeof(ControlTemplate),
                ControlType
            );

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand),
                ControlType, new PropertyMetadata(OnClickCommandChanged));

        private static void OnClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public object ClickCommandParameter
        {
            get { return GetValue(ClickCommandParameterProperty); }
            set { SetValue(ClickCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandParameterProperty =
            DependencyProperty.Register("ClickCommandParameter", typeof(object),
                ControlType, new PropertyMetadata(null));



        #endregion Button

        #endregion Properties
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickCommand?.Execute(ClickCommandParameter);
        }
    }
}
