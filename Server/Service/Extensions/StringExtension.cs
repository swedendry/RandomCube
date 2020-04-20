using System;
using System.Linq;

namespace Service.Extensions
{
    public static class StringExtension
    {
        public static T Parse<T>(this string value)
        {
            var defaultValue = default(T);
            var isEnum = (defaultValue != null && defaultValue.GetType().IsEnum);
            return isEnum ? (T)Enum.Parse(typeof(T), value, true) : (T)Convert.ChangeType(value, typeof(T));
        }

        public static T[] Split<T>(this string value, char separator)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            return value.Split(separator).Select(x => x.Parse<T>()).ToArray();
        }

        public static string Join<T>(this T[] value, char separator)
        {
            if (value == null)
                return default;

            return string.Join(separator, value);
        }
    }
}
