using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TestCustomPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Used { get; set; }
        public int Number { get; set; }

        public TestCustomPropertyAttribute(string name, bool used = true, int number = 0)
        {
            Name = name;
            Used = used;
            Number = number;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TestCustomClassAttribute : Attribute
    {

        public TestCustomClassAttribute(byte attributeId)
        {
            AttributeId = attributeId;
        }

        public byte AttributeId { get; private set; }

    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TestCustomEnumAttribute : Attribute
    {

        public TestCustomEnumAttribute(byte attributeId)
        {
            AttributeId = attributeId;
        }

        public byte AttributeId { get; private set; }

    }


    [TestCustomClass(3)]
    public class TestClassAttributeClass
    {
        public string TestProperty1 { get; set; }
        public string TestProperty2 { get; set; }
        public string TestProperty3 { get; set; }
    }

    public class TestPropertyAttributeClass
    {
        [TestCustomProperty("TestName1")]
        public string TestProperty1 { get; set; }
        [TestCustomProperty("TestName2")]
        public string TestProperty2 { get; set; }
        [TestCustomProperty("TestName3")]
        public string TestProperty3 { get; set; }
    }

    public enum TestEnumAttributeEnum
    {
        [TestCustomEnum(1)]
        [Display(Name = "Display_Name")]
        EnumValue1,
        [TestCustomEnum(1)]
        [Description("Description_Name")]
        EnumValue2,
        [TestCustomEnum(1)]
        EnumValue3
    }

}
