using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using ModTool.ServerHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 禁言
    /// </summary>
    public class noChat
    {
        /// <summary/>
        public class class1 : CommandMethod
        {
            /// <summary/>
            public Action<Player, Dictionary<string, string>> OnAction = null;

            /// <summary/>
            public class1(Player player, Dictionary<string, string> acc, string text, Action<string> print) : base(text, 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, null, print));
                SubCommand.Add(new CommandGetPlayer());

                Runing += args =>
                {
                    if (args[0] is Player noPlayer == false)
                    {
                        print?.Invoke("玩家为null");
                        return;
                    }

                    Dictionary<string, string> noAcc = noPlayer.GetAccount();
                    if (noAcc == null)
                    {
                        print?.Invoke("该玩家未登录");
                        return;
                    }

                    if (player != null && AccountHelp.MeasureAdminLevel(acc, noAcc) == false)
                    {
                        print?.Invoke("你的权限不足");
                        return;
                    }

                    OnAction?.Invoke(noPlayer, noAcc);
                };
            }
        }

        /// <summary>
        /// 获取禁言指令
        /// </summary>
        public static CommandObject GetYes(Player player, Dictionary<string, string> acc, Action<string> print)
        {
            class1 c = new class1(player, acc, "禁言", print);
            c.OnAction += (noP, noAcc) =>
            {
                string msg = $"{(player == null ? "Server" : $"玩家{player.name}")}禁言{noP.name}";

                noAcc.SetVal("禁言", msg);

                print?.Invoke(msg);
                PrintTo.PrintToPlayAll(msg, Color.Red);
            };

            return c;
        }

        /// <summary>
        /// 获取取消禁言指令
        /// </summary>
        public static CommandObject GetNo(Player player, Dictionary<string, string> acc, Action<string> print)
        {
            class1 c = new class1(player, acc, "取消禁言", print);
            c.OnAction += (noP, noAcc) =>
            {
                string msg = $"{(player == null ? "Server" : $"玩家{player.name}")}取消禁言{noP.name}";

                noAcc.DelKey("禁言");

                print?.Invoke(msg);
                PrintTo.PrintToPlayAll(msg, Color.Green);
            };

            return c;
        }
    }
}
