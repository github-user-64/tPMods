using BedWars.BedWarsData;
using BedWars.Common.UI;
using tContentPatch.Content.UI;
using Terraria;

namespace BedWars.Edit.UI.EditShop
{
    internal class EditShopItems : UIStackPanel
    {
        private ShopData data = null;
        private int index = -1;

        public EditShopItems(ShopData data)
        {
            this.data = data;

            Width.Set(0, 1);
            IsAutoUpdateSize = true;

            BuildItems();
        }

        private void BuildItems()
        {
            int rowCount = Chest.maxItems / 10;
            int columnCount = Chest.maxItems / rowCount;
            int itemSize = 32;

            UIWrapPanel2 wp = new UIWrapPanel2();
            wp.ItemMargin = 4;
            wp.Width.Set(columnCount * (itemSize + wp.ItemMargin) + wp.ItemMargin, 0);
            Append(wp);

            for (int i = 0; i < data.item.Count; ++i)
            {
                EditShopItemSlot item = new EditShopItemSlot(data, i);
                item.Width.Set(itemSize, 0);
                item.Height.Set(itemSize, 0);
                item.OnChecked += v => index = v;
                wp.Append(item);
            }
        }

        public static int[] GetVal(int val)
        {
            int[] vs = new int[4];

            for (int i = 0; i < vs.Length; ++i)
            {
                vs[i] = val % 100;
                val /= 100;
            }
            
            return vs;
        }
    }
}
