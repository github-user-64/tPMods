using CommandHelp;
using System;
using System.Collections.Generic;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 方法(分组, 玩家名或索引)
    /// </summary>
    public class CommandMethodGAP : CommandMethodAHelp
    {
        /// <summary>
        /// 方法(分组, 玩家名或索引)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tip"></param>
        /// <param name="print"></param>
        public CommandMethodGAP(string text, string tip, Action<string> print) : base(text, 2, $"{tip}(分组, 玩家名或索引)", print)
        {
            TipText = $"{tip}(分组, 玩家名或索引)";

            CommandGetGroup g = new CommandGetGroup(print: print) { TipText = "分组名或索引" };
            CommandGetPlayName p = new CommandGetPlayName(print: print) { TipText = "玩家名或索引" };

            SubCommand.Add(g);
            g.SubCommand.Add(new CommandHelpList(g.SubCommand, p.TipText, print));
            g.SubCommand.Add(p);
        }

        /// <inheritdoc/>
        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            if (args[0] is GroupData gd == false) return null;
            if (args[1] is string name == false) return null;
            OnRuning(gd, name);

            return base.OnRuning(ref index, commandList, args);
        }

        /// <summary>
        /// 分组和玩家名都正确时调用
        /// </summary>
        /// <param name="group"></param>
        /// <param name="playName"></param>
        public virtual void OnRuning(GroupData group, string playName) { }
    }
}
