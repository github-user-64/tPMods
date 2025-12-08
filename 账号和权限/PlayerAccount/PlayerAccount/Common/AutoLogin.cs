using Microsoft.Xna.Framework;
using ModTool.Utils;
using PlayerAccount.Account;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace PlayerAccount.Common
{
    internal class AutoLogin : Mod
    {
        public override void Load()
        {
            ModTool.PatchGame.PatchNetMessage.OnSyncConnectedPlayerPo += ply =>
            {
                if (Main.player?.IndexInRange(ply) != true) return;

                Player player = Main.player[ply];

                Dictionary<string, string> acc = player.GetAccount();
                if (acc == null) return;

                var (name, ip, port, uuid, ex) = player.GetPlayerData();
                if (ex != null) return;

                if (acc.GetVal(AccountTag.Name) != name) return;
                if (acc.GetVal(AccountTag.IP) != ip) return;
                if (acc.GetVal(AccountTag.Port) != port.ToString()) return;
                if (acc.GetVal(AccountTag.UUID) != uuid) return;

                player.Login(acc.GetVal(AccountTag.Password, null));

                ModTool.ServerHelp.PrintTo.PrintToPlay(ply, $"{name}[c/00ff00:登录成功]", Color.White);
            };
        }
    }
}
