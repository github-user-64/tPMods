using Microsoft.Xna.Framework;
using System;

namespace BedWars.BedWarsData
{
    public class TeamData : ICheck
    {
        public string name = null;
        /// <summary>
        /// 该队伍最大玩家数量
        /// </summary>
        public int maxPlay = 99;
        /// <summary>
        /// 玩家的<see cref="Terraria.Player.team"/>, 0:无,1:红,2:绿,3:蓝,4:黄,5:粉
        /// </summary>
        public int team = 0;
        /// <summary>
        /// 能否重生
        /// </summary>
        public bool canSpaw = true;
        /// <summary>
        /// 判断能否重生的方块位置, 相对位置
        /// </summary>
        public Point spawTilePos;
        /// <summary>
        /// 重生位置, 相对位置
        /// </summary>
        public Point spawPos;

        public void Check(MapData mapData)
        {
            if (mapData.InMapRelative(spawTilePos) == false) throw new Exception("重生方块超出地图");
            if (mapData.InMapRelative(spawPos) == false) throw new Exception("重生位置超出地图");
        }
    }
}
