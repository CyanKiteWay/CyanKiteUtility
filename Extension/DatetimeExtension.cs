using System;
using System.Globalization;

namespace CyanKiteUtility
{
    public static class DatetimeExtension
    {
        /// <summary>
        /// 时间转毫秒长时间字符串
        /// </summary>
        public static string ToMillisecondLongTime(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss:fff");
        }

        /// <summary>
        /// 时间转毫秒短时间字符串
        /// </summary>
        public static string ToMillisecondShortTime(this DateTime time)
        {
            return time.ToString("HH:mm:ss:fff");
        }

        /// <summary>
        /// 时间转秒短时间字符串
        /// </summary>
        public static string ToSecondShortTime(this DateTime time)
        {
            return time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 毫秒长时间字符串转DateTime
        /// </summary>
        public static DateTime? FromMillisecondLongTime(this string time)
        {
            if (DateTime.TryParseExact(time, "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// 毫秒短时间字符串转DateTime
        /// </summary>
        public static DateTime? FromMillisecondShortTime(this string time)
        {
            if (DateTime.TryParseExact(time, "HH:mm:ss:fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                return value;
            }
            return null;
        }
    }
}
