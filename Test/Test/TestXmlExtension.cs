using CyanKiteUtility;
using Test.Model;

namespace Test.Test
{
    public class TestXmlExtension
    {
        public static void Test()
        {
            TestConfig testObj1 = new TestConfig();
            testObj1.IP = "127.0.0.2";
            testObj1.Number = 3307;
            testObj1.Name = "root2";
            testObj1.Address = "23.365.256.89";

            TestConfig testObj2 = new TestConfig();
            testObj2.IP = "127.0.0.4";
            testObj2.Number = 3306;
            testObj2.Name = "root5";
            testObj2.Address = "http://www.google.com";

            string xmlstr = testObj1.ToXmlStr();
            TestConfig testObj3 = xmlstr.XmlStrToObject<TestConfig>();

            string path = "data.xml";
            testObj2.SaveToXmlFile(path);
            TestConfig testObj4 = path.ReadXmlFileToObject<TestConfig>();
        }
    }
}
