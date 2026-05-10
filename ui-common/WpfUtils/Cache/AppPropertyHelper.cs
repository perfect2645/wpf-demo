using System.Windows;

namespace WpfUtils.Cache
{
    public static class AppPropertyHelper
    {
        public static bool TryGetProperty<T>(string key, out T? value)
        {
            if (key == null)
            {
                value = default;
                return false;
            }

            if (Application.Current.Properties.Contains(key))
            {
                value = (T?)Application.Current.Properties[key];
                return true;
            }

            value = default;
            return false;
        }

        public static void AddProperty(string key, object value)
        {
            Application.Current.Properties.Add(key, value);
        }
    }
}
