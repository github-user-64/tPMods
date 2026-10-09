using System;
using System.Reflection;

namespace ExtenContent.Utils
{
    /// <summary/>
    public static class Utils
    {
        internal static void ResetStaticMembers(Type type)
        {
            ConstructorInfo init = type.TypeInitializer;
            if (init == null) return;

            init.Invoke(null, null);
        }

        internal static void LogPrint(string s)
        {
            if (s == null) return;

            s = $"扩展内容:{s}";

            tContentPatch.ContentPatch.PrintTry(s);
            tContentPatch.Utils.Log.Add(s);
        }
    }
}
