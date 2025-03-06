using System;
using System.Reflection;

namespace CyanKiteUtility
{
    public static class DelegateExtension
    {
        /// <summary>
        /// 清除一个对象的某个委托或事件所挂钩的函数
        /// </summary>
        /// <param name="objectHasDelegates">包含委托的对象</param>
        /// <param name="delegateName">委托名称</param>
        public static void ClearDelegate(this object objectHasDelegates, string delegateName)
        {
            if (objectHasDelegates == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(delegateName))
            {
                return;
            }

            try
            {
                BindingFlags mFieldFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;

                var fieldInfo = objectHasDelegates.GetType().GetField(delegateName, mFieldFlags);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(objectHasDelegates, null);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 手动触发指定委托（自定义委托和事件）
        /// </summary>
        /// <param name="objectHasDelegates">包含委托的对象</param>
        /// <param name="delegateName">委托名称</param>
        /// <param name="args">委托的参数</param>
        public static void TriggerDelegate(this object objectHasDelegates, string delegateName, params object[] args)
        {
            if (objectHasDelegates == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(delegateName))
            {
                return;
            }
            try
            {
                BindingFlags mFieldFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;
                
                var fieldInfo = objectHasDelegates.GetType().GetField(delegateName, mFieldFlags);
                if (fieldInfo != null)
                {
                    var newDelegate = fieldInfo.GetValue(objectHasDelegates) as Delegate;
                    if (newDelegate != null)
                    {
                        var ali = newDelegate.GetInvocationList();
                        newDelegate.DynamicInvoke(args);
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
