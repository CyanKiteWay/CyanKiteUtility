using CyanKiteUtility;
using Test.Model;

namespace Test.Test
{
    public class TestDelegateExtension
    {
        public static void Test()
        {
            TestDelegate testObj = new TestDelegate();
            testObj.Value1 = 20;
            testObj.TriggerDelegate("TestEventGetValue", testObj, new IntEventArgs() { ValueA = 24 });
            testObj.TriggerDelegate("TestGetValuePro", 9);
            testObj.TriggerDelegate("testGetValue", 18);

            // 清除事件和委托
            testObj.ClearDelegate("TestEventGetValue");
            testObj.ClearDelegate("TestGetValuePro");
            testObj.ClearDelegate("testGetValue");

            // 再次触发无效
            testObj.TriggerDelegate("TestEventGetValue", testObj, new IntEventArgs() { ValueA = 30 });
            testObj.TriggerDelegate("TestGetValuePro", 9);
            testObj.TriggerDelegate("testGetValue", 18);
            return;
        }
    }
}
