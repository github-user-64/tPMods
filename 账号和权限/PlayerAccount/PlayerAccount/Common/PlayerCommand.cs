using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Common.FunctionCommand;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 玩家指令
    /// </summary>
    public class PlayerCommand : Mod
    {
        /// <summary>
        /// 获取指令(来自该玩家, 玩家账号)
        /// </summary>
        /// <param name="player">来自该玩家</param>
        /// <param name="account">已登录玩家账号</param>
        /// <param name="print"></param>
        public delegate List<CommandObject> GetCosDelegate(Player player, Dictionary<string, string> account, Action<string> print);
        /// <summary>
        /// 指令
        /// </summary>
        public static List<GetCosDelegate> CMD { get; private set; } = new List<GetCosDelegate>();

        /// <inheritdoc/>
        public override void Load()
        {
            CMD.Add(GetCMD);
            ChatBarCMD.Common.GameChatCommand.NetMode2.CMD.Add(GetChatCMD);
        }

        private List<CommandObject> GetChatCMD(int clientId, Action<string> print)
        {
            if (Main.player?.IndexInRange(clientId) != true) return null;

            Player player = Main.player[clientId];
            if (player == null) return null;

            Dictionary<string, string> acc = player.GetAccount();

            List<CommandObject> cos = new List<CommandObject>();

            CMD.RemoveAll(i => i == null);

            foreach (var i in CMD)
            {
                try
                {
                    List<CommandObject> c = i?.Invoke(player, acc, print);
                    if (c == null) continue;
                    cos.AddRange(c);
                }
                catch { }
            }

            return cos;
        }

        private List<CommandObject> GetCMD(Player player, Dictionary<string, string> account, Action<string> print)
        {
            if (Main.netMode != 2) return null;

            List<CommandObject> cos = new List<CommandObject>();

            if (ServerConfig.data.EnableRegister) cos.Add(new register.cmd(player, account, print));
            cos.Add(new login.cmd(player, account, print));
            cos.Add(new playing.cmd(player, print));

            bool isban = account.HasKey(AccountTag.Ban);//是封禁
            int? al = AccountHelp.GetAdminLevel(account);//管理等级

            if (isban == false && al != null)
            {
                cos.Add(new kick.cmd(player, account, print));
                ban.cmd ban = new ban.cmd(player, account, print);
                cos.Add(ban);

                if (al == 0)
                {
                    ban.SubCommand.Add(new banAdd.cmd(print));
                    ban.SubCommand.Add(new banDel.cmd(print));
                    cos.Add(new accAction.cmd(print));
                }

                cos.Add(noChat.GetYes(player, account, print));
                cos.Add(noChat.GetNo(player, account, print));

                cos.Add(new openRegister.cmd(print));
            }

            return cos;
        }
    }
}
