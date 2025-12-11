using CommandHelp;
using PlayerAccount.Account;
using PlayerAccount.Common.FunctionCommand;
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
            root.SubCommand.Add(new CommandPrintList(root.SubCommand, "保存账号, 更新账号, 更新服务器配置, 踢出玩家", ContentPatch.PrintTry));
            list.Add(root);

            CommandMethod save = new CommandMethod("save");
            save.Runing += _ =>
            {
                string msg = AccountFileHelp.BackupSaveData();
                ContentPatch.PrintTry(msg ?? "保存数据成功");
            };
            root.SubCommand.Add(save);

            CommandMethod readAcc = new CommandMethod("update");
            readAcc.Runing += _ =>
            {
                ContentPatch.PrintTry(AccountFileHelp.UpdateData() ?? "更新数据成功");
            };
            root.SubCommand.Add(readAcc);

            CommandMethod readConfig = new CommandMethod("updateConfig");
            readConfig.Runing += _ =>
            {
                ContentPatch.PrintTry(ServerConfig.Update() ? "更新配置成功" : "更新配置失败");
            };
            root.SubCommand.Add(readConfig);

            root.SubCommand.Add(new kick.cmd(true, print: ContentPatch.PrintTry));

            return list;
        }
    }
}
