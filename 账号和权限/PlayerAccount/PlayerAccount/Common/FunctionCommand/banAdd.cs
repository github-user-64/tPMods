using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 添加封禁
    /// </summary>
    public class banAdd
    {
        /// <summary/>
        public class cmd : CommandObject
        {
            /// <summary>
            /// 服务端或腐竹使用
            /// </summary>
            public cmd(Action<string> print = null) : base("add")
            {
                SubCommand.Add(new CommandPrintList(SubCommand, null, print));

                CommandMethod ip = new CommandMethod("ip", 2);
                ip.SubCommand.Add(new CommandPrintList(ip.SubCommand, "不填写端口则为默认值null", print));
                ip.AddRAdd(new CommandString2()).AddRAdd(new CommandString2(true));
                ip.Runing += args =>
                {
                    string ex = AccountHelp.BanAddIP(args[0] as string, args[1] as string);
                    print?.Invoke(ex ?? "已添加");
                };
                SubCommand.Add(ip);

                CommandMethod name = new CommandMethod("name", 1);
                name.SubCommand.Add(new CommandPrintList(name.SubCommand, "玩家名", print));
                name.AddRAdd(new CommandString());
                name.Runing += args =>
                {
                    string ex = AccountHelp.BanAddName(args[0] as string);
                    print?.Invoke(ex ?? "已添加");
                };
                SubCommand.Add(name);

                CommandMethod acc = new CommandMethod("acc", 2);
                acc.SubCommand.Add(new CommandPrintList(acc.SubCommand, null, print));
                acc.AddRAdd(new CommandGetAcc()).AddRAdd(new CommandString2(true));
                acc.Runing += args =>
                {
                    if (args[0] is Dictionary<string, string> ac == false)
                    {
                        print?.Invoke("账号为null");
                        return;
                    }

                    string ex = AccountHelp.BanAddAccName(ac.GetVal(AccountTag.Name), args[1] as string);
                    print?.Invoke(ex ?? "已添加");
                };
                SubCommand.Add(acc);
            }
        }
    }
}
