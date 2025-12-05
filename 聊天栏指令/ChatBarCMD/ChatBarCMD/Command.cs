using ChatBarCMD.Common.GameChatCommand;
using ChatBarCMD.Utils;
using CommandHelp;
using System.Collections.Generic;
using tContentPatch;

namespace ChatBarCMD
{
    internal class Command : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> cos = new List<CommandObject>();

            CommandObject root = new CommandObject("chatcmd");
            root.SubCommand.Add(new CommandPrintList(root.SubCommand, null, ContentPatch.PrintTry));

            root.SubCommand.Add(get1("enable", NetMode01.Enable, "单人和客户端启用指令"));

            root.SubCommand.Add(get2("head", NetMode01.Head, "单人和客户端指令头"));

            root.SubCommand.Add(get2("headToS", NetMode01.HeadToServer, "单人和客户端指令头,发送到服务端"));

            //

            root.SubCommand.Add(get1("sEnable", NetMode2.Enable, "服务端启用指令"));

            root.SubCommand.Add(get2("sHead", NetMode2.Head, "服务端指令头"));

            //

            root.SubCommand.Add(get1("enableTip", CommandTip.Enable, "启用指令提示"));

            //

            CommandMethod save = new CommandMethod("save");
            save.Runing += _ => Setting.SaveData();
            root.SubCommand.Add(save);

            cos.Add(root);
            return cos;
        }

        public static CommandObject get1(string texe, GetSetReset<bool> gsr, string tip = null)
        {
            return new CommandHRA<bool>(texe, gsr, tip, ContentPatch.PrintTry,
                new CommandTrue(), new CommandFalse());
        }

        public static CommandObject get2(string texe, GetSetReset<string> gsr, string tip = null)
        {
            return new CommandHRA<string>(texe, gsr, tip, ContentPatch.PrintTry,
                new CommandString());
        }
    }
}
