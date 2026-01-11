using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using System;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 服务器发送消息到游戏
    /// </summary>
    public class printToGame
    {
        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary>
            /// 服务器发送消息到游戏
            /// </summary>
            public cmd(Action<string> print) : base("print", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "发送到全部, 发送到玩家", print));

                CommandObject a = new CommandObject("all");
                a.SubCommand.Add(new CommandString2());
                SubCommand.Add(a);

                CommandGetPlayer cgp = new CommandGetPlayer();
                cgp.SubCommand.Add(new CommandString2());
                SubCommand.Add(cgp);

                Runing += args =>
                {
                    string msg = $"Server:{args[1] as string}";

                    if (args[0] is Player p)
                    {
                        ModTool.ServerHelp.ToPlayerPrint.PrintToPlay(p.whoAmI, msg, Color.White);
                    }
                    else
                    {
                        ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll(msg, Color.White);
                    }
                };
            }
        }
    }
}
