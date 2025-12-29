using Microsoft.Xna.Framework;
using System;

namespace BedWars.BedWarsData
{
    public class MapInfoData : ICheck
    {
        public Point pos;
        public Point size;

        public void Check(MapData mapData)
        {
            if (DataCheck.InWorld(pos) == false) throw new Exception("地图位置超出世界");

            if (size.X < 2) throw new Exception("地图大小不能小于2");
            if (size.Y < 2) throw new Exception("地图大小不能小于2");

            if (DataCheck.InWorldSize(pos, size) == false) throw new Exception("地图大小超出世界");
        }
    }
}
