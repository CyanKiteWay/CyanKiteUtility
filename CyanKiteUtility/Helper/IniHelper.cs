using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CyanKiteUtility
{
    public class IniHelper
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileStringA(string section, string key, string def, byte[] retVal, int size, string filePath);

        private string sPath = null;

        public IniHelper(string path)
        {
            this.sPath = path;
        }

        public void WriteValue(string section, string key, string value)
        {
            WriteString(section, key, value, sPath);
        }

        public string ReadValue(string section, string key)
        {
            return ReadString(section, key, "", sPath);
        }

        public List<string> ReadKeys(string section)
        {
            var result = ReadAllKeyString(section, sPath);
            if (!string.IsNullOrWhiteSpace(result.Trim()))
            {
                var list = result.Split('\0');
                return list.Where(item=> !string.IsNullOrWhiteSpace(item)).ToList();
            }
            return new List<string>();
        }

        public List<string> ReadSections()
        {
            var result = ReadAllSectionString(sPath);
            if (!string.IsNullOrWhiteSpace(result.Trim()))
            {
                var list = result.Split('\0');
                return list.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            }
            return new List<string>();

        }


        //与ini交互必须统一编码格式
        public static byte[] GetBytes(string s, string encodingName)
        {
            return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
        }

        public static string ReadString(string section, string key, string def, string fileName, string encodingName = "utf-8", int size = 1024)
        {
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileString(GetBytes(section, encodingName), GetBytes(key, encodingName), GetBytes(def, encodingName), buffer, size, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }

        public static bool WriteString(string section, string key, string value, string fileName, string encodingName = "utf-8")
        {
            return WritePrivateProfileString(GetBytes(section, encodingName), GetBytes(key, encodingName), GetBytes(value, encodingName), fileName);
        }

        public static string ReadAllKeyString(string section, string fileName, string encodingName = "utf-8", int size = 65536)
        {
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }

        public static string ReadAllSectionString(string fileName, string encodingName = "utf-8", int size = 65536)
        {
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }
    }
}
