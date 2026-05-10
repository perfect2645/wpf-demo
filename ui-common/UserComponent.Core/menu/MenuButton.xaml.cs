using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using Utils;

namespace UserComponent.Core.menu
{
    /// <summary>
    /// MenuButton.xaml 的交互逻辑
    /// </summary>
    public partial class MenuButton : UserControl
    {
        #region Properties

        private static readonly Type ControlType = typeof(MenuButton);
            
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

        public string ContentText
        {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), ControlType);

        #region FontSize Automation


        public double ContentFontSize
        {
            get { return (double)GetValue(ContentFontSizeProperty); }
            set { SetValue(ContentFontSizeProperty, value); }
        }

        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.Register("ContentFontSize", typeof(double), ControlType, new PropertyMetadata(12.0, OnFontSizeChanged));

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion FontSize Automation

        #endregion Properties
        public MenuButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickCommand?.Execute(ClickCommandParameter);
        }
    }
}
