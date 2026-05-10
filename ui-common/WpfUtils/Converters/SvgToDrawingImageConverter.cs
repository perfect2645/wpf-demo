using Logging;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace WpfUtils.Converters
{
    public static class SvgToDrawingImageConverter
    {
        public static DrawingImage? ConvertSvgToDrawingImage(string svgPath)
        {
            if (string.IsNullOrEmpty(svgPath))
                throw new ArgumentNullException(nameof(svgPath));
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string fullPath = Path.Combine(baseDirectory, svgPath);

                if (!File.Exists(fullPath))
                    throw new FileNotFoundException("SVG file not exists.", fullPath);

                var settings = new WpfDrawingSettings
                {
                    TextAsGeometry = true, // 将文本转换为几何图形，确保显示一致
                    IncludeRuntime = false,
                };

                using (var reader = new FileSvgReader(settings))
                {
                    var drawingGroup = reader.Read(svgPath);
                    if (drawingGroup == null)
                        return null;

                    return new DrawingImage(drawingGroup);
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"SVG Converting failed: {ex.Message}");
                return null;
            }
        }

        public static async Task<DrawingImage?> ConvertSvgToDrawingImageAsync(string svgPath)
        {
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return ConvertSvgToDrawingImage(svgPath);
            });
        }
    }
}
