using Microsoft.Xna.Framework;

namespace BedWars.BedWarsData
{
    public class TeamData
    {
        /// <summary>
        /// 该队伍最大玩家数量
        /// </summary>
        public int maxPlay = 4;
        /// <summary>
        /// 玩家的<see cref="Terraria.Player.team"/>, 0:无,1:红,2:绿,3:蓝,4:黄,5:紫
        /// </summary>
        public int team = 0;
        /// <summary>
        /// 重生位置
        /// </summary>
        public Point spawPos;
        /// <summary>
        /// 判断能否重生的方块类型, 小于0不能重生
        /// </summary>
        public int type = -1;
        /// <summary>
        /// 判断能否重生的方块位置
        /// </summary>
        public Point spawTile;
    }
}
