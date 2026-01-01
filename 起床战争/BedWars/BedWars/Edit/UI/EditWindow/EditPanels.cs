using BedWars.Edit.UI.EditItem_DisplayData;
using BedWars.Edit.UI.EditItem_EditData;
using BedWars.Edit.UI.EditItem_File;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditPanel0 : UIScrollViewer2
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
    }

    internal class EditPanel1 : UIScrollViewer2
    {
        public EditPanel1()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new TpMapPos("传送", "传送到地图位置"));
            AddChild(new EditMapPos("设置地图位置"));
            AddChild(new EditMapSize("设置地图大小"));
        }
    }

    internal class EditPanel2 : UIScrollViewer2
    {
        public EditPanel2()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new DisplayMapPosSize("显示地图位置大小"));
            AddChild(new DisplaySpawItem("显示生成物品位置"));
            AddChild(new DisplayTeam("显示队伍位置"));
            AddChild(new DisplayCanActionTile("显示可交互图格"));
        }
    }

    internal class EditPanel3 : EditSpawItem.EditPanel
    {

    }

    internal class EditPanel4 : EditTeam.EditPanel
    {

    }

    internal class EditPanel5 : UIScrollViewer2
    {
        public EditPanel5()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new EditCopyTile("复制", null, "复制图格数据"));
            AddChild(new EditPlaceTile("放置", null, "放置图格"));
            AddChild(new EditTileCanAction("添加可交互方块"));
            AddChild(new EditTileNoCanAction("删除可交互方块"));
        }
    }
}
