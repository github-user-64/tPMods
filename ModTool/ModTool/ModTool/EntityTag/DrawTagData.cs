using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.AdditionalData;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.UI;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 显示标签数据
    /// </summary>
    internal class DrawTagData : PatchMain
    {
        private static bool EnableDrawPlayer = true;
        private static bool EnableDrawProjectile = true;
        private static bool EnableDrawItem = true;
        private static bool EnableDrawNPC = true;

        public override void SetupDrawInterfaceLayersPostfix(List<GameInterfaceLayer> gameInterfaceLayers)
        {
            int index = gameInterfaceLayers.FindIndex(i => i.Name == "Vanilla: Inventory");
            if (index != -1)
            {
                gameInterfaceLayers.Insert(index, new LegacyGameInterfaceLayer(
                    "StaticTile.ModTool: DrawTagData.InventoryPrefix",
                    () =>
                    {
                        if (EnableDrawPlayer == false)
                            Draw(Main.spriteBatch, Main.player, Entitys.player);
                        if (EnableDrawProjectile == false)
                            Draw(Main.spriteBatch, Main.projectile, Entitys.projectile);
                        if (EnableDrawItem == false)
                            Draw(Main.spriteBatch, Main.item, Entitys.item);
                        if (EnableDrawNPC == false)
                            Draw(Main.spriteBatch, Main.npc, Entitys.npc);

                        return true;
                    },
                    InterfaceScaleType.Game));
            }
        }

        private static void Draw<T>(SpriteBatch spriteBatch, T[] list,
            AdditionalData<Dictionary<string, string>, T> ad) where T : Entity
        {
            Vector2 pos = Main.LocalPlayer.Center;

            foreach (T i in list)
            {
                if (i == null) continue;
                if (i.active == false) continue;

                if (pos.Distance(i.Center) > 1000) continue;

                Dictionary<string, string> tag = ad.GetKeyVal(i);
                if (tag == null) continue;

                Draw(spriteBatch, i, tag);
            }
        }

        private static void Draw<T>(SpriteBatch spriteBatch, T obj, Dictionary<string, string> tag) where T : Entity
        {

        }
    }
}
