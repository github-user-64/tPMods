using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        public static readonly EditData instance = new EditData();

        public string DirMapData => ThisMod.DirMapData;
        /// <summary>
        /// 会为<see langword="null"/>
        /// </summary>
        public MapData Data { get; private set; } = null;
        public MapInfoData DataInfo => Data?.Info;
        public List<SpawItemData> DataSpawItems => Data?.SpawItems;
        public List<TeamData> DataTeams => Data?.Teams;
        public List<List<TileData>> DataTile => Data?.Tile;
        public List<ChestData> DataChests => Data?.Chests;
        public List<SignData> DataSigns => Data?.Signs;
        public InventoryData DataInventory => Data?.Inventory;
        public List<ShopData> DataShops => Data?.Shops;

        public string SetMapPos(Point pos)
        {
            if (Data == null) return "地图数据为null";

            Rectangle rect = DataInfo.rect;
            rect.X = pos.X;
            rect.Y = pos.Y;

            if (DataCheck.InWorld(rect) == false) return "地图超出世界";

            DataInfo.rect = rect;

            return null;
        }

        public string SetMapSize(Point size)
        {
            if (Data == null) return "地图数据为null";
            if (size.X < 2) return "大小不能小于2";
            if (size.Y < 2) return "大小不能小于2";

            Rectangle rect = DataInfo.rect;
            rect.Width = size.X;
            rect.Height = size.Y;

            if (DataCheck.InWorld(rect) == false) return "地图超出世界";

            DataInfo.rect = rect;

            if (DataInfo.spawPos.X >= DataInfo.Width) DataInfo.spawPos.X = DataInfo.Width - 1;
            if (DataInfo.spawPos.Y >= DataInfo.Height) DataInfo.spawPos.Y = DataInfo.Height - 1;

            _ = DataSpawItems.RemoveAll(i => Data.InMapRelative(i.pos) == false);
            _ = DataTeams.RemoveAll(i => Data.InMapRelative(i.spawTile) == false || Data.InMapRelative(i.spawPos) == false);
            _ = DataChests.RemoveAll(i => Data.InMapRelative(i.x, i.y) == false);
            _ = DataSigns.RemoveAll(i => Data.InMapRelative(i.x, i.y) == false);
            _ = DataShops.RemoveAll(i => Data.InMapRelative(i.pos) == false);

            if (DataInfo.voidHeight > DataInfo.Height) DataInfo.voidHeight = DataInfo.Height;

            Data.RepairTile();

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SetMapSpawPos(Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            DataInfo.spawPos = pos;

            return null;
        }

        public string SetVoidHeight(int height)
        {
            if (DataInfo == null) return "地图数据为null";

            if (height < 0) height = 0;
            else if (height > DataInfo.Height) height = DataInfo.Height;

            DataInfo.voidHeight = height;

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置, <paramref name="pos"/>转为相对位置, 成功返回<see langword="null"/>
        /// </summary>
        public string CheckPos(ref Point pos)
        {
            if (DataInfo == null) return "地图数据为null";

            pos.X -= DataInfo.X;
            pos.Y -= DataInfo.Y;

            if (Data.InMapRelative(pos) == false) return "超出地图";

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为相对位置
        /// </summary>
        public void Tp(Point pos)
        {
            Tp(new Rectangle(pos.X, pos.Y, 1, 1));
        }

        /// <summary>
        /// <paramref name="rect"/>为相对位置
        /// </summary>
        public void Tp(Rectangle rect)
        {
            if (DataInfo == null)
            {
                Main.NewText("地图数据为null");
                return;
            }

            Point point = new Point(DataInfo.X + rect.X, DataInfo.Y + rect.Y);

            Vector2 pos = point.ToWorldCoordinates(0, 0);
            pos += new Point(rect.Width, rect.Height).ToWorldCoordinates() / 2;

            point = pos.ToTileCoordinates();
            if (WorldGen.InWorld(point.X, point.Y) == false)
            {
                Main.NewText($"超出世界:{pos.X},{pos.Y}");
                return;
            }

            Main.LocalPlayer.Center = pos;
        }
    }
}
