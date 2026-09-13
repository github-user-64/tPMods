using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ExtenContent.Extens
{
    public static class ExtenInstance<T> where T : IExtenType
    {
        public static T Instance { get; internal set; }
    }

    public static class ExtenInstance
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo> Instance = new ConcurrentDictionary<Type, PropertyInfo>();

        public static void Register(IExtenType instance)
        {
            Register(instance.GetType(), instance);
        }

        public static void Register(Type type, IExtenType instance)
        {
            PropertyInfo property = Instance.GetOrAdd(type,
                typeof(ExtenInstance<>).MakeGenericType(type).GetProperty("Instance"));

            property.SetValue(null, instance);
        }
    }
}
