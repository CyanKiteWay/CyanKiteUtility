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
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToXml(this object o)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                NewLineHandling = NewLineHandling.None,
                Indent = false,
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
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlText"></param>
        /// <returns></returns>
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
    }
}
