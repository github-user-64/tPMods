using ReLogic.Content;
using System;
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
        internal static ModObject mo { get; private set; } = null;
        internal static IAssetRepository Asset { get; private set; } = null;

        internal static void Init(ModObject mo)
        {
            ThisMod.mo = mo;
            Asset = Utils.AssetUtils.GameAndExtenAssetRepository.GetAssets(ThisMod.mo);
        }

        internal static void Log(string s)
        {
            if (s == null) return;

            s = $"{ModName}:{s}";

            tContentPatch.ContentPatch.PrintTry(s);
            tContentPatch.Utils.Log.Add(s);
        }

        internal static void Log(out DateTime time, string s = null)
        {
            time = DateTime.Now;

            if (s != null) Log(s);
        }

        internal static void Log(string s, DateTime time)
        {
            TimeSpan len = DateTime.Now - time;

            Log($"{s}:{len.TotalSeconds}秒");
        }
    }
}
