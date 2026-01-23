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
    public class AccTags : CommandMethod
    {
        /// <summary>
        /// 设置账号标签
        /// </summary>
        public AccTags(Action<string> print) : base(CommandText.AccTags, 3)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandGetAcc ga = new CommandGetAcc();
            ga.SubCommand.Add(new CommandPrintList(ga.SubCommand, "标签", print));
            SubCommand.Add(ga);

            CommandString t = new CommandString();
            t.SubCommand.Add(new CommandPrintList(t.SubCommand, "标签值", print));
            t.SubCommand.Add(new CommandString(true));
            ga.SubCommand.Add(t);

            Runing += args =>
            {
                if (args[0] is Dictionary<string, string> acc == false)
                {
                    print?.Invoke("账号为null");
                    return;
                }

                if (args[1] is string tag == false)
                {
                    print?.Invoke("标签为null");
                    return;
                }

                string val = args[2] as string;

                acc.SetVal(tag, val);

                print?.Invoke($"{acc.GetVal(AccountTag.Name)}的标签设置为{acc.GetKeyValString(tag)}");
            };
        }
    }
}
