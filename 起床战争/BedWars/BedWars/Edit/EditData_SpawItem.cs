using BedWars.BedWarsData;
using Microsoft.Xna.Framework;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SpawItemAdd(Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            SpawItemData temp = new SpawItemData();
            temp.mapData = Data;
            temp.pos = pos;

            DataSpawItems.Add(temp);

            return null;
        }

        public string SpawItemDel(SpawItemData data)
        {
            if (Data == null) return "地图数据为null";

            DataSpawItems.Remove(data);

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SpawItemSetPos(SpawItemData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.pos = pos;

            return null;
        }
    }
}
