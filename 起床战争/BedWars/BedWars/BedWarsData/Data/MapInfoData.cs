using Microsoft.Xna.Framework;
using System;

namespace BedWars.BedWarsData
{
    public class MapInfoData : ICheck
    {
        public Point pos;
        public Point size;
        /// <summary>
        /// 重生点, 相对位置
        /// </summary>
        public Point spawPos;
        public int startGameMinPlay = 2;//开始游戏所需最小玩家数
        //游戏1分钟等于1秒
        //游戏1时=60秒
        //游戏60tick等于1秒
        //游戏1时=60*60=3600tick
        //3600=1时
        public double time = 3600 * (12 - 4);//维持时间, 小于0不维持
        //白天, 4:30到7:30
        public bool dayTime = true;

        public void Check(MapData mapData)
        {
            if (DataCheck.InWorld(pos) == false) throw new Exception("地图位置超出世界");

            if (size.X < 2) throw new Exception("地图大小不能小于2");
            if (size.Y < 2) throw new Exception("地图大小不能小于2");

            if (DataCheck.InWorldSize(pos, size) == false) throw new Exception("地图大小超出世界");

            if (mapData.InMapRelative(spawPos) == false) throw new Exception("重生点超出地图");
        }
    }
}
