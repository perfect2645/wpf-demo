using wpf.ui.model.payloads;
using wpf.ui.service.http;
using CommunityToolkit.Mvvm.ComponentModel;
using Ioc;
using Logging;
using Messaging.Http.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Threading;
using UserComponent.Core.route;
using UserComponent.Core.viewmodels.diagrams.abstractions;
using WpfUtils.Popup;

namespace wpf.ui.viewmodels.diagrams.abstractions
{
    public abstract partial class DiagramBase : ObservableObject, IActiveAware, IPopup
    {
        #region Properties

        public string? ViewId { get; init; }

        [ObservableProperty]
        private bool _isActive;

        public PopupSettings? PopupSettings { get; set; }

        public IRouteService RouteService { get; }

        #endregion Properties

        #region Constructor

        public DiagramBase()
        {
            RouteService = AppContainer.ServiceProvider.GetRequiredService<IRouteService>();
        }

        public DiagramBase(string? viewId) : this()
        {
            ViewId = viewId;
        }

        #endregion Constructor


        public void SetIsActive(bool isActive)
        {
            if (IsActive != isActive)
                IsActive = isActive;
        }

        public void RaiseAllPropertiesChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        public async Task InvokeAsync(Action action)
        {
            await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
            {
                action?.Invoke();
            });
        }

        #region LoadData



        //protected abstract Task<StationCircuitData?> RunSearchAsync(string stationId);



        //protected abstract void SetDataAsync(StationCircuitData? data);

        #endregion LoadData
    }
}
