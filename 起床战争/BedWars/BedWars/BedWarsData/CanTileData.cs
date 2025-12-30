using Microsoft.Xna.Framework;

namespace BedWars.BedWarsData
{
    /// <summary>
    /// 玩家能交互的图格
    /// </summary>
    public class CanTileData : ICheck
    {
        /// <summary>
        /// 相对位置
        /// </summary>
        public Point pos;

        public void Check(MapData mapData)
        {
            //应该没差,只是用来判断这个位置能否交互方块而已
        }
    }
}
