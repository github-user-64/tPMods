using CommandHelp;
using PlayerGroup.CMDUtils;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerGroup
{
    /// <summary>
    /// 指令
    /// </summary>
    public class Command : Mod
    {
        /// <inheritdoc/>
        public override List<CommandObject> GetCommands()
        {
            return GetCMD(ContentPatch.PrintTry);
        }

        public static List<CommandObject> GetCMD(Action<string> print)
        {
            List<CommandObject> list = new List<CommandObject>();

            CommandObject root = new CommandObject("pg") { TipText = "玩家分组" };
            root.SubCommand.Add(new CommandHelpList(root.SubCommand,
                "显示数据, 添加分组, 删除分组, 保存分组, 更新数据, 添加玩家, 删除玩家, 设置标签, 删除标签"
                , print));

            root.SubCommand.Add(get_list(print));
            root.SubCommand.Add(get_add(print));
            root.SubCommand.Add(get_del(print));
            root.SubCommand.Add(get_save(print));
            root.SubCommand.Add(get_update(print));
            root.SubCommand.AddRange(Command_group.GetCO(print));

            list.Add(root);

            return list;
        }

        public static CommandObject get_list(Action<string> print) => new pg_list(print);
        public static CommandObject get_list_g(Action<string> print) => new pg_list_g(print);
        public static CommandObject get_list_play(Action<string> print) => new pg_list_play(print);
        public static CommandObject get_add(Action<string> print) => new pg_add(print);
        public static CommandObject get_del(Action<string> print) => new pg_del(print);
        public static CommandObject get_save(Action<string> print) => new pg_save(print);
        public static CommandObject get_update(Action<string> print) => new CommandMethodAction("update", () =>
        {
            Utils.Utils.PrintTry($"{GroupHelp.UpdateData() ?? "更新成功"}", print);
        })
        { TipText = "更新数据" };
    }

    internal class pg_list : CommandMethodAHelp
    {
        public pg_list(Action<string> print) : base("list", 1, "显示数据", print)
        {
            SubCommand.Add(Command.get_list_g(print));
            SubCommand.Add(Command.get_list_play(print));
        }
    }

    internal class pg_list_g : CommandMethodAHelp
    {
        public pg_list_g(Action<string> print) : base("g", 1, "显示全部分组列表,加上分组名[\"group\"]或索引[0]显示分组信息", print)
        {
            SubCommand.Add(new CommandGetGroup(true, print));

            Runing += args =>
            {
                if (args[0] is GroupData gd) GroupHelp.PrintGroup(gd, print);
                else GroupHelp.PrintGroup(print);
            };
        }
    }

    internal class pg_list_play : CommandMethodAHelp
    {
        public pg_list_play(Action<string> print) : base("p", 1, "显示玩家名所在分组(玩家名[\"play\"]或索引[0])", print)
        {
            SubCommand.Add(new CommandGetPlayName(print: print));

            Runing += args =>
            {
                if (args[0] is string s) GroupHelp.PrintNameHasGroup(s, print);
            };
        }
    }

    internal class pg_add : CommandMethodAHelp
    {
        public pg_add(Action<string> print) : base("add", 1, "添加分组(\"分组名\")", print)
        {
            SubCommand.Add(new CommandString() { TipText = "分组名" });

            Runing += args =>
            {
                if (args[0] is string v == false) return;

                if (GroupHelp.HasGroup(v))
                {
                    Utils.Utils.PrintTry($"分组已存在[{v}]", print);
                    return;
                }

                GroupData gd = GroupHelp.AddGroup(v);
                Utils.Utils.PrintTry($"已添加分组[{gd.Name}]", print);
            };
        }
    }

    internal class pg_del : CommandMethodAHelp
    {
        public pg_del(Action<string> print) : base("del", 1, "删除分组(分组名或索引)", print)
        {
            SubCommand.Add(new CommandGetGroup(print: print));
            
            Runing += args =>
            {
                if (args[0] is GroupData gd == false) return;
                if (GroupHelp.DelGroup(gd)) Utils.Utils.PrintTry($"已删除分组[{gd.Name}]", print);
                else Utils.Utils.PrintTry($"分组[{gd.Name}]不在列表内", print);
            };
        }
    }

    internal class pg_save : CommandMethodAHelp
    {
        public pg_save(Action<string> print) : base("save", 1, "all:保存全部, 保存指定名称或索引的分组", print)
        {
            SubCommand.Add(new CommandMethodAction("all", () =>
            {
                Utils.Utils.ActionState(() => GroupHelp.SaveData(GroupSetting.datas), "已保存全部", "保存失败", print);
            })
            {
                TipText = "保存全部"
            });
            SubCommand.Add(new CommandGetGroup(print: print));

            Runing += args =>
            {
                if (args[0] is GroupData gd == false) return;
                Utils.Utils.ActionState(() => GroupHelp.SaveData(gd), $"已保存分组[{gd.Name}]", "保存失败", print);
            };
        }
    }
}
