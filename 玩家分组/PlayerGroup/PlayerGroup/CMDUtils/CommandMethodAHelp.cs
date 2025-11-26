using CommandHelp;
using System;
using System.Collections.Generic;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 有提示的方法
    /// </summary>
    public class CommandMethodAHelp : CommandMethod
    {
        /// <summary>
        /// 输出
        /// </summary>
        public Action<string> print = null;

        /// <summary>
        /// 有提示的方法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="argCount"></param>
        /// <param name="tip"></param>
        /// <param name="print"></param>
        public CommandMethodAHelp(string text = null, int argCount = 0, string tip = null, Action<string> print = null) : base(text, argCount)
        {
            this.print = print;
            SubCommand.Add(new CommandHelpList(SubCommand, tip, print));
        }

        /// <inheritdoc/>
        public override object Run(ref int index, List<CommandObject> commandList)
        {
            if (index + 1 < commandList.Count)
            {
                if (commandList[index + 1] is CommandHelpList chl)
                {
                    ++index;
                    chl.Run(ref index, commandList);
                    return null;
                }
            }

            return base.Run(ref index, commandList);
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="text"></param>
        /// <param name="argCount"></param>
        /// <param name="tip"></param>
        /// <param name="action"></param>
        /// <param name="cos"></param>
        /// <returns></returns>
        public static CommandMethodAHelp Build(string text = null, int argCount = 0, string tip = null, Action<object[]> action = null, params CommandObject[] cos)
        {
            CommandMethodAHelp co = new CommandMethodAHelp(text, argCount, tip);
            co.SubCommand.AddRange(cos);
            if (action != null) co.Runing += action;

            return co;
        }
    }
}
