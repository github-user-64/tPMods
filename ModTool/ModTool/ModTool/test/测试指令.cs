using CommandHelp;
using ModTool.EntityTag;
using System;
using System.Collections.Generic;
using System.Linq;
using tContentPatch;
using Terraria;

namespace ModTool.test
{
    internal class 测试指令 : Mod
    {
        public override void Loaded()
        {
            List<tContentPatch.ModLoad.ModObject> mos = ContentPatch.GetModObjects();
            if (mos == null) return;

            tContentPatch.ModLoad.ModObject mo = mos.FirstOrDefault(i => i.config.key == "StaticTile.ChatBarCMD");
            if (mo == null) return;

            Init();
        }

        private static void Init()
        {
            ChatBarCMD.Common.GameChatCommand.GameChatCommand.GameCMD.Add(() =>
            {
                return GetCMD(Main.LocalPlayer, s => Main.NewText(s));
            });
        }

        private static List<CommandObject> GetCMD(Player player, Action<string> print)
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
