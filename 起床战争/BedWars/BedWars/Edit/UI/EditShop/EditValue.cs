using BedWars.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BedWars.Edit.UI.EditShop
{
    internal class EditValue : UIElement
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Item_71", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/Item_72", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico3 = Main.Assets.Request<Texture2D>("Images/Item_73", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico4 = Main.Assets.Request<Texture2D>("Images/Item_74", AssetRequestMode.ImmediateLoad);
        private static Texture2D[] icos = new Texture2D[] { ico1.Value, ico2.Value, ico3.Value, ico4.Value };

        private int index = -1;
        private UITextBoxUpdate<int> tb = null;

        public EditValue(int index, string text)
        {
            this.index = index;

            Width.Set(-6, 1 / 4f);
            Height.Set(30, 0);
            Left.Set(0, 1 / 4f * index);

            UIImage ico = new UIImage(icos[index]);
            ico.Width.Set(10, 0);
            ico.Height.Set(10, 0);
            ico.VAlign = 0.5f;
            ico.ScaleToFit = true;
            Append(ico);

            tb = new UITextBoxUpdate<int>(null, text);
            tb.Width.Set(-ico.Width.Pixels - 6, 1);
            tb.Height.Set(0, 0.8f);
            tb.HAlign = 1;
            tb.VAlign = 0.5f;
            tb.MouseText = text;
            Append(tb);
        }

        public void SetData(ModifyShop.ItemData data = null)
        {
            tb.gss = null;
            if (data == null) return;

            tb.gss = GetGss(data, index);
        }

        private static GetSetStringInt GetGss(ModifyShop.ItemData data, int index)
        {
            return new GetSetStringInt(() => EditShopItems.GetVal(data.value)[index],
            v =>
            {
                int[] vals = EditShopItems.GetVal(data.value);
                vals[index] = v;

                try
                {
                    data.value = Item.buyPrice(vals[3], vals[2], vals[1], vals[0]);
                }
                catch { }
            });
        }
    }
}
