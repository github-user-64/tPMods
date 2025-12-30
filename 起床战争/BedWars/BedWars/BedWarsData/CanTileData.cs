using Microsoft.Xna.Framework;
using System;

namespace BedWars.BedWarsData
{
    /// <summary>
    /// 玩家能交互的图格
    /// </summary>
    public class CanTileData : ICheck
    {
        /// <summary>
        /// 相对于<see cref="MapInfoData.pos"/>
        /// </summary>
        public int x;
        /// <summary>
        /// 相对于<see cref="MapInfoData.pos"/>
        /// </summary>
        public int y;

        public void Check(MapData mapData)
        {
            Point pos = mapData.Info.pos;
            pos.X += x;
            pos.Y += y;

            if (mapData.InMap(pos) == false)
            {
                throw new Exception($"能交互图格超出地图:{pos.X},{pos.Y}");
            }
        }
    }
}
