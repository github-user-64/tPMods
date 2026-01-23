using CommandHelp;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Common.FunctionCommand
{
    internal class UpdateConfig : CommandMethod
    {
        public UpdateConfig() : base(CommandText.UpdateConfig) { }

        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            G(CommandText.Update, "更新指令文本");
            G(ServerConfig.Update, "更新服务器配置");
            G(SetChat.ChatConfig.instance.UpdateData, "更新聊天配置");

            return null;
        }

        private static void G(Func<bool> fun, string s)
        {
            ContentPatch.PrintTry($"{s}{(fun() ? "成功" : "失败")}");
        }
    }
}
