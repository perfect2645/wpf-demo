using wpf.ui.model.diagrams.schedule;
using wpf.ui.viewmodels.diagrams.abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Interop;

namespace wpf.ui.viewmodels.diagrams.envControl.schedule
{
    public partial class TimeTableVm : DiagramBase
    {
        [ObservableProperty]
        private ObservableCollection<TimeTableItem>? _itemSource;
        public ICommand? PreviousPageClickCommand { get; set; }
        public ICommand? NextPageClickCommand { get; set; }
        [ObservableProperty]
        private bool _canPreviousPageButtonClick;
        [ObservableProperty]
        private bool _canNextPageButtonClick;
        public TimeTableVm()
        {
            ItemSource = GetFirstPageData();
            CanPreviousPageButtonClick = false;
            CanNextPageButtonClick = true;
            PreviousPageClickCommand = new RelayCommand(OnPreviousPageButtonClick);
            NextPageClickCommand = new RelayCommand(OnNextPageButtonClick);
        }

        private void OnPreviousPageButtonClick()
        {
            ItemSource = GetFirstPageData();
            CanPreviousPageButtonClick = false;
            CanNextPageButtonClick = true;
        }

        private void OnNextPageButtonClick()
        {
            ItemSource = GetSecondPageData();
            CanPreviousPageButtonClick = true;
            CanNextPageButtonClick = false;
        }

        private ObservableCollection<TimeTableItem> GetFirstPageData()
        {
            return new ObservableCollection<TimeTableItem>
            {
                new TimeTableItem { Id = "1", Category = "南端空调大系", Time = "0000", Mode = "040106  南端空调大系统冬季（系统关闭）" },
                new TimeTableItem { Id = "2", Category = "南端空调大系", Time = "0500", Mode = "040101  南端空调大系统空调季小新风2" },
                new TimeTableItem { Id = "3", Category = "北端空调大系", Time = "0000", Mode = "040206  北端空调大系统冬季（系统关闭）" },
                new TimeTableItem { Id = "4", Category = "北端空调大系", Time = "0500", Mode = "040201  北端空调大系统空调季小新风2" },
                new TimeTableItem { Id = "5", Category = "南端附属用房", Time = "0000", Mode = "040301  南端附属用房空调通风模式小新风" },
                new TimeTableItem { Id = "6", Category = "北端附属用房", Time = "0000", Mode = "040401  北端附属用房空调通风模式小新风" },
                new TimeTableItem { Id = "7", Category = "南端通风机房", Time = "0000", Mode = "040501  南端通风机房通风模式通风" },
                new TimeTableItem { Id = "8", Category = "北端通风机房", Time = "0000", Mode = "040601  北端通风机房通风模式通风" },
                new TimeTableItem { Id = "9", Category = "南端附属用房", Time = "0000", Mode = "040701  南端附属用房2通风模式通风" },
                new TimeTableItem { Id = "10", Category = "南端变电机房", Time = "0000", Mode = "040801  南端变电用房通风模式空调季" },
                new TimeTableItem { Id = "11", Category = "北端附属用房", Time = "0000", Mode = "040901  北端附属用房2通风模式" },
                new TimeTableItem { Id = "12", Category = "南端冷冻战通", Time = "0000", Mode = "041001  南端冷冻站通风模式通风" },
                new TimeTableItem { Id = "13", Category = "北段水泵房", Time = "0000", Mode = "041101  北端水泵房通风模式通风" },
                new TimeTableItem { Id = "14", Category = "空调水系统", Time = "0600", Mode = "041305  空调水系统自由模式" },
                new TimeTableItem { Id = "15", Category = "空调水系统", Time = "0630", Mode = "041304  空调水系统自由模式" },
                new TimeTableItem { Id = "16", Category = "空调水系统", Time = "2200", Mode = "041307  空调水系统关闭模式（非空调季）" },
                new TimeTableItem { Id = "17", Category = "空调水系统", Time = "2215", Mode = "041306  空调水系统自由模式" },
                new TimeTableItem { Id = "18", Category = "南端照明系统", Time = "0510", Mode = "041506  南端照明系统高峰时段" },
                new TimeTableItem { Id = "19", Category = "南端照明系统", Time = "0700", Mode = "041503  南端照明系统高峰时段" },
                new TimeTableItem { Id = "20", Category = "南端照明系统", Time = "1700", Mode = "041506  南端照明系统高峰时段" }
            };
        }

        private ObservableCollection<TimeTableItem> GetSecondPageData()
        {
            return new ObservableCollection<TimeTableItem>
            {
                new TimeTableItem { Id = "21", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "22", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "23", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "24", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "25", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "26", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "27", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "28", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "29", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "30", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "31", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "32", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "33", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "34", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "35", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "36", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "37", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "38", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "39", Category = "", Time = "0000", Mode = "040000" },
                new TimeTableItem { Id = "40", Category = "", Time = "0000", Mode = "040000" }
            };
        }
    }
}
