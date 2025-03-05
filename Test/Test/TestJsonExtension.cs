using CyanKiteUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test.Tets
{
    public class TestJsonExtension
    {
        public static void Test()
        {
            TestConfig testObj1 = new TestConfig();
            testObj1.IP = "127.0.0.2";
            testObj1.Number = 3307;
            testObj1.Name = "root2";
            testObj1.Address = "23.365.256.89";
            string jsonstr = testObj1.ToJsonStr();
            string xmlstr = testObj1.ToXmlStr();

            TestConfig testObj2 = jsonstr.JsonStrToObject<TestConfig>();
            TestConfig testObj3 = xmlstr.XmlStrToObject<TestConfig>();

            string path1 = "data.json";

            TestConfig testObj4 = new TestConfig();
            testObj4.IP = "127.0.0.3";
            testObj4.Number = 3305;
            testObj4.Name = "root3";
            testObj4.Address = "abca-sadas.dsa";

            testObj4.SaveToJsonFile(path1);
            TestConfig testObj5 = path1.ReadJsonFileToObject<TestConfig>();

            string path2 = "data.xml";
            TestConfig testObj6 = new TestConfig();
            testObj6.IP = "127.0.0.4";
            testObj6.Number = 3306;
            testObj6.Name = "root5";
            testObj6.Address = "http://www.google.com";

            testObj6.SaveToXmlFile(path2);
            TestConfig testObj7 = path2.ReadXmlFileToObject<TestConfig>();
        }
    }
}
