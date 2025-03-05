using System;
using System.Linq;
using System.Reflection;

namespace CyanKiteUtility
{
    public static class AttributeExtension
    {
        /// <summary>
        /// 针对类对象的某个属性获取它的某个属性
        /// </summary>
        /// <typeparam name="T">目标属性</typeparam>
        /// <param name="property">类对象的某个属性</param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this PropertyInfo property)
        {
            return (T)property.GetCustomAttributes(true).FirstOrDefault(a => typeof(T) == a.GetType());
        }

        /// <summary>
        /// 针对类型获取它的某个属性
        /// </summary>
        /// <typeparam name="T">目标属性</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this Type type)
        {
            return (T)type.GetCustomAttributes(true).FirstOrDefault(a => typeof(T) == a.GetType());
        }

        /// <summary>
        /// 针对枚举的某个枚举值获取它的某个属性
        /// </summary>
        /// <typeparam name="T">目标属性</typeparam>
        /// <param name="val">枚举的某个枚举值</param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this Enum val)
        {
            var type = val.GetType();
            var memberInfo = type.GetMember(val.ToString());
            return (T)memberInfo[0].GetCustomAttributes(false).FirstOrDefault(a => typeof(T) == a.GetType());
        }
    }
}
