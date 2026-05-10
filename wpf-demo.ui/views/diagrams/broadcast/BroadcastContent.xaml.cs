using System.Windows.Controls;

namespace wpf.ui.views.diagrams.broadcast
{
    /// <summary>
    /// BroadcastContent.xaml 的交互逻辑
    /// </summary>
    public partial class BroadcastContent : UserControl
    {
        public BroadcastContent()
        {
            InitializeComponent();
            for (int i = 0; i < 15; i++)
            {
                this.BroadcastContentDataGrid.Items.Add(new { });
            }
        }
    }
}
