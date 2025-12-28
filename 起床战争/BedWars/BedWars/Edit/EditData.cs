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

        public string SetMapPos(Point pos)
        {
            if (Data == null) return "地图数据为null";
            if (DataCheck.InWorld_MapInfoPos(pos) == false) return "地图位置超出世界";

            int offX = pos.X - DataInfo.pos.X;
            int offY = pos.Y - DataInfo.pos.Y;
            DataInfo.pos = pos;

            DataSpawItems.ForEach(i =>
            {
                i.pos.X += offX;
                i.pos.Y += offY;
            });

            return null;
        }

        public string SetMapSize(Point size)
        {
            if (Data == null) return "地图数据为null";
            if (DataCheck.InWorld_MapInfoSize(Data.Info.pos, size) == false) return "地图大小超出世界";
            if (size.X < 2) return "大小不能小于2";
            if (size.Y < 2) return "大小不能小于2";

            DataInfo.size = size;

            _ = DataSpawItems.RemoveAll(i => Data.InMap(i.pos) == false);

            return null;
        }

        public string AddSpawItem(Point pos)
        {
            if (Data == null) return "地图数据为null";
            if (Data.InMap(pos) == false) return "生成物品超出地图";

            SpawItemData temp = new SpawItemData();
            temp.mapData = Data;
            temp.pos = pos;
            Data.SpawItems.Add(temp);

            return null;
        }

        public string DelSpawItem(SpawItemData data)
        {
            if (Data == null) return "地图数据为null";

            Data.SpawItems.Remove(data);

            return null;
        }
    }
}
