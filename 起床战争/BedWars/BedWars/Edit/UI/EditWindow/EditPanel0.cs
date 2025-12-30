using BedWars.Edit.UI.EditItem_File;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel0 : UIScrollViewer2, IPanel
    {
        public EditPanel0()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new UpdateModConfig("更新", "更新模组配置"));
            AddChild(new MapNew("新建", "新建地图"));
            AddChild(new MapLoad("加载", "加载地图"));
            AddChild(new MapSave("保存", "保存地图"));
        }

        public void OnOpen() { }
    }
}
