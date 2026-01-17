using System.IO;
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
        public bool invisibleBlock;//漆回声
        public bool invisibleWall;//漆回声

        //
        /// <summary>
        /// 不保存. 可以操作图格, 图格
        /// </summary>
        public bool CanActionTile = false;
        /// <summary>
        /// 不保存. 可以操作图格, 墙
        /// </summary>
        public bool CanActionWall = false;

        public void Check(MapData mapData)
        {

        }

        public TileData Read(BinaryReader sr)
        {
            canAction = sr.ReadBoolean();
            active = sr.ReadBoolean();
            type = sr.ReadUInt16();
            frameX = sr.ReadInt16();
            frameY = sr.ReadInt16();
            wall = sr.ReadUInt16();
            liquid = sr.ReadByte();
            liquidType = sr.ReadInt32();
            halfBrick = sr.ReadBoolean();
            slope = sr.ReadByte();
            actuator = sr.ReadBoolean();
            inActive = sr.ReadBoolean();
            wire = sr.ReadBoolean();
            wire2 = sr.ReadBoolean();
            wire3 = sr.ReadBoolean();
            wire4 = sr.ReadBoolean();
            color = sr.ReadByte();
            invisibleBlock = sr.ReadBoolean();
            invisibleWall = sr.ReadBoolean();

            return this;
        }

        public void Writer(BinaryWriter sw)
        {
            sw.Write(canAction);
            sw.Write(active);
            sw.Write(type);
            sw.Write(frameX);
            sw.Write(frameY);
            sw.Write(wall);
            sw.Write(liquid);
            sw.Write(liquidType);
            sw.Write(halfBrick);
            sw.Write(slope);
            sw.Write(actuator);
            sw.Write(inActive);
            sw.Write(wire);
            sw.Write(wire2);
            sw.Write(wire3);
            sw.Write(wire4);
            sw.Write(color);
            sw.Write(invisibleBlock);
            sw.Write(invisibleWall);
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
            data.invisibleBlock = tile.invisibleBlock();
            data.invisibleWall = tile.invisibleWall();
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
            tile.invisibleBlock(data.invisibleBlock);
            tile.invisibleWall(data.invisibleWall);
        }
    }
}
