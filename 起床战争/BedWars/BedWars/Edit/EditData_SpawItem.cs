using BedWars.BedWarsData;
using Microsoft.Xna.Framework;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string AddSpawItem(Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

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

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SetSpawItemPos(SpawItemData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.pos = pos;

            return null;
        }
    }
}
