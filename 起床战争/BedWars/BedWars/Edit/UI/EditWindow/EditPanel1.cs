using BedWars.Edit.UI.EditItem_EditData;
using BedWars.Edit.UI.Elements;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel1 : UIPanelSVEditControl, IPanel
    {
        public EditPanel1()
        {
            AddItem(new TpMapPos("传送", "传送到地图位置"));
            AddItem(new EditMapPos("设置地图位置"));
            AddItem(new EditMapSize("设置地图大小"));
        }

        public void OnOpen() { }
    }
}
