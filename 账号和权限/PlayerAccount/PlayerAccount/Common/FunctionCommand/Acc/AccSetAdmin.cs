using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary/>
    public class AccSetAdmin : CommandMethod
    {
        /// <summary>
        /// 将账号设为管理员, <paramref name="player"/>为空视为服务端
        /// </summary>
        public AccSetAdmin(Player player = null, Action<string> print = null) : base(CommandText.AccSetAdmin, 2)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandGetAcc getAcc = new CommandGetAcc();
            SubCommand.Add(getAcc);

            getAcc.SubCommand.Add(new CommandPrintList(getAcc.SubCommand, "管理等级,注意0是服主级别可以修改账号,值越大级别越小,不填默认1", print));
            getAcc.SubCommand.Add(new CommandAdminLevel(true));

            Runing += args =>
            {
                string msg = SetAccAdminLevel(args[0], args[1], out Dictionary<string, string> acc);
                
                if (msg == null)
                {
                    msg = $"{(player == null ? "Server" : player.name)}";
                    msg += $"将{acc.GetVal(AccountTag.Name, "-")}设为{acc.GetVal(AccountTag.AdminLevel, "-")}级别管理员";
                }

                print?.Invoke(msg);
            };
        }

        /// <summary>
        /// 设置账号的管理员等级
        /// </summary>
        public static string SetAccAdminLevel(Dictionary<string, string> acc, int level)
        {
            if (acc == null) return "账号为空";

            acc.SetVal(AccountTag.AdminLevel, level.ToString());

            return null;
        }
        /// <summary>
        /// 设置账号的管理员等级
        /// </summary>
        public static string SetAccAdminLevel(object acc, object level, out Dictionary<string, string> ac)
        {
            ac = acc as Dictionary<string, string>;
            if (ac == null) return "账号为空";
            if (level is int lv == false) return "级别为空";

            return SetAccAdminLevel(ac, lv);
        }
    }
}
