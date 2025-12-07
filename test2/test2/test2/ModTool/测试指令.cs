using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Command;
using ModTool.EntityTag;
using System;
using System.Collections.Generic;
using Terraria;

namespace test2.MT
{
    public class 测试指令
    {
        public static void Init()
        {
            ChatBarCMD.Common.GameChatCommand.NetMode01.CMD.Add((i, p) =>
            {
                return GetCMD(Main.LocalPlayer, p);
            });

            ChatBarCMD.Common.GameChatCommand.NetMode01.ServerCMD.Add((i, p) =>
            {
                List<CommandObject> list = new List<CommandObject>();

                CommandObject clear = new CommandObject("clear");
                list.Add(clear);

                clear.SubCommand.Add(new CommandObject("p"));
                clear.SubCommand.Add(new CommandObject("i"));

                list.Add(new CommandObject("playing"));

                return list;
            });

            ChatBarCMD.Common.GameChatCommand.NetMode2.CMD.Add((i, p) =>
            {
                List<CommandObject> list = new List<CommandObject>();

                CommandObject clear = new CommandObject("clear");
                list.Add(clear);

                CommandMethod clear_p = new CommandMethod("p");
                clear_p.Runing += _ =>
                {
                    ModTool.ServerHelp.PrintTo.PrintToPlayAll($"发送到全部", Color.SaddleBrown);
                    ModTool.ServerHelp.PrintTo.PrintToPlay(i, "发给你哦哦哦", Color.BlanchedAlmond);
                };
                clear.SubCommand.Add(clear_p);

                CommandMethod playing = new CommandMethod("playing");
                playing.Runing += _ => p?.Invoke("超级大玩家");
                list.Add(playing);

                CommandMethod getp = new CommandMethod("getp", 1);
                getp.SubCommand.Add(new CommandPrintList(getp.SubCommand, print: p));
                getp.SubCommand.Add(new CommandGetPlayer());
                getp.Runing += args => p?.Invoke((args[0] as Player)?.name ?? "是null哦");
                list.Add(getp);

                return list;
            });
        }

        public static List<CommandObject> GetCMD(Player player, Action<string> print)
        {
            List<CommandObject> list = new List<CommandObject>();

            CommandObject root = new CommandObject("cs") { TipText = "测试指令" };
            root.SubCommand.Add(new CommandPrintList(root.SubCommand, root.TipText, print));
            list.Add(root);

            root.SubCommand.Add(new cs_sett(player, print));
            root.SubCommand.Add(new cs_delt(player, print));

            return list;
        }

        private class cs_sett : CommandeEntityTagMethod
        {
            public override string TipText => "设置标签";

            public cs_sett(Player player, Action<string> print) : base("sett", player, print) { }

            public override void OnTagMethod(Entity entity, string tag, string val)
            {
                if (entity is NPC npc)
                {
                    Entitys.npc.SetVal(npc, tag, val);
                    print?.Invoke($"{npc.GetGivenOrTypeNetName()}的标签设置为{Entitys.npc.GetTagValString(npc, tag)}");
                }
                else if (entity is Item item)
                {
                    Entitys.item.SetVal(item, tag, val);
                    print?.Invoke($"{item.HoverName}的标签设置为{Entitys.item.GetTagValString(item, tag)}");
                }
                else if (entity is Projectile proj)
                {
                    Entitys.projectile.SetVal(proj, tag, val);
                    print?.Invoke($"{proj.Name}的标签设置为{Entitys.projectile.GetTagValString(proj, tag)}");
                }
                else if (entity is Player player)
                {
                    Entitys.player.SetVal(player, tag, val);
                    print?.Invoke($"{player.name}的标签设置为{Entitys.player.GetTagValString(player, tag)}");
                }
            }
        }

        private class cs_delt : CommandeEntityTagMethod
        {
            public override string TipText => "删除标签";

            public cs_delt(Player player, Action<string> print) : base("delt", player, print) { }

            public override void OnTagMethod(Entity entity, string tag, string val)
            {
                if (entity is NPC npc)
                {
                    Entitys.npc.DelTag(npc, tag);
                    print?.Invoke($"{npc.GetGivenOrTypeNetName()}的标签{tag}已删除");
                }
                else if (entity is Item item)
                {
                    Entitys.item.DelTag(item, tag);
                    print?.Invoke($"{item.HoverName}的标签{tag}已删除");
                }
                else if (entity is Projectile proj)
                {
                    Entitys.projectile.DelTag(proj, tag);
                    print?.Invoke($"{proj.Name}的标签{tag}已删除");
                }
                else if (entity is Player player)
                {
                    Entitys.player.DelTag(player, tag);
                    print?.Invoke($"{player.name}的标签{tag}已删除");
                }
            }
        }
    }
}
