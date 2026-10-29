using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExtenContent.Exten
{
    internal static class LoaderUtils
    {
        /// <summary>
        /// 调用类的静态方法
        /// </summary>
        public static void ResetStaticMembers(Type type)
        {
            ConstructorInfo init = type.TypeInitializer;
            if (init == null) return;

            init.Invoke(null, null);
        }

        /// <summary>
        /// 从程序集中加载对应类型<br/>
        /// 如果是<see langword="abstract"/>或<see cref="ILoadable.CanLoad"/>返回<see langword="false"/>则不加载<br/>
        /// 找不到返回<see langword="null"/>
        /// </summary>
        public static List<T> CreateInstance<T>(Assembly assembly) where T : ILoadable
        {
            Type[] type = assembly.GetTypes();

            List<Type> types = type.ToList().FindAll(t =>
            t.IsAbstract == false &&
            typeof(T).IsAssignableFrom(t));

            if (types.Count < 1) return null;

            List<T> instance = new List<T>();

            foreach (Type t in types)
            {
                T obj = (T)Activator.CreateInstance(t);
                if (obj.CanLoad() != true) continue;

                instance.Add(obj);
            }

            return instance;
        }
    }
}
