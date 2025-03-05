using System;
using System.Collections.Generic;
using System.Text;

namespace CyanKiteUtility
{
    public static class ObjectGetBytesExtension
    {
        /// <summary>
        /// 将给定的变量转换为byte数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this object value)
        {
            if(value == null)
            {
                return null;
            }

            if (value is byte)
            {
                return new byte[] { (byte)value };
            }
            else if (value is short)
            {
                return BitConverter.GetBytes((short)value);
            }
            else if (value is int)
            {
                return BitConverter.GetBytes((int)value);
            }
            else if (value is long)
            {
                return BitConverter.GetBytes((long)value);
            }
            else if (value is float)
            {
                return BitConverter.GetBytes((float)value);
            }
            else if (value is double)
            {
                return BitConverter.GetBytes((double)value);
            }
            else if (value is string)
            {
                return Encoding.ASCII.GetBytes((string)value);
            }
            else if (value is CustomFixedLengthString)
            {
                return Encoding.ASCII.GetBytes(((CustomFixedLengthString)value).CharArray);
            }
            else if (value is Array)
            {
                List<byte> bytes = new List<byte>();
                foreach (var item in (Array)value)
                {
                    bytes.AddRange(item.GetBytes());
                }
                return bytes.ToArray();
            }
            return null;
        }
    }

    /// <summary>
    /// 固定长度的字符串
    /// </summary>
    public class CustomFixedLengthString
    {
        public char[] CharArray { get; set; }

        public CustomFixedLengthString(string content, int length = 0):this(content.ToCharArray(),length)
        {
        }

        public CustomFixedLengthString(char[] content, int length = 0)
        {
            var chars = content;
            if (length <= 0)
            {
                CharArray = chars;
                return;
            }
            CharArray = new char[length];

            int minLength = chars.Length > length ? length : chars.Length;
            for (int i = 0; i < minLength; i++)
            {
                CharArray[i] = chars[i];
            }
        }
    }
}
