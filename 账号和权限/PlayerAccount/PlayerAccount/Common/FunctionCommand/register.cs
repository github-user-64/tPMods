using CommandHelp;
using ModTool.Command;
using PlayerAccount.Account;
using System;
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
            public cmd(Player player, Action<string> print) : base("register", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "密码", print));
                SubCommand.Add(new CommandString2());

                Runing += args =>
                {
                    if (args[0] is string pas == false)
                    {
                        print?.Invoke("参数错误");
                        return;
                    }

                    foo(player, pas, print);
                };
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        public static void foo(Player player, string password, Action<string> print)
        {
            if (password == null)
            {
                print?.Invoke("密码为空");
                return;
            }
            if (password.Length < 4)
            {
                print?.Invoke("密码长度小于4");
                return;
            }
            if (password.Length > 8)
            {
                print?.Invoke("密码长度大于8");
                return;
            }
            Regex regex = new Regex("[^0-9a-zA-Z\u4e00-\u9fa5]");
            if (regex.IsMatch(password))
            {
                print?.Invoke("密码不能有特殊字符");
                return;
            }

            string ex = player.Register(password);
            if (ex != null)
            {
                print?.Invoke($"注册失败:{ex}");
                return;
            }

            if (print == null) return;

            string text1 = $"{player.name}注册成功,密码是:[c/aaffaa:{password}]";
            string text2 = $"登录请输入/login [c/aaffaa:密码]";

            print(text1);
            print(text2);
        }
    }
}
