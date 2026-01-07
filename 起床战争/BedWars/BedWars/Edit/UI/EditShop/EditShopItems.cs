using BedWars.BedWarsData;
using BedWars.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common;
using ReLogic.Content;
using tContentPatch.Content.UI;
using tContentPatch.Content.UI.ModSet;
using Terraria;
using Terraria.GameContent.UI.Elements;
using static System.Net.Mime.MediaTypeNames;

namespace BedWars.Edit.UI.EditShop
{
    internal class EditShopItems : UIStackPanel
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Item_27", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/NPC_105", AssetRequestMode.ImmediateLoad);

        private ShopData data = null;
        private ModifyShop.ItemData editItem = null;
        private EditShopItemSlot editIco = null;
        private UIItemTextBoxUpdate<int> editType = null;
        private UIItemTextBoxUpdate<byte> editPrefix = null;
        private EditValue editVal0 = null;
        private EditValue editVal1 = null;
        private EditValue editVal2 = null;
        private EditValue editVal3 = null;

        public EditShopItems(ShopData data)
        {
            this.data = data;

            Width.Set(0, 1);
            ItemMargin = 4;
            IsAutoUpdateSize = true;

            UIStackPanel sp1 = new UIStackPanel();
            sp1.Width.Set(0, 1);
            sp1.Height.Set(30, 0);
            sp1.ItemMargin = 6;
            sp1.Horizontal = true;
            Append(sp1);

            editIco = new EditShopItemSlot();
            editIco.Width.Set(sp1.Height.Pixels, 0);
            editIco.Height.Set(sp1.Height.Pixels, 0);
            sp1.Append(editIco);

            editType = new UIItemTextBoxUpdate<int>(null, "物品类型", ico: ico1.Value);
            editType.Width.Set(123, 0);
            editType.Height.Set(sp1.Height.Pixels, 0);
            editType.tb.Width.Set(-sp1.Height.Pixels, 1);
            editType.MouseText = "物品type";
            sp1.Append(editType);

            editPrefix = new UIItemTextBoxUpdate<byte>(null, "物品前缀", ico: ico2.Value);
            editPrefix.Width.Set(123, 0);
            editPrefix.Height.Set(sp1.Height.Pixels, 0);
            editPrefix.tb.Width.Set(-sp1.Height.Pixels, 1);
            editPrefix.MouseText = "物品前缀";
            sp1.Append(editPrefix);

            UIItem editVals = new UIItem();
            editVals.Width.Set(300, 0);
            editVals.Height.Set(30, 0);
            Append(editVals);

            editVal0 = new EditValue(0, "铜");
            editVal1 = new EditValue(1, "银");
            editVal2 = new EditValue(2, "金");
            editVal3 = new EditValue(3, "铂金");
            editVals.Append(editVal0);
            editVals.Append(editVal1);
            editVals.Append(editVal2);
            editVals.Append(editVal3);

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
                EditShopItemSlot item = new EditShopItemSlot(data.item[i]);
                item.Width.Set(itemSize, 0);
                item.Height.Set(itemSize, 0);
                item.OnChecked += SetEditItem;
                wp.Append(item);
            }
        }

        private void SetEditItem(ModifyShop.ItemData editItem = null)
        {
            this.editItem = editItem;

            editIco.SetData(this.editItem);
            editVal0.SetData(this.editItem);
            editVal1.SetData(this.editItem);
            editVal2.SetData(this.editItem);
            editVal3.SetData(this.editItem);
            editType.gss = null;
            editPrefix.gss = null;

            if (this.editItem == null) return;

            editType.gss = new GetSetStringInt(() => editItem.type, v => editItem.type = v);
            editPrefix.gss = new GetSetStringByte(() => editItem.prefix, v => editItem.prefix = v);
        }

        public static int[] GetVal(int val)
        {
            int[] vs = new int[4];

            for (int i = 0; i < vs.Length - 1; ++i)
            {
                vs[i] = val % 100;
                val /= 100;
            }

            vs[vs.Length - 1] = val;

            return vs;
        }
    }
}
