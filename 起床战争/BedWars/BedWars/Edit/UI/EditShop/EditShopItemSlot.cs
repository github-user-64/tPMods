using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace BedWars.Edit.UI.EditShop
{
    internal class EditShopItemSlot : UIPanel
    {
        public Action<int> OnChecked = null;
        public bool IsChecked { get; protected set; } = false;
        private ShopData data = null;
        private int index = -1;
        private Item item = null;

        public EditShopItemSlot(ShopData data, int index)
        {
            this.data = data;
            this.index = index;

            item = new Item();

            SetPadding(6);
            BackgroundColor = BorderColor = new Color(43, 60, 120);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Parent?.Children?.AsParallel()?.ForAll(ui =>
            {
                if (ui is EditShopItemSlot slot) slot.IsChecked = false;
            });

            IsChecked = true;

            OnChecked?.Invoke(index);

            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            BorderColor = IsChecked ? new Color(255, 215, 0) : BackgroundColor;

            if (IsMouseHovering) UpdateMouseHovering();

            data.item[index].Paste(ref item);

            if (IsMouseHovering == false) return;

            List<string> ss = new List<string>();
            
            ss.Add(item.HoverName);
            ss.Add($"id:{item.type}");
            int[] vs = EditShopItems.GetVal(item.value);
            ss.Add($"[c/FFD700:价格][i:74]{vs[3]}[i:73]{vs[2]}[i:72]{vs[1]}[i:71]{vs[0]}");
            ss.Add("[c/aaffaa:用物品点击复制到商店,右键移除]");

            tContentPatch.Content.DrawTip.SetDraw(ss.ToArray());
        }

        private void UpdateMouseHovering()
        {
            Item hi = Main.LocalPlayer.HeldItem;

            if (Main.mouseLeft && Main.mouseLeftRelease && hi != null && hi.type != ItemID.None)
            {
                data.item[index].Copy(hi.Clone());

                SoundEngine.PlaySound(SoundID.Coins);
            }
            else if (Main.mouseRight && Main.mouseRightRelease)
            {
                data.item[index].Copy();

                SoundEngine.PlaySound(SoundID.Grab);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle rect = GetInnerDimensions();
            Vector2 center = new Vector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            ItemSlot.DrawItemIcon(item, ItemSlot.Context.CreativeInfinite, spriteBatch,
                center, 1, rect.Width, Color.White);
        }
    }
}
