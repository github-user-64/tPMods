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
    /// 账号操作
    /// </summary>
    public class accAction
    {
        /// <summary/>
        public class cmd : CommandObject
        {
            /// <summary/>
            public cmd(Action<string> print) : base("acc")
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "删除账号, 显示账号数据, 设置账号标签, 删除账号标签", print));

                SubCommand.Add(new del(print));

                SubCommand.Add(new data(print));

                SubCommand.Add(new tags(print));

                SubCommand.Add(new tagd(print));
            }
        }

        /// <summary/>
        public class del : CommandMethod
        {
            /// <summary>
            /// 删除账号
            /// </summary>
            public del(Action<string> print) : base("del", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "账号", print));
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

        /// <summary/>
        public class data : CommandMethod
        {
            /// <summary>
            /// 显示账号数据
            /// </summary>
            public data(Action<string> print) : base("data", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "账号", print));
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

        /// <summary/>
        public class tags : CommandMethod
        {
            /// <summary>
            /// 设置账号标签
            /// </summary>
            public tags(Action<string> print) : base("tags", 3)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "账号", print));

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

        /// <summary/>
        public class tagd : CommandMethod
        {
            /// <summary>
            /// 删除账号标签
            /// </summary>
            public tagd(Action<string> print) : base("tagd", 2)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, "账号", print));

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
}
