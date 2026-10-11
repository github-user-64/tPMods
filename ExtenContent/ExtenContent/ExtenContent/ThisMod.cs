using tContentPatch.ModLoad;

namespace ExtenContent
{
    /// <summary/>
    public static class ThisMod
    {
        /// <summary/>
        public const string ModKey = "StaticTile.ExtenContent";
        /// <summary/>
        public const string ModName = "扩展内容";
        internal static ModObject mo = null;

        internal static void LogPrint(string s)
        {
            if (s == null) return;

            s = $"{ModName}:{s}";

            tContentPatch.ContentPatch.PrintTry(s);
            tContentPatch.Utils.Log.Add(s);
        }
    }
}
