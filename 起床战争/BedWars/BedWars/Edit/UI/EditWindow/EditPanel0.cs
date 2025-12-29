using BedWars.Edit.UI.EditItem_EditData;
using BedWars.Edit.UI.EditItem_File;
using BedWars.Edit.UI.Elements;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel0 : UIPanelSVEditControl, IPanel
    {
        public EditPanel0()
        {
            AddItem(new UpdateModConfig("更新", "更新模组配置"));
            AddItem(new MapNew("新建", "新建地图"));
            AddItem(new MapLoad("加载", "加载地图"));
            AddItem(new MapSave("保存", "保存地图"));
            AddItem(new TpMapPos("传送", "传送到地图位置"));
            AddItem(new EditMapPos("设置地图位置"));
            AddItem(new EditMapSize("设置地图大小"));
        }

        public void OnOpen() { }
    }
}
