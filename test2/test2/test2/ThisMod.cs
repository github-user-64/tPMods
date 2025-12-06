using System.Collections.Generic;
using System.Linq;
using tContentPatch;
using tContentPatch.ModLoad;

namespace test2
{
    internal class ThisMod : Mod
    {
        public override void Load()
        {
            List<ModObject> mos = ContentPatch.GetModObjects();
            if (mos == null) return;

            ModObject mo1 = mos.FirstOrDefault(i => i.config.key == "StaticTile.ChatBarCMD");
            if (mo1 == null) return;

            ModObject mo2 = mos.FirstOrDefault(i => i.config.key == "StaticTile.ModTool");
            if (mo2 == null) return;

            MT.测试指令.Init();
        }
    }
}
