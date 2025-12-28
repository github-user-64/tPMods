using BedWars.BedWarsData;
using BedWars.Common.UI;
using Terraria.ID;

namespace BedWars.Edit.UI.EditSpawItem
{
    internal class EditItemType : UIWrapPanel2
    {
        private SpawItemData data = null;

        public EditItemType(SpawItemData data)
        {
            this.data = data;

            ItemMargin = 2;
        }

        public void UpdateData()
        {
            RemoveAllChildren();

            for (int i = 0; i < data.types.Count; ++i)
            {
                EditItemTypeItem ui = new EditItemTypeItem(data, UpdateData, i);

                Append(ui);
            }
        }

        public void AddType()
        {
            data.types.Add(ItemID.IronPickaxe);

            UpdateData();
        }
    }
}
