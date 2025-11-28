using System.Collections.Generic;
using System.Linq;
using tContentPatch;
using Terraria;

namespace PlayerAccount.ModLinkage
{
    public class ModChatBarCMD : Mod
    {
        public override void Loaded()
        {
            List<tContentPatch.ModLoad.ModObject> mos = ContentPatch.GetModObjects();
            if (mos == null) return;

            tContentPatch.ModLoad.ModObject mo = mos.FirstOrDefault(i => i.config.key == "StaticTile.ChatBarCMD");
            if (mo == null) return;

            Init();
        }

        private static void Init()
        {
            ChatBarCMD.Common.GameChatCommand.GameChatCommand.GameCMD.Add(() =>
            {
                if (Main.netMode != 0) return null;

                return Common.GameCMD.GetGameCO(s => Main.NewText(s));
            });
        }
    }
}
