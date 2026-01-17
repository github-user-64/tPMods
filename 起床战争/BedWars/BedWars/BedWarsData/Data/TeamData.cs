using Microsoft.Xna.Framework;
using Newtonsoft.Json;
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
        /// 判断能否重生的方块范围, 相对位置
        /// </summary>
        [JsonConverter(typeof(RectangleJsonConverter))]
        public Rectangle spawTile;
        /// <summary>
        /// 重生位置, 相对位置
        /// </summary>
        public Point spawPos;
        public int statLifeMax = 400;
        public int statManaMax = 20 * 2;

        public void Check(MapData mapData)
        {
            if (spawTile.Width < 1) throw new Exception("重生方块不能小于1");
            if (spawTile.Height < 1) throw new Exception("重生方块不能小于1");

            if (mapData.InMapRelative(spawTile) == false) throw new Exception("重生方块超出地图");
            if (mapData.InMapRelative(spawPos) == false) throw new Exception("重生位置超出地图");
        }
    }
}
