using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展实例
    /// </summary>
    public static class ExtenInstance<T> where T : IExtenType
    {
        /// <summary>
        /// 对应<typeparamref name="T"/>类型的实例
        /// </summary>
        public static T Instance { get; internal set; }
    }

    /// <summary>
    /// 扩展实例
    /// </summary>
    public static class ExtenInstance
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo> Instance = new ConcurrentDictionary<Type, PropertyInfo>();

        /// <summary>
        /// 注册<paramref name="instance"/>的实例
        /// </summary>
        public static void Register(IExtenType instance)
        {
            Register(instance.GetType(), instance);
        }

        /// <summary>
        /// 注册<paramref name="type"/>类型的实例
        /// </summary>
        public static void Register(Type type, IExtenType instance)
        {
            PropertyInfo property = Instance.GetOrAdd(type,
                typeof(ExtenInstance<>).MakeGenericType(type).GetProperty("Instance"));

            property.SetValue(null, instance);
        }
    }
}
