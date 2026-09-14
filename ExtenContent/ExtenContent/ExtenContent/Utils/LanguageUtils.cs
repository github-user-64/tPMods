using System;
using System.Reflection;
using Terraria.Localization;

namespace ExtenContent.Utils
{
    /// <summary/>
    public static class LanguageUtils
    {
        private static readonly MethodInfo SetValue = typeof(LocalizedText).GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// 获取指定<paramref name="key"/>的<see cref="LocalizedText"/>, 没有会先注册
        /// </summary>
        public static LocalizedText GetOrRegister(string key, string val)
        {
            LocalizedText lt = Language.GetText(key);
            if (lt != null) SetValue.Invoke(lt, new object[] { val });

            return lt;
        }

        /// <summary>
        /// <paramref name="key"/>为<paramref name="type"/>的命名空间加类名加<paramref name="key"/><para/>
        /// 获取指定<paramref name="key"/>的<see cref="LocalizedText"/>, 没有会先注册
        /// </summary>
        public static LocalizedText GetOrRegister(Type type, string key, string val)
        {
            return GetOrRegister($"{type.Namespace}.{type.Name}.{key}", val);
        }
    }
}
