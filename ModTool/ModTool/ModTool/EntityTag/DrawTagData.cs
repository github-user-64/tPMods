using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.AdditionalData;
using ModTool.Common.UI;
using ModTool.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using tContentPatch;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.UI;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 显示标签数据
    /// </summary>
    public class DrawTagData : PatchMain
    {
        private static GetSetReset<bool> EnableDrawPlayer = new GetSetReset<bool>();
        private static GetSetReset<bool> EnableDrawProjectile = new GetSetReset<bool>();
        private static GetSetReset<bool> EnableDrawItem = new GetSetReset<bool>();
        private static GetSetReset<bool> EnableDrawNPC = new GetSetReset<bool>();
        private static Color[] info_Colors = { Color.Green, Color.BlueViolet, Color.Gold, Color.Pink };

        /// <inheritdoc/>
        public override void SetupDrawInterfaceLayersPostfix(List<GameInterfaceLayer> gameInterfaceLayers)
        {
            int index = gameInterfaceLayers.FindIndex(i => i.Name == "Vanilla: Inventory");
            if (index != -1)
            {
                gameInterfaceLayers.Insert(index, new LegacyGameInterfaceLayer(
                    "StaticTile.ModTool: DrawTagData.InventoryPrefix",
                    () =>
                    {
                        if (EnableDrawPlayer.val) DrawAD(Main.spriteBatch, Main.player, Entitys.player);
                        if (EnableDrawProjectile.val) DrawAD(Main.spriteBatch, Main.projectile, Entitys.projectile);
                        if (EnableDrawItem.val) DrawAD(Main.spriteBatch, Main.item, Entitys.item, i => i != Main.item.Length - 1);
                        if (EnableDrawNPC.val) DrawAD(Main.spriteBatch, Main.npc, Entitys.npc);

                        return true;
                    },
                    InterfaceScaleType.Game));
            }
        }

        /// <summary>
        /// 绘制附加数据
        /// </summary>
        public static void DrawAD<T>(SpriteBatch spriteBatch, T[] list,
            AdditionalData<Dictionary<string, string>, T> ad,
            Func<int, bool> fun = null) where T : Entity
        {
            Vector2 pos = Main.LocalPlayer.Center;

            for (int i = 0; i < list.Length; ++i)
            {
                T e = list[i];

                if (e == null) continue;
                if (e.active == false) continue;
                if (fun != null && fun(i) == false) continue;

                if (pos.Distance(e.Center) > 1000) continue;

                Dictionary<string, string> tag = ad.GetTagDic(e);
                if (tag == null) continue;

                Color color = info_Colors[i % info_Colors.Length];

                DrawTag(spriteBatch, e, tag, color);
            }
        }

        /// <summary>
        /// 绘制标签数据
        /// </summary>
        public static void DrawTag<T>(SpriteBatch spriteBatch, T obj, Dictionary<string, string> tag, Color color) where T : Entity
        {
            StringBuilder strb = new StringBuilder();

            foreach (KeyValuePair<string, string> i in tag)
            {
                if (strb.Length > 0) strb.Append('\n');

                strb.Append('{');

                if (i.Key != null)
                {
                    strb.Append(i.Key.ToString());
                }

                strb.Append(',');

                if (i.Value != null)
                {
                    strb.Append(i.Value.ToString());
                }

                strb.Append('}');
            }

            Vector2 pos = obj.position - Main.screenPosition;
            pos.Y += obj.height;

            Terraria.Utils.DrawBorderString(Main.spriteBatch,
                strb.ToString(),
                pos, color);
        }

        private class setui : ModSetting
        {
            public override string Name => "数据显示";
            public override string Title => "模组工具: 数据显示";
            public override UIElement GetUI()
            {
                UIScrollViewer2 sv = new UIScrollViewer2();
                sv.Width.Precent = 1;
                sv.Height.Precent = 1;

                sv.AddChild(new tContentPatch.Content.UI.ModSet.UIItemTitle(null, "显示标签"));
                sv.AddChild(new UIItemSwitch(EnableDrawPlayer, null, "显示玩家标签"));
                sv.AddChild(new UIItemSwitch(EnableDrawProjectile, null, "显示射弹标签"));
                sv.AddChild(new UIItemSwitch(EnableDrawItem, null, "显示物品标签"));
                sv.AddChild(new UIItemSwitch(EnableDrawNPC, null, "显示NPC标签"));

                return sv;
            }
        }
    }
}
