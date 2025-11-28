using CommandHelp;
using System;
using System.Collections.Generic;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 方法(分组, 标签, 值)
    /// </summary>
    public class CommandMethodGAT : CommandMethodAHelp
    {
        /// <summary>
        /// 方法(分组, 标签, 值)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tip"></param>
        /// <param name="print"></param>
        public CommandMethodGAT(string text, string tip, Action<string> print) : base(text, 3, $"{tip}(分组, 标签, 值(可不填))", print)
        {
            TipText = $"{tip}(分组, 标签, 值(可不填))";

            CommandGetGroup g = new CommandGetGroup(print: print) { TipText = "分组" };
            CommandString tag = new CommandString() { TipText = "标签" };
            CommandString tagV = new CommandString(true) { TipText = "标签值(可不填)" };

            SubCommand.Add(g);
            g.SubCommand.Add(new CommandHelpList(g.SubCommand, tag.TipText, print));
            g.SubCommand.Add(tag);
            tag.SubCommand.Add(new CommandHelpList(tag.SubCommand, tagV.TipText, print));
            tag.SubCommand.Add(tagV);
        }

        /// <inheritdoc/>
        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            if (args[0] is GroupData gd == false) return null;
            if (args[1] is string tag == false) return null;
            OnRuning(gd, tag, args[2] as string);

            return base.OnRuning(ref index, commandList, args);
        }

        /// <summary>
        /// 指令正确时调用
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        public virtual void OnRuning(GroupData group, string tag, string val) { }
    }
}
