using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Common.FunctionCommand;
using PlayerAccount.Common.FunctionCommand.Acc;
using PlayerAccount.Common.FunctionCommand.Ban;
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
        private static readonly List<GetCosDelegate> _Cmd = new List<GetCosDelegate>();
        /// <summary>
        /// 指令
        /// </summary>
        public static event GetCosDelegate Cmd
        {
            add
            {
                if (value != null) _Cmd.Add(value);
            }
            remove => _Cmd.Remove(value);
        }

        /// <inheritdoc/>
        public override void Load()
        {
            Cmd += GetServerCmd;
            ChatBarCMD.Common.GameChatCommand.NetMode2.CMD.Add(GetChatCmd);
        }

        private List<CommandObject> GetChatCmd(int clientId, Action<string> print)
        {
            if (Main.player?.IndexInRange(clientId) != true) return null;

            Player player = Main.player[clientId];
            if (player == null) return null;

            Dictionary<string, string> acc = player.GetAccount();

            List<CommandObject> cos = new List<CommandObject>();

            foreach (var i in _Cmd)
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

        private List<CommandObject> GetServerCmd(Player player, Dictionary<string, string> account, Action<string> print)
        {
            if (Main.dedServ == false) return null;

            List<CommandObject> cos = new List<CommandObject>();

            if (ServerConfig.data.EnableRegister) cos.Add(new Register(player, account, print));
            cos.Add(new Login(player, account, print));
            cos.Add(new Playing(print));

            bool isban = account.HasKey(AccountTag.Ban);//是封禁
            int? al = AccountHelp.GetAdminLevel(account);//管理等级

            if (isban == false && al != null)
            {
                cos.Add(new Kick(player, account, print));

                Ban ban = new Ban(player, account, print);
                cos.Add(ban);

                cos.Add(noChat.GetYes(player, account, print));
                cos.Add(noChat.GetNo(player, account, print));

                cos.Add(new EnableRegister(print));

                if (al == 0)//服主
                {
                    cos.Add(new Acc(print));

                    ban.SubCommand.Add(new BanAdd(print));
                    ban.SubCommand.Add(new BanDel(print));
                }
            }

            return cos;
        }
    }
}
