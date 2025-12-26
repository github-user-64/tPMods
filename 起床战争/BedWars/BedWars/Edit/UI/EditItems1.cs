using BedWars.Edit.UI.EditItem_DisplayData;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class EditItems1 : UIScrollViewer2, IPanel
    {
        public EditItems1()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new DisplayMapPosSize("显示地图位置大小"));
        }

        public void OnOpen() { }
    }
}
