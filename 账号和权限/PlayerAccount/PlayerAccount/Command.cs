using CommandHelp;
using PlayerAccount.Account;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount
{
    internal class Command : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> list = new List<CommandObject>();

            CommandObject root = new CommandObject("pa");
            root.SubCommand.Add(new CommandPrintList(root.SubCommand, "", ContentPatch.PrintTry));
            list.Add(root);

            CommandMethod save = new CommandMethod("save");
            save.Runing += _ => AccountData.SaveData();
            root.SubCommand.Add(save);

            return list;
        }
    }
}
