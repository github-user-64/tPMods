using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand.Acc
{
    /// <summary/>
    public class AccTagd : CommandMethod
    {
        /// <summary>
        /// 删除账号标签
        /// </summary>
        public AccTagd(Action<string> print) : base(CommandText.AccTagd, 2)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandGetAcc ga = new CommandGetAcc();
            ga.SubCommand.Add(new CommandPrintList(ga.SubCommand, "标签", print));
            ga.SubCommand.Add(new CommandString());
            SubCommand.Add(ga);

            Runing += args =>
            {
                if (args[0] is Dictionary<string, string> acc == false)
                {
                    print?.Invoke("账号为null");
                    return;
                }

                string tag = args[1] as string;

                acc.DelKey(tag);

                print?.Invoke($"{acc.GetVal(AccountTag.Name)}的标签{tag}已删除");
            };
        }
    }
}
