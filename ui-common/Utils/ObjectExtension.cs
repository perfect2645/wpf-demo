namespace Utils
{
    public static class ObjectExtension
    {
        public static int ToInt(this object? obj)
        {
            if (obj == null)
            {
                return 0;
            }

            var intResult = int.TryParse(obj.ToString(), out int result);
            if (!intResult)
            {
                var doubleResult = double.TryParse(obj.ToString(), out double doubleTry);
                if (doubleResult)
                {
                    return Convert.ToInt32(doubleTry);
                }
            }
            return Convert.ToInt32(obj);
        }

        public static string NotNullString(this object? source)
        {
            var strSource = source?.ToString();
            return strSource ?? string.Empty;
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 8, 0, 0, 0);
            var tsStr = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            return tsStr;
        }

        public static bool NotNullBool(this bool? source)
        {
            if (source == null)
            {
                return false;
            }

            return source.Value;
        }
    }
}
