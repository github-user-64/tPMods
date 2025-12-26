using BedWars.Edit.UI.EditItem_EditData;
using BedWars.Edit.UI.EditItem_File;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI
{
    internal class EditItems0 : UIScrollViewer2, IPanel
    {
        public EditItems0()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new UpdateModConfig("更新", "更新模组配置"));
            AddChild(new MapNew("新建", "新建地图"));
            AddChild(new MapLoad("加载", "加载地图"));
            AddChild(new MapSave("保存", "保存地图"));
            AddChild(new EditMapPos("设置地图位置"));
            AddChild(new EditMapSize("设置地图大小"));
        }

        public void OnOpen() { }
    }
}
