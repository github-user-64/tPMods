using ModTool.Utils;
using ModTool.Utils.GetDataEventArgs;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Net;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 玩家能否交互
    /// </summary>
    public static partial class PlayerCanAction
    {
        private delegate bool CanGetDataEvent(Player player, MessageBuffer This, int start, int length, int messageType);
        private static Dictionary<int, CanGetDataEvent> kv = null;

        internal static void Init()
        {
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(asd);

            kv = new Dictionary<int, CanGetDataEvent>();
            kv.Add(MessageID.SyncProjectile, CanNewProjectile);
            kv.Add(MessageID.SyncItem, CanNewItem);
            kv.Add(MessageID.TogglePVP, CanTogglePVP);
            kv.Add(MessageID.Unknown45, CanToggleTeam);
            kv.Add(MessageID.PlayerControls, CanControls);
            kv.Add(MessageID.TileManipulation, CanTileManipulation);
            kv.Add(MessageID.PlaceObject, CanPlaceObject);
            kv.Add(MessageID.TileEntityPlacement, CanTileEntityPlacement);
            kv.Add(MessageID.Unknown20, CanSendTileSquare);
            kv.Add(MessageID.ChestUpdates, CanChestUpdates);
            kv.Add(MessageID.HitSwitch, CanHitSwitch);
            kv.Add(MessageID.ItemFrameTryPlacing, CanItemFrameTryPlacing);
            kv.Add(MessageID.WeaponsRackTryPlacing, CanWeaponsRackTryPlacing);
            kv.Add(MessageID.FoodPlatterTryPlacing, CanFoodPlatterTryPlacing);
            kv.Add(MessageID.RequestChestOpen, CanRequestChestOpen);
            kv.Add(MessageID.QuickStackChests, CanQuickStackChests);
            kv.Add(MessageID.PlayerBuffs, CanPlayerBuffs);
            kv.Add(MessageID.LiquidUpdate, CanLiquidUpdate);
            kv.Add(MessageID.Unknown63, CanPaintTile);
            kv.Add(MessageID.Unknown64, CanPaintWall);

            //Utils.ServerSideCharacter(true);

            OnCanPaintTile += e =>
            {
                if (e.player.inventory[0].type == 0) return true;
                ContentPatch.PrintTry($":000");
                return false;
            };
        }

        private static bool asd(MessageBuffer This, int start, int length, int messageType)
        {
            if (Main.netMode != 2) return true;

            if (Main.player.IndexInRange(This.whoAmI) != true) return true;
            Player player = Main.player[This.whoAmI];
            if (player == null) return true;

            CanGetDataEvent foo = kv.GetVal(messageType, null);
            if (foo == null) return true;

            return foo(player, This, start, length, messageType);
        }

        private static bool CanNewProjectile(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            SyncProjectileEventArgs e = This.SyncProjectile(player);//同步射弹

            return OnCanNewProjectile.Call(e, () =>
            {
                for (int index = 0; index < Main.projectile.Length; ++index)
                {
                    Projectile proj = Main.projectile[index];

                    if (proj.active) continue;

                    proj.identity = e.identity;
                    proj.type = ProjectileID.None;
                    proj.owner = e.owner;

                    NetMessage.TrySendData(MessageID.SyncProjectile, This.whoAmI, -1, null, index);
                    break;
                }
            });
        }

        private static bool CanNewItem(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            SyncItemEventArgs e = This.SyncItem(player);//同步物品
            if (e.index != Main.item.Length - 1) return true;//客户端生成物品时index是列表最后一个

            return OnCanNewItem.Call(e, null);
        }

        private static bool CanTogglePVP(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            TogglePVPEventArgs e = This.TogglePVP(player);//切换pvp

            return OnCanTogglePVP.Call(e, () =>
            {
                NetMessage.TrySendData(MessageID.TogglePVP, This.whoAmI, -1, null, player.whoAmI);
            });
        }

        private static bool CanToggleTeam(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            ToggleTeamEventArgs e = This.Unknown45_ToggleTeam(player);//切换队伍

            return OnCanToggleTeam.Call(e, () =>
            {
                NetMessage.TrySendData(MessageID.Unknown45, This.whoAmI, -1, null, player.whoAmI);
            });
        }

        private static bool CanControls(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            ControlsEventArgs e = This.PlayerControls(player);//玩家控制

            return OnCanControls.Call(e, () =>
            {
                NetMessage.TrySendData(MessageID.PlayerControls, This.whoAmI, -1, null, player.whoAmI);//启用服务端角色时有效
            });
        }

        private static bool CanTileManipulation(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            TileManipulationEventArgs e = This.TileManipulation(player);//操作方块

            return OnCanTileManipulation.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanPlaceObject(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            PlaceObjectEventArgs e = This.PlaceObject(player);//放置对象

            return OnCanPlaceObject.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanTileEntityPlacement(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            TileEntityPlacementEventArgs e = This.TileEntityPlacement(player);//放置实体方块

            return OnCanTileEntityPlacement.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanSendTileSquare(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            SendTileSquareEventArgs e = This.SendTileSquare(player);//发送多图格方块数据

            return OnCanSendTileSquare.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanChestUpdates(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            ChestUpdatesEventArgs e = This.ChestUpdates(player);//箱子放置破坏

            return OnCanChestUpdates.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanHitSwitch(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            HitSwitchEventArgs e = This.HitSwitch(player);//点击开关

            return OnCanHitSwitch.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 5);
            });
        }

        private static bool CanItemFrameTryPlacing(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            ItemFrameTryPlacingEventArgs e = This.ItemFrameTryPlacing(player);//物品框放置物品

            return OnCanItemFrameTryPlacing.Call(e, () =>
            {
                int num = TEItemFrame.Find(e.x, e.y);
                if (num == -1) return;
                TEItemFrame tEItemFrame = (TEItemFrame)TileEntity.ByID[num];
                if (tEItemFrame == null) return;
                NetMessage.TrySendData(MessageID.TileEntitySharing, This.whoAmI, -1, null, tEItemFrame.ID, e.x, e.y);
            });
        }

        private static bool CanWeaponsRackTryPlacing(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            WeaponsRackTryPlacingEventArgs e = This.WeaponsRackTryPlacing(player);//武器架放置物品

            return OnCanWeaponsRackTryPlacing.Call(e, null);
        }

        private static bool CanFoodPlatterTryPlacing(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            FoodPlatterTryPlacingEventArgs e = This.FoodPlatterTryPlacing(player);//食物盘子放置物品

            return OnCanFoodPlatterTryPlacing.Call(e, null);
        }

        private static bool CanRequestChestOpen(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            RequestChestOpenEventArgs e = This.RequestChestOpen(player);//请求打开箱子

            return OnCanRequestChestOpen.Call(e, null);
        }

        private static bool CanQuickStackChests(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            QuickStackChestsEventArgs e = This.QuickStackChests(player);//快速堆叠到箱子

            return OnCanQuickStackChests.Call(e, () =>
            {
                if (e.player.inventory?.IndexInRange(e.slot) != true) return;

                NetMessage.TrySendData(MessageID.SyncEquipment, This.whoAmI, -1, null, e.player.whoAmI, e.slot, e.player.inventory[e.slot].prefix);
            });
        }
        
        private static bool CanPlayerBuffs(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            PlayerBuffsEventArgs e = This.PlayerBuffs(player);//玩家buffs

            return OnCanPlayerBuffs.Call(e, () =>
            {
                NetMessage.TrySendData(MessageID.PlayerBuffs, This.whoAmI, -1, null, player.whoAmI);//启用服务端角色时有效
            });
        }

        private static bool CanLiquidUpdate(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            LiquidUpdateEventArgs e = This.LiquidUpdate(player);//液体更新

            return OnCanLiquidUpdate.Call(e, () =>
            {
                if (WorldGen.InWorld(e.x, e.y) == false) return;

                //NetMessage.TrySendData(MessageID.LiquidUpdate, This.whoAmI, -1, null, e.x, e.y);

                HashSet<int> ints = new HashSet<int>();
                ints.Add(((e.x & 0xFFFF) << 16) | (e.y & 0xFFFF));

                NetPacket pack = NetLiquidModule.Serialize(ints);
                NetManager.Instance.SendToClient(pack, This.whoAmI);
            });
        }

        private static bool CanPaintTile(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            PaintTileEventArgs e = This.Unknown63_PaintTile(player);//油漆方块

            return OnCanPaintTile.Call(e, () =>
            {
                if (WorldGen.InWorld(e.x, e.y) == false) return;
                Tile tile = Main.tile[e.x, e.y];
                NetMessage.TrySendData(MessageID.Unknown63, This.whoAmI, -1, null, e.x, e.y, tile.color());
            });
        }

        private static bool CanPaintWall(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            PaintWallEventArgs e = This.Unknown64_PaintWall(player);//油漆墙

            return OnCanPaintWall.Call(e, () =>
            {
                if (WorldGen.InWorld(e.x, e.y) == false) return;
                Tile tile = Main.tile[e.x, e.y];
                NetMessage.TrySendData(MessageID.Unknown64, This.whoAmI, -1, null, e.x, e.y, tile.wallColor());
            });
        }

        //private static bool Can(Player player, MessageBuffer This, int start, int length, int messageType)
        //{
        //    PaintWallEventArgs e = This.Unknown64_PaintWall(player);//油漆墙

        //    return OnCanPaintWall.Call(e, () =>
        //    {

        //    });
        //}

        //到时候给有什么什么类型的[OnCanXXX]加上特性, 在Call里判断有对应特性的直接转到对应的[OnCan特性]里并直接调用isFalse
        //锁和开锁箱子
        //放置npc
        //boss
    }
}
