using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
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

                Runing += args =>
                {
                    if (args[0] is string n == false)
                    {
                        print?.Invoke("名称为null");
                        return;
                    }
                    if (args[1] is string p == false)
                    {
                        print?.Invoke("密码为null");
                        return;
                    }

                    string ex = foo(n, p) ?? $"{n}添加为服主并保存成功";
                    print?.Invoke(ex);
                };
            }
        }

        /// <summary>
        /// 添加服主账号并保存, 成功返回<see langword="null"/>
        /// </summary>
        public static string foo(string name, string password)
        {
            if (name == null) return "名称null";
            if (password == null) return "密码为null";
            if (password.Length < 4) return "密码长度小于4";

            Regex regex = new Regex("[^0-9a-zA-Z\u4e00-\u9fa5]");
            if (regex.IsMatch(password)) return "密码不能有特殊字符";

            string ex = AccountHelp.Register(name, password, out Dictionary<string, string> regOkAcc);
            if (ex != null) return $"注册失败:{ex}";

            regOkAcc.SetVal(AccountTag.AdminLevel, "0");

            string ex2 = AccountFileHelp.BackupSaveData();
            if (ex2 != null) return $"保存账号数据失败:{ex2}";

            return null;
        }
    }
}
