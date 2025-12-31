using Terraria;

namespace BedWars.BedWarsData
{
    public class TileData : ICheck
    {
        /// <summary>
        /// 可交互图格, 让玩家可以交互这个位置的图格
        /// </summary>
        public bool canAction = false;
        public bool active = false;//有图格
        public ushort type;//方块类型
        public short frameX;
        public short frameY;
        public ushort wall;//墙
        public byte liquid;//液体
        public int liquidType;
        public bool halfBrick;//半砖
        public byte slope;//坡
        public bool actuator;//制动器
        public bool inActive;//应该是是否被制动器了
        public bool wire;//线
        public bool wire2;
        public bool wire3;
        public bool wire4;
        public byte color;//漆

        public void Check(MapData mapData)
        {

        }

        public static void Copy(TileData data, Tile tile)
        {
            data.active = tile.active();
            data.type = tile.type;
            data.frameX = tile.frameX;
            data.frameY = tile.frameY;
            data.wall = tile.wall;
            data.liquid = tile.liquid;
            data.liquidType = tile.liquidType();
            data.halfBrick = tile.halfBrick();
            data.slope = tile.slope();
            data.actuator = tile.actuator();
            data.inActive = tile.inActive();
            data.wire = tile.wire();
            data.wire2 = tile.wire2();
            data.wire3 = tile.wire3();
            data.wire4 = tile.wire4();
            data.color = tile.color();
        }

        public static void Place(TileData data, Tile tile)
        {
            tile.active(data.active);
            tile.type = data.type;
            tile.frameX = data.frameX;
            tile.frameY = data.frameY;
            tile.wall = data.wall;
            tile.liquid = data.liquid;
            tile.liquidType(data.liquidType);
            tile.halfBrick(data.halfBrick);
            tile.slope(data.slope);
            tile.actuator(data.actuator);
            tile.inActive(data.inActive);
            tile.wire(data.wire);
            tile.wire2(data.wire2);
            tile.wire3(data.wire3);
            tile.wire4(data.wire4);
            tile.color(data.color);
        }
    }
}
