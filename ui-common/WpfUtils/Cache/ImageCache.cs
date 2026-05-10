using Logging;
using System.Collections.Concurrent;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using Utils.Enumerable;
using Utils.Tasking;

namespace WpfUtils.Cache
{
    public static class ImageCache
    {
        private static readonly ConcurrentDictionary<string, DrawingImage> _drawingImageCache = new ConcurrentDictionary<string, DrawingImage>();

        static ImageCache()
        {
            var xamlMap = new Dictionary<string, string>()
            {
                { "design/全线接线图1.xaml", "电力-全线接线图^1" },
                { "design/全线接线图2.xaml", "电力-全线接线图^2" },
                { "design/全线接线图3.xaml", "电力-全线接线图^3" },
                { "design/全线接线图4.xaml", "电力-全线接线图^4" }
            };
            var tasks = InitAsync(xamlMap);
            var task = Task.WhenAll(tasks);
            task.SafeFireAndForget();
        }

        public static async ValueTask InitAsync()
        {
            await ValueTask.CompletedTask;
        }

        public static List<Task> InitAsync(Dictionary<string, string> xamlMap)
        {
            var result = new List<Task>();
            foreach(var xaml in xamlMap)
            {
                var xamlTask = Task.Run(() => LoadAndCacheDrawingImageAsync(xaml.Key, xaml.Value));
                result.Add(xamlTask);
            }

            return result;
        }

        private static void LoadAndCacheDrawingImageAsync(string filePath, string name)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);

                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var reader = new XamlReader();
                    object content = reader.LoadAsync(stream);

                    if (content is DrawingGroup drawingGroup)
                    {
                        var drawingImage = new DrawingImage(drawingGroup);
                        drawingImage.Freeze();
                        _drawingImageCache.TryAdd(name, drawingImage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Logger.Logger.Error($"Failed to convert XAML file: {filePath}", ex);
            }
        }

        public static bool Exists(string key)
        {
            return _drawingImageCache.Exists(key);
        }

        public static DrawingImage? Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            _drawingImageCache.TryGetValue(key, out var image);
            return image;
        }

        public static void Set(string key, DrawingImage image)
        {
            if (string.IsNullOrWhiteSpace(key) || image == null)
                return;

            _drawingImageCache[key] = image;
        }

        public static bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;

            return _drawingImageCache.TryRemove(key, out _);
        }

        public static void Clear()
        {
            _drawingImageCache.Clear();
        }

        public static int Count => _drawingImageCache.Count;
    }
}
