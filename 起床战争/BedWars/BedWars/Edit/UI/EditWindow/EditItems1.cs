using BedWars.Edit.UI.EditItem_DisplayData;
using BedWars.Edit.UI.Elements;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditItems1 : UIEditItems, IPanel
    {
        public EditItems1()
        {
            AddItem(new DisplayMapPosSize("显示地图位置大小"));
            AddItem(new DisplaySpawItem("显示生成物品位置"));
        }

        public void OnOpen() { }
    }
}
