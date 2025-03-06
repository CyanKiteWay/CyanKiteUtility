using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;

namespace CyanKiteUtility
{
    public static class AttributeExtension
    {
        /// <summary>
        /// 针对类对象的某个属性获取它的某个特征
        /// </summary>
        /// <typeparam name="attribute">指定的目标特征</typeparam>
        /// <param name="property">类对象的某个属性</param>
        /// <returns></returns>
        public static attribute GetTargetAttribute<attribute>(this PropertyInfo property) where attribute : Attribute
        {
            return property.GetCustomAttribute<attribute>();
        }

        /// <summary>
        /// 针对类类型获取它的指定特征
        /// </summary>
        /// <typeparam name="attribute">指定的目标特征</typeparam>
        /// <param name="type">类类型</param>
        /// <returns></returns>
        public static attribute GetTargetAttribute<attribute>(this Type type) where attribute : Attribute
        {
            return type.GetCustomAttribute<attribute>();
        }

        /// <summary>
        /// 针对类类型获取所有包含指定特征的属性
        /// </summary>
        /// <typeparam name="attribute">指定的目标特征</typeparam>
        /// <param name="type">类类型</param>
        /// <returns></returns>
        public static Dictionary<PropertyInfo, attribute> GetAllPropertyContainTargetAttribute<attribute>(this Type type) where attribute : Attribute
        {
            Dictionary<PropertyInfo, attribute> result = new Dictionary<PropertyInfo, attribute>();
            PropertyInfo[] propertys = type.GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                var result_attribute = property.GetCustomAttribute<attribute>();
                if (result_attribute != null)
                {
                    result.Add(property, result_attribute);
                }
            }
            return result;
        }

        /// <summary>
        /// 针对枚举的某个枚举值获取它的指定特征
        /// </summary>
        /// <typeparam name="attribute">指定的目标特征</typeparam>
        /// <param name="val">枚举的某个枚举值</param>
        /// <returns></returns>
        public static attribute GetTargetAttribute<attribute>(this Enum val) where attribute : Attribute
        {
            var type = val.GetType();
            FieldInfo info = type.GetField(val.ToString());
            return info.GetCustomAttribute<attribute>();
        }

        /// <summary>
        /// 针对枚举的某个枚举值获取它的自定义信息
        /// </summary>
        /// <typeparam name="attribute">指定的目标特征</typeparam>
        /// <param name="value">枚举的某个枚举值</param>
        /// <returns></returns>
        public static EnumItemInfo GetEnumItemInfo(this Enum val)
        {
            EnumItemInfo itemInfo = new EnumItemInfo();
            var type = val.GetType();
            FieldInfo info = type.GetField(val.ToString());
            //不是枚举字段不处理
            if (info.FieldType.IsEnum)
            {
                itemInfo.Name = info.Name;

                //获取值
                itemInfo.Value = (int)type.InvokeMember(info.Name, BindingFlags.GetField, null, null, null);
                //获取注解
                DescriptionAttribute arr = info.GetCustomAttribute<DescriptionAttribute>();
                itemInfo.Info = arr?.Description ?? "";
            }

            return itemInfo;
        }

        /// <summary>
        /// 针对枚举类型获取枚举内所有值的Description信息
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<EnumItemInfo> GetAllDescriptionInfo(this Type type)
        {
            List<EnumItemInfo> infoList = new List<EnumItemInfo>();
            FieldInfo[] fieldInfos = type.GetFields();
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
                    itemInfo.Value = (int)type.InvokeMember(item.Name, BindingFlags.GetField, null, null, null);
                    //获取注解
                    DescriptionAttribute arr = item.GetCustomAttribute<DescriptionAttribute>();
                    itemInfo.Info = arr?.Description ?? "";

                    infoList.Add(itemInfo);
                }
            }
            return infoList;
        }
    }

    /// <summary>
    /// 自定义的枚举值信息类
    /// </summary>
    [Serializable]
    public class EnumItemInfo
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public EnumItemInfo() 
        {
            Name = "";
            Value = -1;
            Info = "";
        }
    }
}
