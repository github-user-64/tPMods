using System;

namespace BedWars.BedWarsData
{
    /// <summary>
    /// 玩家能交互的图格
    /// </summary>
    public class CanTileData : ICheck
    {
        public int x;
        public int y;

        public void Check(MapData mapData)
        {
            if (mapData.InMap(new Microsoft.Xna.Framework.Point(x, y)) == false)
            {
                throw new Exception($"能交互图格超出地图:{x},{y}");
            }
        }
    }
}
