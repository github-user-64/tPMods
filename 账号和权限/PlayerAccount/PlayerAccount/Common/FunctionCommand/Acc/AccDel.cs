using CommandHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand.Acc
{
    /// <summary/>
    public class AccDel : CommandMethod
    {
        /// <summary>
        /// 删除账号
        /// </summary>
        public AccDel(Action<string> print) : base(CommandText.AccDel, 1)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));
            SubCommand.Add(new CommandGetAcc());

            Runing += args =>
            {
                if (args[0] is Dictionary<string, string> acc == false)
                {
                    print?.Invoke("账号为null");
                    return;
                }

                AccountHelp.DelAccount(acc);

                print?.Invoke($"已删除[{acc.GetVal(AccountTag.Name)}]");
            };
        }
    }
}
