using Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Ioc
{
    public sealed class AppContainer
    {
        private static readonly Lazy<AppContainer> instance = new Lazy<AppContainer>(() => new AppContainer());

        private static IServiceCollection _serviceCollection;
        private static IServiceProvider? _serviceProvider;

        public static IServiceCollection ServiceCollection
        {
            get => _serviceCollection;
        }

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    _serviceProvider = _serviceCollection.BuildServiceProvider();
                }
                return _serviceProvider;
            }
        }

        private AppContainer() { }

        static AppContainer()
        {
            _serviceCollection = new ServiceCollection();
        }

        public static void Build()
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        #region Helper Methods

        public static TService? GetService<TService>() where TService : notnull
        {
            try
            {
                return ServiceProvider.GetRequiredService<TService>();
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex);
                return default(TService);
            }
        }

        public static TService? GetKeyedService<TService>(string key) where TService : notnull
        {
            try
            {
                return ServiceProvider.GetRequiredKeyedService<TService>(key);
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error(ex);
                return default(TService);
            }
        }

        #endregion Helper Methods
    }
}
