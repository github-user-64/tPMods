using CommandHelp;
using PlayerAccount.Account;
using PlayerAccount.Common.FunctionCommand;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount
{
    internal class ModCommand : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> list = new List<CommandObject>();

            CommandObject root = new CommandObject("pa");
            root.SubCommand.Add(new CommandPrintList(root.SubCommand,
                "save:保存账号数据, update:更新账号数据, acc:账号操作, print:发送消息到游戏", ContentPatch.PrintTry));
            list.Add(root);

            //保存
            CommandMethod save = new CommandMethod("save");
            save.Runing += _ =>
            {
                string msg = AccountFileHelp.BackupSaveData();
                ContentPatch.PrintTry(msg ?? "保存数据成功");
            };
            root.SubCommand.Add(save);

            //更新
            CommandMethod readAcc = new CommandMethod("update");
            readAcc.Runing += _ =>
            {
                ContentPatch.PrintTry(AccountFileHelp.UpdateData() ?? "更新数据成功");
            };
            root.SubCommand.Add(readAcc);

            //更新配置
            CommandMethod readConfig = new CommandMethod("updateConfig");
            readConfig.Runing += _ =>
            {
                ContentPatch.PrintTry(ServerConfig.Update() ? "更新服务器配置成功" : "更新服务器配置失败");
                ContentPatch.PrintTry(Common.SetChat.ChatConfig.instance.UpdateData() == true ? "更新聊天配置成功" : "更新聊天配置失败");
            };
            root.SubCommand.Add(readConfig);

            //踢出
            root.SubCommand.Add(new kick.cmd(ContentPatch.PrintTry));

            //封禁
            ban.cmd ban = new ban.cmd(ContentPatch.PrintTry);
            ban.SubCommand.Add(new banAdd.cmd(ContentPatch.PrintTry));
            ban.SubCommand.Add(new banDel.cmd(ContentPatch.PrintTry));
            root.SubCommand.Add(ban);

            //账号操作
            root.SubCommand.Add(new accAction.cmd(ContentPatch.PrintTry));

            //发送消息到游戏
            root.SubCommand.Add(new printToGame.cmd(ContentPatch.PrintTry));

            //禁言
            root.SubCommand.Add(noChat.GetYes(null, null, ContentPatch.PrintTry));
            root.SubCommand.Add(noChat.GetNo(null, null, ContentPatch.PrintTry));

            //打开关闭注册
            root.SubCommand.Add(new openRegister.cmd(ContentPatch.PrintTry));

            //添加服主账号
            root.SubCommand.Add(new addMan.cmd(ContentPatch.PrintTry));

            //将在线玩家账户设为管理员
            root.SubCommand.Add(new addPlayerAdmin.cmd(null, ContentPatch.PrintTry));

            return list;
        }
    }
}
