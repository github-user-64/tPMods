using CommandHelp;
using Microsoft.Xna.Framework;
using System;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 启用注册
    /// </summary>
    public class openRegister
    {
        /// <summary/>
        public class cmd : CommandMethod
        {
            /// <summary/>
            public cmd(Action<string> print) : base("启用注册", 1)
            {
                SubCommand.Add(new CommandPrintList(SubCommand, null, print));
                SubCommand.Add(new CommandeEnum(false, "打开", "关闭"));

                Runing += args =>
                {
                    if (args[0] is int v == false) return;

                    if (v == 0)
                    {
                        ServerConfig.data.EnableRegister = true;
                        print?.Invoke("服务器注册已启用");
                        ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll("服务器注册已启用", Color.Green);
                    }
                    else
                    {
                        ServerConfig.data.EnableRegister = false;
                        print?.Invoke("服务器注册已禁用");
                        ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll("服务器注册已禁用", Color.Red);
                    }
                };
            }
        }
    }
}
