using CommandHelp;
using PlayerGroup.CMDUtils;
using System;
using System.Collections.Generic;

namespace PlayerGroup
{
    /// <summary>
    /// 指令, 分组操作
    /// </summary>
    public class Command_group
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<CommandObject> GetCO(Action<string> print)
        {
            List<CommandObject> list = new List<CommandObject>
            {
                get_addp(print),
                get_delp(print),
                get_sett(print),
                get_delt(print),
            };

            return list;
        }

        public static CommandObject get_addp(Action<string> print) => new pg_addn(print);
        public static CommandObject get_delp(Action<string> print) => new pg_deln(print);
        public static CommandObject get_sett(Action<string> print) => new pg_sett(print);
        public static CommandObject get_delt(Action<string> print) => new pg_delt(print);
    }

    internal class pg_addn : CommandMethodGAP
    {
        public pg_addn(Action<string> print) : base("addp", "添加玩家名", print) { }

        public override void OnRuning(GroupData group, string playName)
        {
            if (group.AddName(playName)) Utils.Utils.PrintTry($"[{group.Name}]已添加名称[{playName}]", print);
            else Utils.Utils.PrintTry($"[{group.Name}]中已存在名称[{playName}]", print);
        }
    }

    internal class pg_deln : CommandMethodGAP
    {
        public pg_deln(Action<string> print) : base("delp", "删除玩家名", print) { }

        public override void OnRuning(GroupData group, string playName)
        {
            if (group.DelName(playName)) Utils.Utils.PrintTry($"[{group.Name}]已删除名称[{playName}]", print);
            else Utils.Utils.PrintTry($"[{group.Name}]中不存在名称[{playName}]", print);
        }
    }

    internal class pg_sett : CommandMethodGAT
    {
        public pg_sett(Action<string> print) : base("sett", "设置标签", print) { }

        public override void OnRuning(GroupData group, string tag, string val)
        {
            group.SetTag(tag, val);
            Utils.Utils.PrintTry($"[{group.Name}]的标签设置为[{tag}:{group.GetTag(tag)}]");
        }
    }

    internal class pg_delt : CommandMethodGAT
    {
        public pg_delt(Action<string> print) : base("delt", "删除标签", print) { }

        public override void OnRuning(GroupData group, string tag, string val)
        {
            bool ok = group.DelTag(tag);
            if (ok) Utils.Utils.PrintTry($"[{group.Name}]已删除标签[{tag}]");
            else Utils.Utils.PrintTry($"[{group.Name}]没有[{tag}]标签被删除");
        }
    }
}
