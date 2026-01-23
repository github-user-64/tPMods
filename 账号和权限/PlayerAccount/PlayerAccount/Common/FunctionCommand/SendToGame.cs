using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using ModTool.ServerHelp;
using System;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 服务器发送消息到游戏
    /// </summary>
    public class SendToGame : CommandMethod
    {
        /// <summary>
        /// 服务器发送消息到游戏
        /// </summary>
        public SendToGame(Action<string> print) : base(CommandText.SendToGame, 2)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandObject all = new CommandObject(CommandText.SendToGameAll);
            all.SubCommand.Add(new CommandString2());
            SubCommand.Add(all);

            CommandGetPlayer ply = new CommandGetPlayer();
            ply.SubCommand.Add(new CommandString2());
            SubCommand.Add(ply);

            Runing += args =>
            {
                string msg = $"Server:{args[1] as string}";

                if (args[0] is Player p)
                {
                    ToPlayerPrint.PrintToPlay(p.whoAmI, msg, Color.White);
                }
                else
                {
                    ToPlayerPrint.PrintToPlayAll(msg, Color.White);
                }
            };
        }
    }
}
