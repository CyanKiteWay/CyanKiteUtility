using CyanKiteUtility;
using Test.Model;

namespace Test.Test
{
    public class TestGetVariableNameHelper
    {
        public static void Test()
        {
            string TableName = "123";
            string nameOfTestVariable = GetVariableNameHelper.GetMemberName(() => TableName);
            Console.WriteLine(nameOfTestVariable);

            TestConfig testConfig = new TestConfig();
            nameOfTestVariable = GetVariableNameHelper.GetMemberName(() => testConfig.Number);
            Console.WriteLine(nameOfTestVariable);
        }
    }
}
