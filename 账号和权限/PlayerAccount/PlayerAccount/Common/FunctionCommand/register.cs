using CommandHelp;
using ModTool.Command;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 注册
    /// </summary>
    public class register
    {
        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary/>
            public cmd(Player player, Dictionary<string, string> account, Action<string> print) : base("register", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "密码", print));
                SubCommand.Add(new CommandString2());

                Runing += args =>
                {
                    if (args[0] is string pas == false)
                    {
                        print?.Invoke("密码为null");
                        return;
                    }

                    string exmsg = foo(player, account, pas);
                    if (exmsg != null)
                    {
                        print?.Invoke(exmsg);
                        return;
                    }

                    if (print == null) return;

                    print($"{player.name}注册成功,密码是:[c/aaffaa:{pas}]");
                    print("登录请输入/login [c/aaffaa:密码]");
                };
            }
        }

        /// <summary>
        /// 注册, 成功返回<see langword="null"/>
        /// </summary>
        public static string foo(Player player, Dictionary<string, string> account, string password)
        {
            if (account != null) return "已登录无法注册";
            if (password == null) return "密码为空";
            if (password.Length < 4) return "密码长度小于4";
            if (password.Length > 8) return "密码长度大于8";

            Regex regex = new Regex("[^0-9a-zA-Z\u4e00-\u9fa5]");
            if (regex.IsMatch(password)) return "密码不能有特殊字符";

            string ex = player.RegisterPlayer(password);
            if (ex != null) return $"注册失败:{ex}";

            return null;
        }
    }
}
