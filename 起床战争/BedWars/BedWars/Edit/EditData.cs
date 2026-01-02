using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

        public string SetMapPos(Point pos)
        {
            if (Data == null) return "地图数据为null";
            if (DataCheck.InWorld(pos) == false) return "地图位置超出世界";
            if (DataCheck.InWorldSize(pos, Data.Info.size) == false) return "地图大小超出世界";

            DataInfo.pos = pos;

            return null;
        }

        public string SetMapSize(Point size)
        {
            if (Data == null) return "地图数据为null";
            if (DataCheck.InWorldSize(Data.Info.pos, size) == false) return "地图大小超出世界";
            if (size.X < 2) return "大小不能小于2";
            if (size.Y < 2) return "大小不能小于2";

            DataInfo.size = size;

            _ = DataSpawItems.RemoveAll(i => Data.InMapRelative(i.pos) == false);
            _ = DataTeams.RemoveAll(i => Data.InMapRelative(i.spawTilePos) == false || Data.InMapRelative(i.spawPos) == false);
            _ = DataChests.RemoveAll(i => Data.InMapRelative(i.x, i.y) == false);

            Data.RepairTile();

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置, <paramref name="pos"/>转为相对位置, 成功返回<see langword="null"/>
        /// </summary>
        public string CheckPos(ref Point pos)
        {
            if (DataInfo == null) return "地图数据为null";

            pos.X -= DataInfo.pos.X;
            pos.Y -= DataInfo.pos.Y;

            if (Data.InMapRelative(pos) == false) return "超出地图";

            return null;
        }
    }
}
