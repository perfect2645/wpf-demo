using wpf.ui.route;
using CommunityToolkit.Mvvm.ComponentModel;
using Logging;
using System.Windows;
using System.Windows.Media;
using WpfUtils.Cache;
using WpfUtils.Converters;

namespace wpf.ui.viewmodels.panels
{
    public partial class DiagramPanelViewModel : ObservableObject
    {
        #region Test

        [ObservableProperty]
        private ImageSource? _imageSource;

        [ObservableProperty]
        private string? _imageSourcePath;

        [ObservableProperty]
        private string? _imageSourceKey;

        private async ValueTask<bool?> UpdateViewByImageAsync(string menuId)
        {
            if (!RouteCache.DiagramImgMapping.ContainsKey(menuId))
            {
                ImageSource = null;
                return false;
            }

            if (string.IsNullOrEmpty(RouteCache.DiagramImgMapping[menuId]))
            {
                ImageSource = null;
                return null;
            }

            if (!ImageCache.Exists(menuId))
            {
                var target = await SvgToDrawingImageConverter.ConvertSvgToDrawingImageAsync(RouteCache.DiagramImgMapping[menuId]);
                if (target == null)
                {
                    Log4Logger.Logger.Warn($"SVG Converting failed: {RouteCache.DiagramImgMapping[menuId]}");
                    ImageSource = null;
                    return null;
                }
                ImageCache.Set(menuId, target);
            }
            try
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ImageSource = ImageCache.Get(menuId);
                });
                return true;
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Error setting ImageSourcePath: {ex.Message}");
                ImageSourcePath = null;
                return null;
            }
        }

        #endregion Test
    }
}
