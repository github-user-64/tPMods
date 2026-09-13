using System;
using System.Reflection;
using Terraria.Localization;

namespace ExtenContent.Utils
{
    public static class LanguageUtils
    {
        private static readonly MethodInfo SetValue = typeof(LocalizedText).GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);

        public static LocalizedText GetOrRegister(string key, string val)
        {
            LocalizedText lt = Language.GetText(key);
            if (lt != null) SetValue.Invoke(lt, new object[] { val });

            return lt;
        }

        public static LocalizedText GetOrRegister(Type type, string key, string val)
        {
            return GetOrRegister($"{type.Namespace}.{type.Name}.{key}", val);
        }
    }
}
