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
            TipText = tip;

            this.print = print;
            SubCommand.Add(new CommandHelpList(SubCommand, tip, print));
        }

        /// <inheritdoc/>
        public override object Run(ref int index, List<CommandObject> commandList)
        {
            for (int i = 0;
                i < ArgCount &&
                i + index + 1 < commandList.Count;
                ++i)
            {
                if (commandList[index + i + 1] is CommandHelpList chl == false) continue;

                index += i + 1;
                chl.Run(ref index, commandList);
                return null;
            }
            
            return base.Run(ref index, commandList);
        }
    }
}
