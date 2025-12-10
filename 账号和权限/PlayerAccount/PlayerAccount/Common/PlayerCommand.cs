using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
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

        private List<CommandObject> GetCMD(Player player, Dictionary<string, string> account, Action<string> print)
        {
            if (Main.netMode != 2) return null;

            List<CommandObject> cos = new List<CommandObject>
            {
                new FunctionCommand.register.cmd(player, print),
                new FunctionCommand.login.cmd(player, account, print),
                new FunctionCommand.playing.cmd(player, print)
            };

            bool isban = account.HasKey(AccountTag.Ban);
            bool canSetTag = account.HasKey(AccountTag.CanSetAccTag);

            if (isban == false && account.HasKey(AccountTag.Administrator))
            {
                cos.Add(new FunctionCommand.kick.cmd(false, player, account, print));
            }

            return cos;
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
    }
}
