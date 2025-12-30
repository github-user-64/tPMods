using BedWars.Edit.UI.EditItem_EditData;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel1 : UIScrollViewer2, IPanel
    {
        public EditPanel1()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new TpMapPos("传送", "传送到地图位置"));
            AddChild(new EditMapPos("设置地图位置"));
            AddChild(new EditMapSize("设置地图大小"));
        }

        public void OnOpen() { }
    }
}
