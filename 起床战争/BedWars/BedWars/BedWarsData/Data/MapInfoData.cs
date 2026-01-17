using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace BedWars.BedWarsData
{
    public class MapInfoData : ICheck
    {
        /// <summary>
        /// 地图范围
        /// </summary>
        [JsonConverter(typeof(RectangleJsonConverter))]
        public Rectangle rect;
        [JsonIgnore]
        public int X => rect.X;
        [JsonIgnore]
        public int Y => rect.Y;
        [JsonIgnore]
        public Point Pos => new Point(rect.X, rect.Y);
        [JsonIgnore]
        public Point Size => new Point(rect.Width, rect.Height);
        [JsonIgnore]
        public int Width => rect.Width;
        [JsonIgnore]
        public int Height => rect.Height;
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
        /// <summary>
        /// 虚空高度, 从地图底部开始算, 低于虚空高度的玩家会掉血, 高度小于1不生效
        /// </summary>
        public int voidHeight = 0;
        /// <summary>
        /// 玩家死亡掉落物品
        /// </summary>
        public bool playDeathLoot = false;
        /// <summary>
        /// 玩家重生到队伍时保留盔甲
        /// </summary>
        public bool playKeepArmor = true;

        public void Check(MapData mapData)
        {
            if (Width < 2) throw new Exception("地图大小不能小于2");
            if (Height < 2) throw new Exception("地图大小不能小于2");

            if (DataCheck.InWorld(rect) == false) throw new Exception("地图超出世界");

            if (mapData.InMapRelative(spawPos) == false) throw new Exception("重生点超出地图");

            if (voidHeight < 0) voidHeight = 0;
            else if (voidHeight > Height) voidHeight = Height;
        }
    }
}
