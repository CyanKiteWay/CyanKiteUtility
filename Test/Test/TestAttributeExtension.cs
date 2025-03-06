using CyanKiteUtility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Test.Model;

namespace Test.Test
{
    public class TestAttributeExtension
    {
        public static void Test()
        {
            TestPropertyAttributeClass testObj1 = new TestPropertyAttributeClass();
            PropertyInfo[] propertys = testObj1.GetType().GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                var result_attribute1 = property.GetTargetAttribute<TestCustomPropertyAttribute>();
            }

            var attribute_dic = typeof(TestPropertyAttributeClass).GetAllPropertyContainTargetAttribute<TestCustomPropertyAttribute>();


            var result_attribute2 = typeof(TestClassAttributeClass).GetTargetAttribute<TestCustomClassAttribute>();

            var result_attribute3 = TestEnumAttributeEnum.EnumValue3.GetTargetAttribute<TestCustomEnumAttribute>();
            var result_attribute4 = TestEnumAttributeEnum.EnumValue1.GetTargetAttribute<DisplayAttribute>();
            var result_attribute5 = TestEnumAttributeEnum.EnumValue2.GetTargetAttribute<DescriptionAttribute>();

            var result6 = TestEnumAttributeEnum.EnumValue2.GetEnumItemInfo();

            

            var info_list = typeof(TestEnumAttributeEnum).GetAllDescriptionInfo();
        }
    }
}
