using Microsoft.Xna.Framework;
using ModTool.Utils;
using PlayerAccount.Account;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 自动登录
    /// </summary>
    internal static class AutoLogin
    {
        public static bool Login(int ply)
        {
            if (Main.player?.IndexInRange(ply) != true) return false;

            Player player = Main.player[ply];

            //获取账号
            Dictionary<string, string> acc = AccountHelp.GetNameAccount(player.name);
            if (acc == null) return false;

            //获取玩家现在的数据
            var (name, ip, port, uuid, ex) = player.GetPlayerData();
            if (ex != null) return false;

            //匹配数据
            if (acc.GetVal(AccountTag.Name) != name) return false;
            if (ServerConfig.data.AutoLoginMatchIP && acc.GetVal(AccountTag.IP) != ip) return false;
            if (ServerConfig.data.AutoLoginMatchPort && acc.GetVal(AccountTag.Port) != port.ToString()) return false;
            if (ServerConfig.data.AutoLoginMatchUUID && acc.GetVal(AccountTag.UUID) != uuid) return false;

            //登录
            player.Login(acc.GetVal(AccountTag.Password, null));

            ModTool.ServerHelp.PrintTo.PrintToPlay(ply, $"{name}[c/00ff00:登录成功]", Color.White);

            return true;
        }
    }
}
