using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CyanKiteUtility
{
    public class MD5Helper
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <returns></returns>
        public static string MD5Encrypt16(string origin)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(origin)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileMD5(string path)
        {
            try
            {
                if (!FileOperateHelper.IsFileExist(path))
                {
                    return null;
                }
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    StringBuilder sb = new StringBuilder();
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] array = md5.ComputeHash(fs);
                    fs.Close();
                    for (int i = 0; i < array.Length; i++)
                    {
                        sb.Append(array[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


