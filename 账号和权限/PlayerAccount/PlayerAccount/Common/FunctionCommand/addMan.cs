using CommandHelp;
using ModTool.Command;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 添加服主
    /// </summary>
    public class addMan
    {
        /// <summary>
        /// 添加服主
        /// </summary>
        public class cmd : CommandMethod
        {
            /// <summary>
            /// 添加服主
            /// </summary>
            public cmd(Action<string> print) : base("添加服主", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "\"名称\"", print));

                CommandString name = new CommandString();
                name.SubCommand.Add(new CommandPrintList(name.SubCommand, "密码", print));
                name.SubCommand.Add(new CommandString2());
                SubCommand.Add(name);
            }

            /// <inheritdoc/>
            public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
            {
                return null;
            }
        }

        /// <summary>
        /// 注册, 成功返回<see langword="null"/>
        /// </summary>
        public static string foo(string name, string password)
        {
            if (password == null) return "密码为空";
            if (password.Length < 4) return "密码长度小于4";

            Regex regex = new Regex("[^0-9a-zA-Z\u4e00-\u9fa5]");
            if (regex.IsMatch(password)) return "密码不能有特殊字符";

            string ex = AccountHelp.Register(name, password, out Dictionary<string, string> regOkAcc);
            if (ex != null) return $"注册失败:{ex}";

            return null;
        }
    }
}
