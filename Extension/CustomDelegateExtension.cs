using System;
using System.Reflection;

namespace CyanKiteUtility
{
    public static class CustomDelegateExtension
    {
        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        public static void ClearEvent(this object objectHasEvents, string eventName)
        {
            if (objectHasEvents == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(eventName))
            {
                return;
            }

            try
            {
                BindingFlags mFieldFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;

                var fieldInfo = objectHasEvents.GetType().GetField(eventName, mFieldFlags);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(objectHasEvents, null);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //try
            //{
            //    BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static;
            //    EventInfo eventInfo = objectHasEvents.GetType().GetEvent(eventName, bindingFlags);
            //    if (eventInfo == null)
            //    {
            //        return;
            //    }

            //    /********************************************************
            //         * class的每个event都对应了一个同名或前面加了Event前缀的private的delegate类型成员变量
            //         * （这点可以用Reflector证实）
            //         * 因为private成员变量无法在基类中进行修改，
            //         * 所以为了能够拿到base class中声明的事件，要从EventInfo的DeclaringType来获取
            //         * event对应的成员变量的FieldInfo并进行修改
            //         ********************************************************/
            //    FieldInfo fieldInfo = eventInfo.DeclaringType.GetField(eventInfo.Name, bindingFlags);
            //    if (fieldInfo == null)
            //    {
            //        fieldInfo = eventInfo.DeclaringType.GetField("Event_" + eventName, bindingFlags);
            //    }
            //    if (fieldInfo != null)
            //    {
            //        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
            //        fieldInfo.SetValue(objectHasEvents, null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// 手动触发指定委托（自定义委托和事件）
        /// </summary>
        /// <param name="objectHasDelegates">对象</param>
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
