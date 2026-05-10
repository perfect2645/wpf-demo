using System.Windows.Threading;
using WpfUtils;

namespace wpf.ui.viewmodels.panels
{
    public class DatetimePanelVm : NotifyChanged
    {
        private string _currentDate = DateTime.Now.ToString("yyyy年MM月dd日");
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                NotifyUI(() => CurrentDate);
            }
        }

        private string _currentTime = DateTime.Now.ToString("HH:mm:ss");
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                NotifyUI(() => CurrentTime);
            }
        }

        private DispatcherTimer? _timer;

        public DatetimePanelVm()
        {
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, e) => {
                CurrentDate = DateTime.Now.ToString("yyyy年MM月dd日");
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            };
            _timer.Start();
        }

        public void Dispose()
        {
            _timer?.Stop();
        }
    }
}
