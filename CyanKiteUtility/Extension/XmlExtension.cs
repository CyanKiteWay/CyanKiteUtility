using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CyanKiteUtility
{
    public static class XmlExtension
    {
        /// <summary>
        /// 把对象转换为xml格式化字符串
        /// </summary>
        [Obsolete("此方法已经过时, 请使用新方法\"ToXmlStr\".", false)]
        public static string ToXml(this object o)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                //设置为true表示在输出的XML中省略XML声明（即不包含<?xml version="1.0" encoding="utf-8"?>这一行）
                OmitXmlDeclaration = true,
                //保留原始的换行符
                NewLineHandling = NewLineHandling.None,
                //设置为false表示在输出的XML中不进行缩进，即不添加额外的空白字符来格式化XML。
                Indent = false,
                //在输出的XML中省略重复的命名空间声明
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
            XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces(
                          new XmlQualifiedName[] {
                        new XmlQualifiedName(string.Empty)
                       });

            StringWriter stringwriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringwriter, settings);
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, _namespaces);
            string result = stringwriter.ToString();
            return result;
        }

        /// <summary>
        /// 把xml格式化字符串转换为对象
        /// </summary>
        [Obsolete("此方法已经过时, 请使用新方法\"FromXmlStr\".", false)]
        public static T FromXml<T>(this string xmlText) where T : class
        {
            if (xmlText == null)
            {
                xmlText = "";
            }
            xmlText = xmlText.Replace("&", "&amp;");
            var stringReader = new StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(stringReader) as T;
        }

        /// <summary>
        /// 把对象转换为xml格式化字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>结果字符串</returns>
        public static string ToXmlStr(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("转换对象不能为空");
            }
            XmlWriterSettings settings = new XmlWriterSettings
            {
                //设置为true表示在输出的XML中省略XML声明（即不包含<?xml version="1.0" encoding="utf-8"?>这一行）
                OmitXmlDeclaration = true,
                //保留原始的换行符
                NewLineHandling = NewLineHandling.None,
                //设置为false表示在输出的XML中不进行缩进，即不添加额外的空白字符来格式化XML。
                Indent = false,
                //在输出的XML中省略重复的命名空间声明
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
            XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces(
                new XmlQualifiedName[] {
                    new XmlQualifiedName(string.Empty)
                });

            StringWriter stringwriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringwriter, settings);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(xmlWriter, obj, _namespaces);
            return stringwriter.ToString();
        }

        /// <summary>
        /// 用xml字符串格式化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="xmlText"></param>
        public static T XmlStrToObject<T>(this string xmlText)
        {
            xmlText = xmlText.Replace("&", "&amp;");
            var stringReader = new StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }

        /// <summary>
        /// 把对象保存为xml文件
        /// </summary>
        /// <param name="obj">要保存的对象</param>
        public static void SaveToXmlFile(this object obj, string path)
        {
            if (obj == null)
            {
                throw new ArgumentException("保存对象不能为空");
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("文件路径不能为空");
            }

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, obj);
            }

        }

        /// <summary>
        /// 用xml字符串格式化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlText"></param>
        public static T ReadXmlFileToObject<T>(this string path) where T : class
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("文件路径不能为空");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("文件路径不存在", path);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(path))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
