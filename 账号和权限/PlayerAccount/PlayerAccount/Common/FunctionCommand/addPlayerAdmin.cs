using CommandHelp;
using ModTool.Command;
using ModTool.Utils;
using PlayerAccount.Account;
using System;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 将在线玩家账号设为管理员
    /// </summary>
    public class addPlayerAdmin
    {
        /// <summary>
        /// 获取管理等级, 默认1
        /// </summary>
        public class CommandAdminLevel : CommandValue<int>
        {
            /// <inheritdoc/>
            public override string Text => "<int>";

            /// <inheritdoc/>
            protected override int ArgConvertThrow(string arg) => int.Parse(arg);

            /// <inheritdoc/>
            protected override int GetDefault() => 1;

            /// <summary/>
            public CommandAdminLevel(bool isVariable = false) : base(isVariable) { }
        }

        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary/>
            public cmd(Player player, Action<string> print) : base("添加管理员", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, null, print));

                CommandGetPlayer getP = new CommandGetPlayer();
                getP.SubCommand.Add(new CommandPrintList(getP.SubCommand, "管理等级,注意0是服主级别可以修改账号数据,值越大级别越小,不填默认1", print));
                getP.SubCommand.Add(new CommandAdminLevel(true));
                SubCommand.Add(getP);

                Runing += args =>
                {
                    if (args[0] is Player p == false)
                    {
                        print?.Invoke("玩家为null");
                        return;
                    }
                    if (args[1] is int pl == false)
                    {
                        print?.Invoke("级别为null");
                        return;
                    }

                    Dictionary<string, string> pAcc = p.GetAccount();
                    if (pAcc == null)
                    {
                        print?.Invoke("该玩家未登录");
                        return;
                    }

                    pAcc.SetVal(AccountTag.AdminLevel, pl.ToString());
                    print?.Invoke($"{(player == null ? "Server" : player.name)}将{p.name}设为{pl}级别管理员");
                };
            }
        }
    }
}
