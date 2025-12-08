using CommandHelp;
using ModTool.Command;
using PlayerAccount.Account;
using System;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 登录
    /// </summary>
    public class login
    {
        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary/>
            public cmd(Player player, Action<string> print) : base("login", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, print: print));
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
        /// 登录
        /// </summary>
        public static void foo(Player player, string password, Action<string> print)
        {
            string ex = player.Login(password);
            if (ex != null)
            {
                print?.Invoke($"登录失败:{ex}");
                return;
            }

            print?.Invoke($"{player.name}[c/00ff00:登录成功]");
        }
    }
}
