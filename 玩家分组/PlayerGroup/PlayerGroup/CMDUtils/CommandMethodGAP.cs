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
            CommandGetGroup a = new CommandGetGroup(print: print);
            a.SubCommand.Add(new CommandHelpList(a.SubCommand, "玩家名或索引", print));
            a.SubCommand.Add(new CommandGetPlayName());
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
