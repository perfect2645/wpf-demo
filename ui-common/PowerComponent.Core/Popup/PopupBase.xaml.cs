using System.Windows;

namespace PowerComponent.Core.Popup
{
    /// <summary>
    /// PopupBase.xaml 的交互逻辑
    /// </summary>
    public partial class PopupBase : Window
    {
        public static readonly DependencyProperty DialogContentProperty =
            DependencyProperty.Register("DialogContent", typeof(object), typeof(PopupBase));

        public static readonly DependencyProperty DialogTitleProperty =
            DependencyProperty.Register("DialogTitle", typeof(string), typeof(PopupBase),
                new PropertyMetadata("默认标题"));

        public object DialogContent
        {
            get => GetValue(DialogContentProperty);
            set => SetValue(DialogContentProperty, value);
        }

        public string DialogTitle
        {
            get => (string)GetValue(DialogTitleProperty);
            set => SetValue(DialogTitleProperty, value);
        }

        public PopupBase()
        {
            InitializeComponent();
            DataContext = this;
        }

        public virtual void Confirm()
        {
            DialogResult = true;
            Close();
        }

        // 取消命令
        public virtual void Cancel()
        {
            DialogResult = false;
            Close();
        }
    }
}
