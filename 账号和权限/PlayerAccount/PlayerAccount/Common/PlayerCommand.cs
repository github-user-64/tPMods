using CommandHelp;
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
        /// <param name="account">玩家账号</param>
        public delegate List<CommandObject> GetCosDelegate(Player player, Dictionary<string, string> account);
        /// <summary>
        /// 指令
        /// </summary>
        public static List<GetCosDelegate> CMD { get; private set; } = new List<GetCosDelegate>();

        /// <inheritdoc/>
        public override void Load()
        {
            ChatBarCMD.Common.GameChatCommand.NetMode2.CMD.Add(GetCMD);
        }

        private List<CommandObject> GetCMD(int clientId, Action<string> print)
        {
            if (Main.player?.IndexInRange(clientId) != true) return null;

            Player player = Main.player[clientId];
            if (player == null) return null;

            Dictionary<string, string> acc = player.GetAccount();

            List<CommandObject> cos = new List<CommandObject>();

            cos.Add(new FunctionCommand.register.cmd(player, print));
            cos.Add(new FunctionCommand.login.cmd(player, print));
            cos.Add(new FunctionCommand.playing.cmd(player, print));

            CMD.RemoveAll(i => i == null);

            foreach (var i in CMD)
            {
                try
                {
                    List<CommandObject> c = i?.Invoke(player, acc);
                    if (c == null) continue;
                    cos.AddRange(c);
                }
                catch { }
            }

            return cos;
        }
    }
}
