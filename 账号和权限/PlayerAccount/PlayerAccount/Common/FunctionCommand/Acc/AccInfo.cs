using CommandHelp;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand.Acc
{
    /// <summary/>
    public class AccInfo : CommandMethod
    {
        /// <summary>
        /// 显示账号数据
        /// </summary>
        public AccInfo(Action<string> print) : base(CommandText.AccInfo, 1)
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

                string s = null;
                foreach (KeyValuePair<string, string> i in acc)
                {
                    if (s == null)
                    {
                        s = $"{{{i.Key},{i.Value}}}";
                    }
                    else
                    {
                        s += $"\n{{{i.Key},{i.Value}}}";
                    }
                }

                print?.Invoke(s ?? "没有数据");
            };
        }
    }
}
