using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CyanKiteUtility
{
    public class IniConfigHelper
    {
        private IniHelper iniHelper;
        private string iniConfigSection;
        private bool isInit;

        public IniConfigHelper()
        {
            iniConfigSection = "Config";
            isInit = false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(string folder_path, string folder_name, string file_name, string default_file_content = "", string default_config_section = "Config")
        {
            try
            {
                iniConfigSection = default_config_section;
                string folder = Path.Combine(folder_path, folder_name);
                if (!FileOperateHelper.IsDirectoryExist(folder))
                {
                    FileOperateHelper.CreateDirectory(folder);
                }
                string file = Path.Combine(folder, file_name);
                if (!FileOperateHelper.IsFileExist(file))
                {
                    FileOperateHelper.CreateFile(file);
                    FileOperateHelper.WriteFile(file, default_file_content);
                }
                iniHelper = new IniHelper(file);
                isInit = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isInit = false;
            }
        }

        /// <summary>
        /// 读取类对象
        /// </summary>
        /// <param name="obj"></param>
        public void ReadObject(object obj, string sectionName = "")
        {
            if (!isInit)
            {
                return;
            }

            try
            {
                if (obj == null)
                    return;

                if (string.IsNullOrWhiteSpace(sectionName))
                {
                    sectionName = obj.GetType().Name;
                }
                PropertyInfo[] propertys = obj.GetType().GetProperties();
                foreach (PropertyInfo property in propertys)
                {
                    string value = iniHelper.ReadValue(sectionName, property.Name);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    try
                    {
                        var change_value = Convert.ChangeType(value, property.PropertyType);
                        property.SetValue(obj, change_value, null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error converting property '{property.Name}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 保存类对象
        /// </summary>
        public void WriteObject(object obj, string sectionName = "")
        {
            if (!isInit)
            {
                return;
            }

            try
            {
                if (obj == null)
                    return;

                if (string.IsNullOrWhiteSpace(sectionName))
                {
                    sectionName = obj.GetType().Name;
                }
                PropertyInfo[] propertys = obj.GetType().GetProperties();
                foreach (PropertyInfo property in propertys)
                {
                    try
                    {
                        string value = property.GetValue(obj)?.ToString();
                        if (value != null)
                        {
                            iniHelper.WriteValue(sectionName, property.Name, value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error writing property '{property.Name}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 读取配置下的字段
        /// </summary>
        public string ReadConfig(string name)
        {
            if (!isInit)
            {
                return "";
            }

            try
            {
                return iniHelper.ReadValue(iniConfigSection, name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 保存配置下的字段
        /// </summary>
        public void WriteConfig(string name, string value)
        {
            if (!isInit)
            {
                return;
            }

            try
            {
                iniHelper.WriteValue(iniConfigSection, name, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 读取配置下的字段
        /// </summary>
        public string ReadValue(string section, string name)
        {
            if (!isInit)
            {
                return "";
            }

            try
            {
                return iniHelper.ReadValue(section, name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public List<string> ReadKeys(string section)
        {
            if (!isInit)
            {
                return new List<string>();
            }

            try
            {
                return iniHelper.ReadKeys(section);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<string>();
        }
    }
}
