using CommandHelp;
using ModTool.Command;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary/>
    public class Login : CommandMethod
    {
        /// <summary>
        /// 登录
        /// </summary>
        public Login(Player player, Dictionary<string, string> account, Action<string> print) : base(CommandText.Login, 1)
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

                string ex = PlayerLogin(player, account, pas);
                if (ex == null) ex = $"{player?.name}[c/00ff00:登录成功]";

                print?.Invoke(ex);
            };
        }

        /// <summary>
        /// 登录, 成功返回<see langword="null"/>
        /// </summary>
        public static string PlayerLogin(Player player, Dictionary<string, string> account, string password)
        {
            if (account != null) return "不能重复登录";

            string ex = player.Login(password);
            if (ex != null) return $"登录失败:{ex}";

            return null;
        }
    }
}
