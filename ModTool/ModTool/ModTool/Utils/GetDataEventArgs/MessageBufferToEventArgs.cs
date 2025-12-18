using Microsoft.Xna.Framework;
using System.IO;
using Terraria;

namespace ModTool.Utils.GetDataEventArgs
{
    /// <summary>
    /// <see cref="MessageBuffer"/>转为对应事件参数, 调用后<see cref="Stream.Position"/>会改变, 所以有必要的话记得给<see cref="MessageBuffer"/>复位哦
    /// </summary>
    public static class MessageBufferToEventArgs
    {
        /// <summary/>
        public static SyncProjectileEventArgs SyncProjectile(this MessageBuffer This, Player player)
        {
            int identity = This.reader.ReadInt16();
            Vector2 position = This.reader.ReadVector2();
            Vector2 velocity = This.reader.ReadVector2();
            int owner = This.reader.ReadByte();
            int type = This.reader.ReadInt16();

            BitsByte bitsByte8 = This.reader.ReadByte();
            BitsByte bitsByte9 = (byte)(bitsByte8[2] ? This.reader.ReadByte() : 0);
            float[] array3 = new float[Projectile.maxAI];
            array3[0] = (bitsByte8[0] ? This.reader.ReadSingle() : 0f);
            array3[1] = (bitsByte8[1] ? This.reader.ReadSingle() : 0f);
            int bannerIdToRespondTo = (bitsByte8[3] ? This.reader.ReadUInt16() : 0);
            int damage2 = (bitsByte8[4] ? This.reader.ReadInt16() : 0);
            float knockBack2 = (bitsByte8[5] ? This.reader.ReadSingle() : 0f);
            int originalDamage = (bitsByte8[6] ? This.reader.ReadInt16() : 0);
            int num68 = (bitsByte8[7] ? This.reader.ReadInt16() : (-1));

            return new SyncProjectileEventArgs()
            {
                player = player,
                identity = identity,
                type = type,
                owner = owner,
                damage = damage2,
            };
        }
        /// <summary/>
        public static SyncItemEventArgs SyncItem(this MessageBuffer This, Player player)
        {
            SyncItemEventArgs e = new SyncItemEventArgs();

            e.player = player;
            e.index = This.reader.ReadInt16();
            e.position = This.reader.ReadVector2();
            e.velocity = This.reader.ReadVector2();
            e.stack = This.reader.ReadInt16();
            e.prefix = This.reader.ReadByte();
            e.ownIgnore = This.reader.ReadByte();
            e.netid = This.reader.ReadInt16();

            return e;
        }
        /// <summary/>
        public static TogglePVPEventArgs TogglePVP(this MessageBuffer This, Player player)
        {
            int _whoAmI = This.reader.ReadByte();
            bool hostile = This.reader.ReadBoolean();

            return new TogglePVPEventArgs()
            {
                player = player,
                hostile = hostile,
            };
        }
        /// <summary/>
        public static ToggleTeamEventArgs Unknown45_ToggleTeam(this MessageBuffer This, Player player)
        {
            int _whoAmI = This.reader.ReadByte();
            int team = This.reader.ReadByte();

            return new ToggleTeamEventArgs()
            {
                player = player,
                team = team,
            };
        }
        /// <summary/>
        public static ControlsEventArgs PlayerControls(this MessageBuffer This, Player player)
        {
            int _whoAmI = This.reader.ReadByte();
            BitsByte bs0 = This.reader.ReadByte();
            BitsByte bs1 = This.reader.ReadByte();
            BitsByte bs2 = This.reader.ReadByte();
            BitsByte bs3 = This.reader.ReadByte();
            int selectedItem = This.reader.ReadByte();
            Vector2 position = This.reader.ReadVector2();

            ControlsEventArgs e = new ControlsEventArgs();

            e.player = player;
            e.controlUp = bs0[0];
            e.controlDown = bs0[1];
            e.controlLeft = bs0[2];
            e.controlRight = bs0[3];
            e.controlJump = bs0[4];
            e.controlUseItem = bs0[5];
            e.position = position;

            return e;
        }
        /// <summary/>
        public static TileManipulationEventArgs TileManipulation(this MessageBuffer This, Player player)
        {
            TileManipulationEventArgs e = new TileManipulationEventArgs();

            e.player = player;
            e.manipulationType = This.reader.ReadByte();
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.tileType = This.reader.ReadInt16();
            e.placeStyle = This.reader.ReadByte();

            return e;
        }
        /// <summary/>
        public static PlaceObjectEventArgs PlaceObject(this MessageBuffer This, Player player)
        {
            PlaceObjectEventArgs e = new PlaceObjectEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.type = This.reader.ReadInt16();
            e.style = This.reader.ReadInt16();
            e.alternate = This.reader.ReadByte();
            e.random = This.reader.ReadSByte();
            e.direction = (This.reader.ReadBoolean() ? 1 : (-1));

            return e;
        }
        /// <summary/>
        public static TileEntityPlacementEventArgs TileEntityPlacement(this MessageBuffer This, Player player)
        {
            TileEntityPlacementEventArgs e = new TileEntityPlacementEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.type = This.reader.ReadByte();

            return e;
        }
        /// <summary/>
        public static SendTileSquareEventArgs SendTileSquare(this MessageBuffer This, Player player)
        {
            SendTileSquareEventArgs e = new SendTileSquareEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.sizeX = This.reader.ReadByte();
            e.sizeY = This.reader.ReadByte();
            e.changeType = This.reader.ReadByte();

            return e;
        }
        /// <summary/>
        public static ChestUpdatesEventArgs ChestUpdates(this MessageBuffer This, Player player)
        {
            ChestUpdatesEventArgs e = new ChestUpdatesEventArgs();

            e.player = player;
            e.updateType = This.reader.ReadByte();
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.style = This.reader.ReadInt16();
            e.id = This.reader.ReadInt16();

            return e;
        }
        /// <summary/>
        public static HitSwitchEventArgs HitSwitch(this MessageBuffer This, Player player)
        {
            HitSwitchEventArgs e = new HitSwitchEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();

            return e;
        }
        /// <summary>
        /// 自定义的
        /// </summary>
        public static T ItemTryPlacing<T>(this MessageBuffer This, Player player, T e) where T : ItemTryPlacingEventArgs
        {
            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.netid = This.reader.ReadInt16();
            e.prefix = This.reader.ReadByte();
            e.stack = This.reader.ReadInt16();

            return e;
        }
        /// <summary/>
        public static ItemFrameTryPlacingEventArgs ItemFrameTryPlacing(this MessageBuffer This, Player player)
        {
            return ItemTryPlacing(This, player, new ItemFrameTryPlacingEventArgs());
        }
        /// <summary/>
        public static WeaponsRackTryPlacingEventArgs WeaponsRackTryPlacing(this MessageBuffer This, Player player)
        {
            return ItemTryPlacing(This, player, new WeaponsRackTryPlacingEventArgs());
        }
        /// <summary/>
        public static FoodPlatterTryPlacingEventArgs FoodPlatterTryPlacing(this MessageBuffer This, Player player)
        {
            return ItemTryPlacing(This, player, new FoodPlatterTryPlacingEventArgs());
        }
        /// <summary/>
        public static RequestChestOpenEventArgs RequestChestOpen(this MessageBuffer This, Player player)
        {
            RequestChestOpenEventArgs e = new RequestChestOpenEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();

            return e;
        }
        /// <summary/>
        public static QuickStackChestsEventArgs QuickStackChests(this MessageBuffer This, Player player)
        {
            QuickStackChestsEventArgs e = new QuickStackChestsEventArgs();

            e.player = player;
            e.slot = This.reader.ReadInt16();

            return e;
        }
        /// <summary/>
        public static PlayerBuffsEventArgs PlayerBuffs(this MessageBuffer This, Player player)
        {
            PlayerBuffsEventArgs e = new PlayerBuffsEventArgs();

            e.player = player;

            return e;
        }
        /// <summary/>
        public static LiquidUpdateEventArgs LiquidUpdate(this MessageBuffer This, Player player)
        {
            LiquidUpdateEventArgs e = new LiquidUpdateEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e. liquid = This.reader.ReadByte();
            e. liquidType = This.reader.ReadByte();

            return e;
        }
        /// <summary/>
        public static PaintTileEventArgs Unknown63_PaintTile(this MessageBuffer This, Player player)
        {
            PaintTileEventArgs e = new PaintTileEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.color = This.reader.ReadByte();
            e.coat = This.reader.ReadByte();

            return e;
        }
        /// <summary/>
        public static PaintWallEventArgs Unknown64_PaintWall(this MessageBuffer This, Player player)
        {
            PaintWallEventArgs e = new PaintWallEventArgs();

            e.player = player;
            e.x = This.reader.ReadInt16();
            e.y = This.reader.ReadInt16();
            e.color = This.reader.ReadByte();
            e.coat = This.reader.ReadByte();

            return e;
        }
    }
}
