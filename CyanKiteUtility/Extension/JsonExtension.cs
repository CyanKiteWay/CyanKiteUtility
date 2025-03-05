using Newtonsoft.Json;
using System;
using System.IO;

namespace CyanKiteUtility
{
    public static class JsonExtension
    {
        public static JsonSerializerSettings CustomSettings = new JsonSerializerSettings
        {
            //Formatting = Formatting.Indented,
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        [Obsolete("此方法已经过时, 请使用新方法\"ToJsonStr\".", false)]
        public static string ToJSON(this object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }

        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("此方法已经过时, 请使用新方法\"JsonStrToObject\".", false)]
        public static T FromJSON<T>(this string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(T);
            }
        }

        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonStr(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("转换对象不能为空");
            }
            return JsonConvert.SerializeObject(obj, CustomSettings);
        }

        /// <summary>
        /// 把Json字符串转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T JsonStrToObject<T>(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("转换对象不能为空");
            }
            return JsonConvert.DeserializeObject<T>(input, CustomSettings);
        }

        /// <summary>
        /// 把对象保存为Json文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void SaveToJsonFile(this object obj, string path)
        {
            if (obj == null)
            {
                throw new ArgumentException("保存对象不能为空");
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("文件路径不能为空");
            }
            string str = JsonConvert.SerializeObject(obj, CustomSettings);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(str);
            }            
        }

        /// <summary>
        /// 读取json文件并用来格式化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static T ReadJsonFileToObject<T>(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("文件路径不能为空");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("文件路径不存在", path);
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json, CustomSettings);
            }
        }

    }
}
