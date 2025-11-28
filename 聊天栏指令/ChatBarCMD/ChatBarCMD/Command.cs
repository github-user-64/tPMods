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

            root.SubCommand.Add(new CommandHRA<bool>("enable", GameChatCommand.Enable, null, ContentPatch.PrintTry,
                new CommandTrue(), new CommandFalse()));

            root.SubCommand.Add(new CommandHRA<string>("head", GameChatCommand.CMDHead, null, ContentPatch.PrintTry,
                new CommandString()));

            root.SubCommand.Add(new CommandHRA<bool>("enableTip", CommandTip.Enable, null, ContentPatch.PrintTry,
                new CommandTrue(), new CommandFalse()));

            CommandMethod save = new CommandMethod("save");
            save.Runing += _ => Setting.SaveData();
            root.SubCommand.Add(save);

            cos.Add(root);
            return cos;
        }
    }
}
