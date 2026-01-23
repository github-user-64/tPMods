using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using PlayerAccount.Command;
using System;
using System.Collections.Generic;

namespace PlayerAccount.Common.FunctionCommand.Ban
{
    /// <summary/>
    public class BanDel : CommandObject
    {
        /// <summary>
        /// 删除封禁, 服务端或腐竹使用
        /// </summary>
        public BanDel(Action<string> print = null) : base(CommandText.BanDel)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, null, print));

            CommandMethod ip = new CommandMethod(CommandText.BanIp, 2);
            ip.SubCommand.Add(new CommandPrintList(ip.SubCommand, "不填写端口则为默认值null", print));
            ip.AddRAdd(new CommandString2()).AddRAdd(new CommandString2(true));
            ip.Runing += args =>
            {
                string _ip = args[0] as string;
                string _po = args[1] as string;

                string ex = AccountHelp.BanDelIP(_ip, _po);
                print?.Invoke(ex ?? $"已删除:{_ip}:{_po}");
            };
            SubCommand.Add(ip);

            CommandMethod name = new CommandMethod(CommandText.BanName, 1);
            name.SubCommand.Add(new CommandPrintList(name.SubCommand, "玩家名", print));
            name.AddRAdd(new CommandString());
            name.Runing += args =>
            {
                string n = args[0] as string;

                string ex = AccountHelp.BanDelName(n);
                print?.Invoke(ex ?? $"已删除{n}的封禁");
            };
            SubCommand.Add(name);

            CommandMethod acc = new CommandMethod(CommandText.BanAcc, 1);
            acc.SubCommand.Add(new CommandPrintList(acc.SubCommand, null, print));
            acc.AddRAdd(new CommandGetAcc());
            acc.Runing += args =>
            {
                if (args[0] is Dictionary<string, string> ac == false)
                {
                    print?.Invoke("账号为null");
                    return;
                }

                string n = ac.GetVal(AccountTag.Name);

                string ex = AccountHelp.BanDelAccName(n);
                print?.Invoke(ex ?? $"已删除{n}的封禁");
            };
            SubCommand.Add(acc);
        }
    }
}
