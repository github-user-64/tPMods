using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand.Acc
{
    /// <summary/>
    public class AccSetMan : CommandMethod
    {
        /// <summary>
        /// 将账号设为服主
        /// </summary>
        public AccSetMan(Action<string> print) : base(CommandText.AccSetMan, 1)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));
            SubCommand.Add(new CommandGetAcc());

            Runing += args =>
            {
                Dictionary<string, string> acc = args[0] as Dictionary<string, string>;

                string msg = AccSetAdmin.SetAccAdminLevel(acc, 0);

                if (msg == null)
                {
                    msg = $"Server";
                    msg += $"将{acc.GetVal(AccountTag.Name, "-")}设为{acc.GetVal(AccountTag.AdminLevel, "-")}级别管理员";
                }

                print?.Invoke(msg);
            };
        }
    }
}
