using BedWars.Edit.UI.EditItem_DisplayData;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel2 : UIScrollViewer2, IPanel
    {
        public EditPanel2()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new DisplayMapPosSize("显示地图位置大小"));
            AddChild(new DisplaySpawItem("显示生成物品位置"));
        }

        public void OnOpen() { }
    }
}
