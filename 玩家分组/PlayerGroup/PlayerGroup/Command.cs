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
            List<CommandObject> list = new List<CommandObject>();
            CommandObject root = new CommandObject("pg");
            root.SubCommand.Add(new CommandHelpList(root.SubCommand,
                "输出分组, 添加分组, 删除分组, 保存分组, 更新数据"
                , ContentPatch.PrintTry));

            root.SubCommand.Add(get_list(ContentPatch.PrintTry));
            root.SubCommand.Add(get_add(ContentPatch.PrintTry));
            root.SubCommand.Add(get_del(ContentPatch.PrintTry));
            root.SubCommand.Add(get_save(ContentPatch.PrintTry));
            root.SubCommand.Add(get_update(ContentPatch.PrintTry));
            root.SubCommand.AddRange(Command_group.GetCO(ContentPatch.PrintTry));

            list.Add(root);

            return list;
        }

        public static CommandObject get_list(Action<string> print) => new pg_list(print);
        public static CommandObject get_add(Action<string> print) => new pg_add(print);
        public static CommandObject get_del(Action<string> print) => new pg_del(print);
        public static CommandObject get_save(Action<string> print) => new pg_save(print);
        public static CommandObject get_update(Action<string> print) => new CommandMethodAction("update", () =>
        {
            print($"{GroupHelp.UpdateData() ?? "更新成功"}");
        });
    }

    internal class pg_list : CommandMethodAHelp
    {
        public pg_list(Action<string> print) : base("list", 1, "显示全部分组列表,加上分组名(\"group\")或索引显示分组信息", print)
        {
            SubCommand.Add(new CommandGetGroup(true, print));

            Runing += args =>
            {
                if (args[0] is GroupData gd) GroupHelp.PrintGroup(gd, print);
                else GroupHelp.PrintGroup(print);
            };
        }
    }

    internal class pg_add : CommandMethodAHelp
    {
        public pg_add(Action<string> print) : base("add", 1, "添加分组(分组名)", print)
        {
            SubCommand.Add(new CommandString());

            Runing += args =>
            {
                if (args[0] is string v == false) return;

                if (GroupHelp.HasGroup(v))
                {
                    print($"分组已存在[{v}]");
                    return;
                }

                GroupData gd = GroupHelp.AddGroup(v);
                print($"已添加分组[{gd.Name}]");
            };
        }
    }

    internal class pg_del : CommandMethodAHelp
    {
        public pg_del(Action<string> print) : base("del", 1, "删除指定名称或索引的分组", print)
        {
            SubCommand.Add(new CommandGetGroup(print: print));
            
            Runing += args =>
            {
                if (args[0] is GroupData gd == false) return;
                if (GroupHelp.DelGroup(gd)) print($"已删除分组[{gd.Name}]");
                else print($"分组[{gd.Name}]不在列表内");
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
            }));
            SubCommand.Add(new CommandGetGroup(print: print));

            Runing += args =>
            {
                if (args[0] is GroupData gd == false) return;
                Utils.Utils.ActionState(() => GroupHelp.SaveData(gd), $"已保存分组[{gd.Name}]", "保存失败");
            };
        }
    }
}
