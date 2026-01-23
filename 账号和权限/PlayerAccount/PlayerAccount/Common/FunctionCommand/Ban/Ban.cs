using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using ModTool.ServerHelp;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand.Ban
{
    /// <summary>
    /// 封禁
    /// </summary>
    public class Ban : CommandObject
    {
        /// <summary>
        /// 服务端封禁玩家
        /// </summary>
        public Ban(Action<string> print) : this(true, null, null, print)
        {

        }
        /// <summary>
        /// 玩家封禁玩家
        /// </summary>
        public Ban(Player formPlay, Dictionary<string, string> formAcc, Action<string> print) :
            this(false, formPlay, formAcc, print)
        {

        }

        /// <summary>
        /// 封禁来自服务器, 来自玩家, 来自玩家的账号, 输出
        /// </summary>
        public Ban(bool formServer, Player formPlay = null, Dictionary<string, string> formAcc = null,
            Action<string> print = null) : base(CommandText.Ban)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandMethod ply = new CommandMethod(CommandText.BanPly, 2);
            ply.SubCommand.Add(new CommandPrintList(ply.SubCommand, null, print));
            SubCommand.Add(ply);

            CommandGetPlayer ban_ply = new CommandGetPlayer();
            ban_ply.SubCommand.Add(new CommandPrintList(ban_ply.SubCommand, "封禁消息//可不填", print));
            ply.SubCommand.Add(ban_ply);

            CommandString2 ban_msg = new CommandString2(true);
            ban_ply.SubCommand.Add(ban_msg);

            ply.Runing += args =>
            {
                if (args[0] is Player kickP == false)
                {
                    print?.Invoke("玩家为null");
                    return;
                }
                string kickM = args[1] as string;

                string ex = BanPlayer(kickP, formServer, formAcc, kickM);
                if (ex == null)
                {
                    ex = formServer ? "Server" : $"玩家{formPlay?.name}";
                    ex = $"{ex}封禁{kickP?.name}{(kickM == null ? null : $",原因是:{kickM}")}";

                    tContentPatch.ContentPatch.PrintTry(ex);
                    ToPlayerPrint.PrintToPlayAll(ex, Color.White, kickP?.whoAmI ?? -1);
                }
                else
                {
                    print?.Invoke(ex);
                }
            };
        }

        /// <summary>
        /// 封禁玩家, 成功返回<see langword="null"/>
        /// </summary>
        public static string BanPlayer(Player banPly, bool formServer, Dictionary<string, string> formAcc = null, string msg = null)
        {
            if (banPly == null) return "玩家为null";
            if (formServer == false)
            {
                if (AccountHelp.MeasureAdminLevel(formAcc, banPly.GetAccount()) == false) return "你的权限不足";
            }

            string omsg = msg;
            if (msg == null)
            {
                msg = "你被封禁";
            }
            else
            {
                msg = $"你被封禁:{msg}";
            }

            if (msg?.Length > 30 == true) msg = $"{msg.Substring(0, 30)}...";

            KickPlay.Kick(banPly.whoAmI, msg);

            return AccountHelp.BanAddPlayer(banPly, omsg);
        }
    }
}
