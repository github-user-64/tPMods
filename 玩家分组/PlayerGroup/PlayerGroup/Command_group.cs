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
            List<CommandObject> list = new List<CommandObject>();

            list.Add(new pg_addn(print));

            return list;
        }
    }

    internal class pg_addn : CommandMethodGAP
    {
        public pg_addn(Action<string> print) : base("addn", "添加玩家名", print) { }

        public override void OnRuning(GroupData group, string playName)
        {
            if (group.AddName(playName)) Utils.Utils.PrintTry($"[{group.Name}]已添加名称[{playName}]", print);
            else Utils.Utils.PrintTry($"[{group.Name}]中已存在名称[{playName}]", print);
        }
    }
}
