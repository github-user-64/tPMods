using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common;
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
        public Action<ModifyShop.ItemData> OnChecked = null;
        public bool IsChecked { get; protected set; } = false;
        private ModifyShop.ItemData data = null;
        private Item item = null;

        public EditShopItemSlot(ModifyShop.ItemData data = null)
        {
            item = new Item();

            SetData(data);

            SetPadding(6);
            BackgroundColor = BorderColor = new Color(43, 60, 120);
        }

        public void SetData(ModifyShop.ItemData data = null)
        {
            this.data = data;

            UpdateItem();
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Parent?.Children?.AsParallel()?.ForAll(ui =>
            {
                if (ui is EditShopItemSlot slot) slot.IsChecked = false;
            });

            IsChecked = true;

            OnChecked?.Invoke(data);

            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            BorderColor = IsChecked ? new Color(255, 215, 0) : BackgroundColor;

            UpdateData();
            UpdateItem();

            if (IsMouseHovering) UpdateTip();
        }

        private void UpdateItem()
        {
            if (data == null) item.SetDefaults(ItemID.None);
            else data.Paste(ref item);
        }

        private void UpdateData()
        {
            if (IsMouseHovering == false) return;
            if (data == null) return;
            Item hi = Main.LocalPlayer.HeldItem;

            if (Main.mouseLeft && Main.mouseLeftRelease && hi != null && hi.type != ItemID.None)
            {
                data.Copy(hi.Clone());

                SoundEngine.PlaySound(SoundID.Coins);
            }
            else if (Main.mouseRight && Main.mouseRightRelease)
            {
                data.Copy();

                SoundEngine.PlaySound(SoundID.Grab);
            }
        }

        private void UpdateTip()
        {
            if (data == null)
            {
                tContentPatch.Content.DrawTip.SetDraw("空空如也");
                return;
            }

            List<string> ss = new List<string>();

            ss.Add(item.HoverName);
            ss.Add($"id:{item.type}");
            int[] vs = EditShopItems.GetVal(item.value);
            ss.Add($"[c/FFD700:价格][i:71]{vs[0]}[i:72]{vs[1]}[i:73]{vs[2]}[i:74]{vs[3]}");
            ss.Add("[c/aaffaa:用物品点击复制到商店,右键移除]");

            tContentPatch.Content.DrawTip.SetDraw(ss.ToArray());
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (data == null) return;

            CalculatedStyle rect = GetInnerDimensions();
            Vector2 center = new Vector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            ItemSlot.DrawItemIcon(item, ItemSlot.Context.CreativeInfinite, spriteBatch,
                center, 1, rect.Width, Color.White);
        }
    }
}
