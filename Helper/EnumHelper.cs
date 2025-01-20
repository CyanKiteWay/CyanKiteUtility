using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CyanKiteUtility
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举值的描述信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            string result = value.ToString();
            Type type = typeof(T);
            FieldInfo info = type.GetField(value.ToString());
            var attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes != null && attributes.FirstOrDefault() != null)
            {
                result = (attributes.First() as DescriptionAttribute).Description;
            }

            return result;
        }

        /// <summary>
        /// 获取枚举值的描述信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumItemInfo GetEnumItemInfo<T>(this T value) where T : struct
        {
            EnumItemInfo itemInfo = new EnumItemInfo() { Name = "", Value = -1, Description = "" };

            Type type = typeof(T);
            FieldInfo info = type.GetField(value.ToString());
            //不是枚举字段不处理
            if (info.FieldType.IsEnum)
            {
                itemInfo.Name = info.Name;
                
                //获取值
                itemInfo.Value = (int)type.InvokeMember(info.Name, BindingFlags.GetField, null, null, null);
                //获取注解
                Type typeDescription = typeof(DescriptionAttribute);
                DescriptionAttribute arr = info.GetCustomAttributes(typeDescription, true).FirstOrDefault() as DescriptionAttribute;
                itemInfo.Description = arr?.Description ?? "";
            }

            return itemInfo;
        }

        /// <summary>
        /// 根据枚举值类型获取枚举信息List
        /// </summary>
        /// <returns></returns>
        public static List<EnumItemInfo> GetListFromEnum<T>()
        {
            //反射 循环 获取数据
            Type t = typeof(T);
            List<EnumItemInfo> itemList = new List<EnumItemInfo>();
            FieldInfo[] fieldInfos = t.GetFields();
            foreach (var item in fieldInfos)
            {
                //不是枚举字段不处理
                if (item.FieldType.IsEnum)
                {
                    //名称可以直接获取
                    EnumItemInfo itemInfo = new EnumItemInfo()
                    {
                        Name = item.Name
                    };
                    //获取值
                    itemInfo.Value = (int)t.InvokeMember(item.Name, BindingFlags.GetField, null, null, null);
                    //获取注解
                    Type typeDescription = typeof(DescriptionAttribute);
                    DescriptionAttribute arr = item.GetCustomAttributes(typeDescription, true).FirstOrDefault() as DescriptionAttribute;
                    itemInfo.Description = arr?.Description ?? "";

                    itemList.Add(itemInfo);
                }
            }
            return itemList;
        }
    }

    [Serializable]
    public class EnumItemInfo
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
