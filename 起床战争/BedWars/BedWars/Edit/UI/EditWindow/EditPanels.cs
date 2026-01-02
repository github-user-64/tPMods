using BedWars.Edit.UI.EditItem_DisplayData;
using BedWars.Edit.UI.EditItem_EditData;
using BedWars.Edit.UI.EditItem_File;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;

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

            string t = "[c/aaffaa:进入游戏后需要重新加载]"
                + "\n[c/aaffaa:退出后不会保存所以记得][c/ff1111:保存][c/aaffaa:哦:p]";

            UIText text = new UIText(t, 0.7f);
            text.Width.Precent = 1;
            text.MarginTop = 10;
            text.TextOriginX = 0;
            AddChild(text);
        }
    }

    internal class EditPanel1 : UIScrollViewer2
    {
        public EditPanel1()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new DisplayMapPosSize("显示地图位置大小"));
            AddChild(new DisplaySpawItem("显示生成物品位置"));
            AddChild(new DisplayTeam("显示队伍位置"));
            AddChild(new DisplayCanActionTile("显示可交互图格"));
        }
    }

    internal class EditPanel2 : UIScrollViewer2
    {
        public EditPanel2()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            AddChild(new TpMapPos("传送", "传送到地图位置"));
            AddChild(new EditMapPos("设置地图位置"));
            AddChild(new EditMapSize("设置地图大小"));
            AddChild(new EditStartGameMinPlay("开始所需玩家"));
            AddChild(new EditTime("维持时间"));
        }
    }

    internal class EditPanel3 : UIScrollViewer2
    {
        public EditPanel3()
        {
            Width.Precent = 1;
            Height.Precent = 1;

            var ico1 = Main.Assets.Request<Texture2D>("Images/Item_30", AssetRequestMode.ImmediateLoad).Value;
            var ico2 = Main.Assets.Request<Texture2D>("Images/Item_171", AssetRequestMode.ImmediateLoad).Value;

            AddChild(new EditCopyTile("复制", ico1, "复制图格"));
            AddChild(new EditPlaceTile("放置", ico1, "放置图格"));
            AddChild(new EditCopyData("复制", ico2, "复制方块数据"));
            AddChild(new EditPasteData("粘贴", ico2, "粘贴方块数据"));
            AddChild(new EditTileCanAction("添加可交互方块"));
            AddChild(new EditTileNoCanAction("删除可交互方块"));

            string t = "[c/aaffaa:可交互位置的方块能被:破坏,放置,交互]"
                + "\n[c/aaffaa:可交互方块的显示是默认关闭的]"
                + "\n[c/aaffaa:别搞太多可交互方块]";

            UIText text = new UIText(t, 0.7f);
            text.Width.Precent = 1;
            text.MarginTop = 10;
            text.TextOriginX = 0;
            AddChild(text);
        }
    }

    internal class EditPanel4 : EditSpawItem.EditPanel
    {

    }

    internal class EditPanel5 : EditTeam.EditPanel
    {

    }
}
