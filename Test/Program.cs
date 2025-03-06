using System.Xml.Serialization;
using CyanKiteUtility;
using Newtonsoft.Json;
using Test.Test;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestGetVariableNameHelper.Test();
            return;
            TestDelegateExtension.Test();
            return;
            TestAttributeExtension.Test();
            return;
            TestJsonExtension.Test();
            return;
        }
    }

}
