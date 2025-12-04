using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.AdditionalData;
using System.Collections.Generic;
using System.Text;
using tContentPatch;
using Terraria;
using Terraria.UI;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 显示标签数据
    /// </summary>
    public class DrawTagData : PatchMain
    {
        private static bool EnableDrawPlayer = true;
        private static bool EnableDrawProjectile = true;
        private static bool EnableDrawItem = true;
        private static bool EnableDrawNPC = true;
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
                        if (EnableDrawPlayer) DrawAD(Main.spriteBatch, Main.player, Entitys.player);
                        if (EnableDrawProjectile) DrawAD(Main.spriteBatch, Main.projectile, Entitys.projectile);
                        if (EnableDrawItem) DrawAD(Main.spriteBatch, Main.item, Entitys.item);
                        if (EnableDrawNPC) DrawAD(Main.spriteBatch, Main.npc, Entitys.npc);

                        return true;
                    },
                    InterfaceScaleType.Game));
            }
        }

        /// <summary>
        /// 绘制附加数据
        /// </summary>
        public static void DrawAD<T>(SpriteBatch spriteBatch, T[] list,
            AdditionalData<Dictionary<string, string>, T> ad) where T : Entity
        {
            Vector2 pos = Main.LocalPlayer.Center;

            for (int i = 0; i < list.Length; ++i)
            {
                T e = list[i];

                if (e == null) continue;
                if (e.active == false) continue;

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
    }
}
