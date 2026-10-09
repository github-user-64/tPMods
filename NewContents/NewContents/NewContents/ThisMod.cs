using ExtenContent.Extens;
using System;
using tContentPatch;
using tContentPatch.ModLoad;

namespace NewContents
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; protected set; } = null;

        public ThisMod()
        {
            string targetVersion = "1-beta16-t1.4.5.8";
            if (ContentPatch.VersionTPlainModLoader != targetVersion) throw new Exception($"扩展内容的目标tPlainModLoader版本为:{targetVersion}");
        }

        public override void Load(ModObject mo)
        {
            ThisMod.mo = mo;

            RegistExten(ThisMod.mo);
        }

        protected static void RegistExten(ModObject mo)
        {
            ExtenManag.RegistMod(mo);
        }
    }
}
