using System;
using System.IO;
using System.Text;

namespace CyanKiteUtility
{
    public class FileOperateHelper
    {
        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        public static bool IsDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        public static void CreateDirectory(string path)
        {
            //如果目录不存在则创建该目录
            if (!IsDirectoryExist(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 检测指定文件是否存在
        /// </summary>
        public static bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 创建一个文件。
        /// </summary>
        public static void CreateFile(string path)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsFileExist(path))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(path);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 写文件
        /// 当文件不存在时，则创建文件
        /// 当文件存时，clear：true 覆盖内容；clear：false 追加内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="clear">是否清空文件</param>
        public static void WriteFile(string path, string content, bool clear = true)
        {
            if (clear || !System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, true, Encoding.UTF8);
            f2.WriteLine(content);
            f2.Close();
            f2.Dispose();
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            if (!IsFileExist(path))
            {
                Console.WriteLine($"找不到对应的文件：{path}");
                return "";
            }
            StreamReader f2 = new StreamReader(path, Encoding.UTF8);
            string result = f2.ReadToEnd();
            f2.Close();
            f2.Dispose();

            return result;
        }
    }
}
