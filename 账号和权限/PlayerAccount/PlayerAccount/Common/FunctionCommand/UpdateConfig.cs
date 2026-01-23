using CommandHelp;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand
{
    internal class UpdateConfig : CommandMethod
    {
        private readonly Action<string> print = null;

        public UpdateConfig(Action<string> print = null) : base(CommandText.UpdateConfig)
        {
            this.print = print;
        }

        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            G(CommandText.Update, "更新指令文本");
            G(ServerConfig.Update, "更新服务器配置");
            G(SetChat.ChatConfig.instance.UpdateData, "更新聊天配置");

            return null;
        }

        private void G(Func<bool> fun, string s)
        {
            print?.Invoke($"{s}{(fun() ? "成功" : "失败")}");
        }
    }
}
