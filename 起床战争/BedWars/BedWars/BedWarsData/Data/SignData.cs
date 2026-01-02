using System;
using Terraria;

namespace BedWars.BedWarsData
{
    public class SignData : ICheck
    {
        /// <summary>
        /// 告示牌左上角x, 相对位置
        /// </summary>
        public int x;
        /// <summary>
        /// 告示牌左上角y, 相对位置
        /// </summary>
        public int y;
        public string text = null;

        public void Check(MapData mapData)
        {
            if (mapData.InMapRelative(x, y) == false) throw new Exception("告示牌超出地图");
        }

        public void Copy(MapData mapData, Sign sign)
        {
            x = sign.x - mapData.Info.pos.X;
            y = sign.y - mapData.Info.pos.Y;
            text = sign.text;
        }

        public void Paste(MapData mapData)
        {
            if (mapData.InMapRelative(x, y) == false) return;

            int tilex = mapData.Info.pos.X + x;
            int tiley = mapData.Info.pos.Y + y;

            int index = Sign.ReadSign(tilex, tiley, true);
            if (index < 0) return;

            Main.sign[index].text = text;
        }
    }
}
