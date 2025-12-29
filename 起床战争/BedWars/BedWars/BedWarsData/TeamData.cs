using Microsoft.Xna.Framework;
using System;

namespace BedWars.BedWarsData
{
    public class TeamData : ICheck
    {
        /// <summary>
        /// 该队伍最大玩家数量
        /// </summary>
        public int maxPlay = 99;
        /// <summary>
        /// 玩家的<see cref="Terraria.Player.team"/>, 0:无,1:红,2:绿,3:蓝,4:黄,5:紫
        /// </summary>
        public int team = 0;
        /// <summary>
        /// 能否重生
        /// </summary>
        public bool canSpaw = true;
        /// <summary>
        /// 重生位置
        /// </summary>
        public Point spawPos;
        /// <summary>
        /// 判断能否重生的方块位置
        /// </summary>
        public Point spawTilePos;

        public void Check(MapData mapData)
        {
            if (mapData.InMap(spawPos) == false) throw new Exception("重生位置超出地图");
            if (mapData.InMap(spawTilePos) == false) throw new Exception("重生方块超出地图");
        }
    }
}
