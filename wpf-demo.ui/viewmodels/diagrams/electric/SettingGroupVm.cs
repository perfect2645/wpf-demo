using wpf.ui.model.diagrams.electric;
using wpf.ui.route;
using wpf.ui.viewmodels.diagrams.abstractions;
using Logging;
using System.Collections.ObjectModel;
using UserComponent.Core.station;
using Utils;

namespace wpf.ui.viewmodels.diagrams.electric
{
    public class SettingGroupVm : DiagramBase
    {

        #region Properties

        public ObservableCollection<SettingGroupItem> Items { get; }
        = new ObservableCollection<SettingGroupItem>();

        public List<string> ColumnNames { get; }
            = new List<string> { "201", "215", "202", "225", "231", "241", "245", "246", "247" };

        public List<string> RowNames { get; }
            = new List<string> { "定值1", "定值2", "定值3", "定值4" };

        public int RowCount => RowNames.Count;
        public int ColumnCount => ColumnNames.Count;

        #endregion Properties

        public SettingGroupVm()
        {
            InitSettingGroup();
        }

        private void InitSettingGroup()
        {
            for (int row = 0; row < RowNames.Count; row++)
            {
                for (int col = 0; col < ColumnNames.Count; col++)
                {
                    Items.Add(new SettingGroupItem
                    {
                        RowName = RowNames[row],
                        ColName = ColumnNames[col],
                        IsActive = row == 0, // 第0行（定值1）默认激活
                        RowIndex = row,
                        ColIndex = col
                    });
                }
            }
        }

        public void SwitchSettingGroup(string rowName)
        {
            foreach (var item in Items)
            {
                item.IsActive = item.RowName == rowName;
            }
        }
    }
}
