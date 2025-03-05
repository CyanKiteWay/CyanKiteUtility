using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CyanKiteUtility
{
    public static class RegularExpressionExtension
    {
        /// <summary>
        /// 验证字符串是否为IP
        /// </summary>
        /// <param name="strOriginal">原始字符串</param>
        /// <returns>是否为IP</returns>
        public static bool IPVerification(this string strOriginal)
        {
            Regex ipMatch = new Regex(@"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}", RegexOptions.IgnoreCase);
            var matches = ipMatch.Matches(strOriginal);
            if (matches == null || matches.Count != 1)
            {
                return false;
            }
            string matchIP = matches[0].ToString().Trim();

            var points = matchIP.Split('.');
            if (points.Length != 4)
            {
                return false;
            }
            foreach (var point in points)
            {
                if (!int.TryParse(point, out int value))
                {
                    return false;
                }
                else
                {
                    if (value < 0 || value > 255)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// 截取字符串中的IP
        /// </summary>
        /// <param name="strOriginal">原始字符串</param>
        /// <param name="replace">替换目标字符串</param>
        /// <returns>替换后的结果字符串</returns>
        public static List<string> SubstringIP(this string strOriginal)
        {
            List<string> result = new List<string>();
            Regex ipMatch = new Regex(@"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}", RegexOptions.IgnoreCase);
            var matches = ipMatch.Matches(strOriginal);
            foreach (var match in matches)
            {
                string matchIP = match.ToString().Trim();
                if (matchIP.IPVerification())
                {
                    result.Add(match.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// 将连续的一个或者多个空白符替换为指定字符串
        /// </summary>
        /// <param name="strOriginal">原始字符串</param>
        /// <param name="replace">替换目标字符串</param>
        /// <returns>替换后的结果字符串</returns>
        public static string ReplaceSpaces(this string strOriginal, string replace)
        {
            Regex replaceSpace = new Regex(@"\s{1,}", RegexOptions.IgnoreCase);
            return replaceSpace.Replace(strOriginal, replace).Trim();
        }
    }
}