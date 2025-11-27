using CommandHelp;
using PlayerGroup;
using PlayerGroup.CMDUtils;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Common.GameChatCommand
{
    internal partial class GameChatCommand
    {
        public static List<CommandObject> GetGameCO(Action<string> print)
        {
            List<CommandObject> list = new List<CommandObject>();
            list.Add(new CommandHelpList(list, print: print));

            CommandObject root = new CommandObject("pg");
            root.SubCommand.Add(new CommandHelpList(root.SubCommand,
                "显示数据, 添加分组, 删除分组, 保存分组, 更新数据, 添加玩家, 删除玩家, 设置标签, 删除标签"
                , print));
            list.Add(root);

            root.SubCommand.Add(Command.get_list(print));
            root.SubCommand.Add(Command.get_add(print));
            root.SubCommand.Add(Command.get_del(print));
            root.SubCommand.Add(Command.get_save(print));
            root.SubCommand.Add(Command.get_update(print));
            root.SubCommand.AddRange(Command_group.GetCO(print));

            return list;
        }
    }
}
