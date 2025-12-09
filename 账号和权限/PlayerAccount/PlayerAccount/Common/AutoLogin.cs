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
            //自动登录
            ModTool.PatchGame.PatchNetMessage.OnSyncConnectedPlayerPo += ply =>
            {
                if (ServerConfig.data.AutoLogin == false) return;
                if (Main.player?.IndexInRange(ply) != true) return;

                Player player = Main.player[ply];

                //获取账号
                Dictionary<string, string> acc = AccountHelp.GetNameAccount(player.name);
                if (acc == null) return;

                //获取玩家现在的数据
                var (name, ip, port, uuid, ex) = player.GetPlayerData();
                if (ex != null) return;

                //匹配数据
                if (acc.GetVal(AccountTag.Name) != name) return;
                if (ServerConfig.data.AutoLoginMatchIP && acc.GetVal(AccountTag.IP) != ip) return;
                if (ServerConfig.data.AutoLoginMatchPort && acc.GetVal(AccountTag.Port) != port.ToString()) return;
                if (ServerConfig.data.AutoLoginMatchUUID && acc.GetVal(AccountTag.UUID) != uuid) return;

                //登录
                player.Login(acc.GetVal(AccountTag.Password, null));

                ModTool.ServerHelp.PrintTo.PrintToPlay(ply, $"{name}[c/00ff00:登录成功]", Color.White);
            };
        }
    }
}
