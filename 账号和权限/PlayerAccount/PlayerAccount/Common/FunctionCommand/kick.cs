using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
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
            /// <summary>
            /// 服务端踢出玩家
            /// </summary>
            public cmd(Action<string> print) : this(true, null, null, print)
            {

            }
            /// <summary>
            /// 玩家踢出玩家
            /// </summary>
            public cmd(Player formPlay, Dictionary<string, string> formAcc, Action<string> print) :
                this(false, formPlay, formAcc, print)
            {

            }

            /// <summary>
            /// 踢出来自服务器, 来自玩家, 来自玩家的账号, 输出
            /// </summary>
            public cmd(bool formServer, Player formPlay = null, Dictionary<string, string> formAcc = null,
                Action<string> print = null) : base("kick", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "\"完整玩家名\"或匹配玩家名或索引", print));

                CommandGetPlayer c_ply = new CommandGetPlayer();
                c_ply.SubCommand.Add(new CommandPrintList(c_ply.SubCommand, "踢出消息//可不填", print));
                SubCommand.Add(c_ply);

                CommandString2 c_msg = new CommandString2(true);
                c_ply.SubCommand.Add(c_msg);

                Runing += args =>
                {
                    if (args[0] is Player kickP == false)
                    {
                        print?.Invoke("参数错误");
                        return;
                    }
                    string kickM = args[1] as string;
                    
                    string ex = foo(kickP, formServer, formAcc, kickM);
                    if (ex == null)
                    {
                        ex = formServer ? "Server" : $"玩家{formPlay?.name}";
                        ex = $"{ex}踢出{kickP?.name}{(kickM == null ? null : $",原因是:{kickM}")}";

                        tContentPatch.ContentPatch.PrintTry(ex);
                        ModTool.ServerHelp.PrintTo.PrintToPlayAll(ex, Color.White);
                    }
                    else
                    {
                        print?.Invoke(ex);
                    }
                };
            }
        }

        /// <summary>
        /// 踢出玩家, 成功返回<see langword="null"/>
        /// </summary>
        public static string foo(Player kickPly, bool formServer, Dictionary<string, string> formAcc = null, string msg = null)
        {
            if (kickPly == null) return "玩家为null";
            if (formServer == false)
            {
                if (AccountHelp.MeasureAdminLevel(formAcc, kickPly.GetAccount()) == false) return "你的权限不足";
            }

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
