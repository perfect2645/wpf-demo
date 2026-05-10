using CommunityToolkit.Mvvm.Input;
using Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Utils.Enumerable;
using WpfUtils;

namespace UserComponent.Core.menu
{
    public abstract class MenuButtonBaseVm : NotifyChanged, IDisposable
    {
        #region Properties

        public string Id { get; init; }
        public string? ParentId { get; set; }
        public bool IsFirstLevel { get => string.IsNullOrEmpty(ParentId); }
        public ICommand? ClickCommand { get; }
        public object? ClickCommandParameter { get; set; }

        private string? _content;
        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
                NotifyUI(() => Content);
            }
        }

        private double? _contentFontSize;
        public double? ContentFontSize
        {
            get => _contentFontSize;
            set
            {
                _contentFontSize = value;
                NotifyUI(() => ContentFontSize);
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                NotifyUI(() => IsVisible);
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyUI(() => IsSelected);
            }
        }

        private ObservableCollection<MenuButtonBaseVm>? _subItems;
        public ObservableCollection<MenuButtonBaseVm>? SubItems
        { 
            get => _subItems;
            set
            {
                _subItems = value;
                NotifyUI(() => SubItems);
            }
        }

        #endregion Properties

        #region Init

        public MenuButtonBaseVm(string id, string content)
        {
            Id = id;
            Content = content;
            ClickCommand = new RelayCommand<object>(OnMenuClick, CanMenuClick);

            AdjustContentFontSize();
            MenuEventManager.Subscribe(OnMenuSelected);
        }

        protected abstract void AdjustContentFontSize();

        #endregion Init

        #region Menu Selected

        private bool CanMenuClick(object? parameter)
        {
            return !IsSelected;
        }

        private void OnMenuClick(object? parameter)
        {
            var menuEventArgs = new MenuEventArgs(this);
            MenuEventManager.Publish(this, menuEventArgs);
        }

        private void OnMenuSelected(object? sender, MenuEventArgs e)
        {
            if (e.SelectedMenu == null)
            {
                Log4Logger.Logger.Warn($"SelectedMenu is null");
                return;
            }

            if (e.SelectedMenu.Id == Id && IsSelected)
            {
                return;
            }

            if (IsFirstLevel == e.SelectedMenu.IsFirstLevel)
            {
                ClearSelection();
            }
        }

        #endregion Menu Selected

        #region Clear

        public void ClearSelection(bool clearSelf = true)
        {
            if (clearSelf)
            IsSelected = false;

            if (!SubItems.HasItem())
            {
                return;
            }
            foreach (var subMenu in SubItems!)
            {
                subMenu.IsSelected = false;
            }
        }

        public void Dispose()
        {
            MenuEventManager.UnSubscribe(OnMenuSelected);
        }

        #endregion Clear
    }
}
