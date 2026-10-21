using ExtenContent.Extens;
using System;
using System.Linq;
using tContentPatch;
using tContentPatch.ModLoad;

namespace NewContents
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; protected set; } = null;

        public ThisMod()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.config.key == ExtenContent.ThisMod.ModKey);
            if (mo == null) throw new Exception($"找不到:{ExtenContent.ThisMod.ModName}");

            string targetVersion = "1.1";
            if (mo.config.version != targetVersion) throw new Exception($"NewContents的{ExtenContent.ThisMod.ModName}目标版本为:{targetVersion}");
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
