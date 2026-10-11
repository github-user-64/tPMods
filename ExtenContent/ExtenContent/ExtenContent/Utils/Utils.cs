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
    }
}
