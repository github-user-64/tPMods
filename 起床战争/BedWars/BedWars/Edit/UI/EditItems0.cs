using BedWars.Edit.UI.EditItem;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class EditItems0 : UIScrollViewer2
    {
        public EditItems0()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new EditMapPos("设置地图位置"));
            AddChild(new EditMapSize("设置地图大小"));
        }
    }
}
