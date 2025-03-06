using CyanKiteUtility;
using Test.Model;

namespace Test.Test
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

            TestConfig testObj2 = new TestConfig();
            testObj2.IP = "127.0.0.3";
            testObj2.Number = 3305;
            testObj2.Name = "root3";
            testObj2.Address = "abca-sadas.dsa";

            string jsonstr = testObj1.ToJsonStr();
            TestConfig testObj3 = jsonstr.JsonStrToObject<TestConfig>();

            string path = "data.json";
            testObj2.SaveToJsonFile(path);
            TestConfig testObj4 = path.ReadJsonFileToObject<TestConfig>();
        }
    }
}
