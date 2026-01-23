using CommandHelp;
using PlayerAccount.Common.FunctionCommand;
using PlayerAccount.Common.FunctionCommand.Acc;
using PlayerAccount.Common.FunctionCommand.Ban;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount
{
    internal class ModCommand : Mod
    {
        public override List<CommandObject> GetCommands()
        {
            List<CommandObject> list = new List<CommandObject>();

            list.Add(new UpdateConfig(ContentPatch.PrintTry));

            list.Add(new Acc(ContentPatch.PrintTry));

            //踢出
            list.Add(new Kick(ContentPatch.PrintTry));

            //封禁
            Ban ban = new Ban(ContentPatch.PrintTry);
            ban.SubCommand.Add(new BanAdd(ContentPatch.PrintTry));
            ban.SubCommand.Add(new BanDel(ContentPatch.PrintTry));
            list.Add(ban);

            //发送消息到游戏
            list.Add(new SendToGame(ContentPatch.PrintTry));

            //禁言
            list.Add(noChat.GetYes(null, null, ContentPatch.PrintTry));
            list.Add(noChat.GetNo(null, null, ContentPatch.PrintTry));

            //打开关闭注册
            list.Add(new EnableRegister(ContentPatch.PrintTry));

            return list;
        }
    }
}
