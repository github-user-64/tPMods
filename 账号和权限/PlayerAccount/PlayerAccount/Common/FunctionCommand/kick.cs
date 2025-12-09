using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 踢出
    /// </summary>
    public class kick
    {
        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary/>
            public cmd(Player player, Dictionary<string, string> account, Action<string> print) : base("kick", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "\"完整玩家名\"或匹配玩家名或索引", print));

                CommandGetPlayer c_ply = new CommandGetPlayer();
                c_ply.SubCommand.Add(new CommandPrintList(c_ply.SubCommand, "踢出消息//可不填", print));
                SubCommand.Add(c_ply);

                CommandString2 c_msg = new CommandString2(true);
                c_ply.SubCommand.Add(c_msg);

                Runing += args =>
                {
                    if (args[0] is Player ply == false)
                    {
                        print?.Invoke("参数错误");
                        return;
                    }
                    string msg = args[1] as string;
                    
                    string ex = foo(player, account, ply, msg);
                    if (ex == null) ex = $"{player?.name}踢出{ply?.name}{(msg == null ? null : $",原因是:{msg}")}";

                    ModTool.ServerHelp.PrintTo.PrintToPlayAll(ex, Color.White);
                };
            }
        }

        /// <summary>
        /// 踢出玩家, 成功返回<see langword="null"/>
        /// </summary>
        public static string foo(Player player, Dictionary<string, string> account, Player kickPly, string msg = null)
        {
            if (player == null) return "无法判断你的身份";
            if (account == null) return "你未登录";
            if (account.HasKey(AccountTag.Administrator) == false) return "你没有权限";

            if (msg == null)
            {
                msg = "踢出";
            }
            else
            {
                msg = $"踢出:{msg}";
            }

            if (msg?.Length > 30 == true) msg = $"{msg.Substring(0, 30)}...";

            return ModTool.ServerHelp.KickPlay.Kick(kickPly.whoAmI, msg);
        }
    }
}
