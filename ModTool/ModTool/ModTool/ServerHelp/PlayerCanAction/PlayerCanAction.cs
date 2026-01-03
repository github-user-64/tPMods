using Microsoft.Xna.Framework;
using ModTool.PatchGame.PNetMessage_SendData;
using ModTool.Utils;
using ModTool.Utils.GetDataEventArgs;
using System.Collections.Generic;
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
            PatchGame.PMessageBuffer.OnCanGetData.Add(asd);

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
            kv.Add(MessageID.Unknown47, CanEditSign);
            kv.Add(MessageID.LockAndUnlock, CanLockAndUnlock);
            kv.Add(MessageID.BugCatching, CanBugCatching);
            kv.Add(MessageID.BugReleasing, CanBugReleasing);
            kv.Add(MessageID.SpawnBossUseLicenseStartEvent, CanSpawnBossUseLicenseStartEvent);
            kv.Add(MessageID.RequestTeleportationByServer, CanRequestTeleportationByServer);
            kv.Add(MessageID.TeleportEntity, CanTeleportEntity);
            kv.Add(MessageID.DamageNPC, CanDamageNPC);
            kv.Add(MessageID.PlayerHurtV2, CanPlayerHurtV2);

            //Utils.ServerSideCharacter(true);

            //OnCanPlayerHurtV2 += e =>
            //{
            //    if (e.player.inventory[0].type == 0) return true;
            //    ContentPatch.PrintTry($":222");
            //    return false;
            //};
        }

        private static bool asd(MessageBuffer This, int start, int length, int messageType)
        {
            if (Main.netMode != 2) return true;

            if (Main.player.IndexInRange(This.whoAmI) != true) return true;
            Player player = Main.player[This.whoAmI];
            if (player == null) return true;

            CanGetDataEvent foo = kv.GetVal(messageType, null);
            if (foo == null) return true;

            bool v = foo(player, This, start, length, messageType);

            return v;
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
                if (PWorldData.ServerSideCharacter == false) return;//启用服务端角色时有效
                NetMessage.TrySendData(MessageID.PlayerControls, This.whoAmI, -1, null, player.whoAmI);
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
            SendTileSquareEventArgs e = This.Unknown20_SendTileSquare(player);//发送多图格方块数据

            return OnCanSendTileSquare.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, e.sizeX, e.sizeY);
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

            return OnCanWeaponsRackTryPlacing.Call(e);
        }

        private static bool CanFoodPlatterTryPlacing(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            FoodPlatterTryPlacingEventArgs e = This.FoodPlatterTryPlacing(player);//食物盘子放置物品

            return OnCanFoodPlatterTryPlacing.Call(e);
        }

        private static bool CanRequestChestOpen(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            RequestChestOpenEventArgs e = This.RequestChestOpen(player);//请求打开箱子

            return OnCanRequestChestOpen.Call(e);
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
                if (PWorldData.ServerSideCharacter == false) return;//启用服务端角色时有效
                NetMessage.TrySendData(MessageID.PlayerBuffs, This.whoAmI, -1, null, player.whoAmI);
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

        private static bool CanEditSign(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            EditSignEventArgs e = This.Unknown47_EditSign(player);//编辑告示牌

            return OnCanEditSign.Call(e, () =>
            {
                NetMessage.TrySendData(MessageID.Unknown47, This.whoAmI, -1, null, e.signIndex, e.whoAmI);
            });
        }

        private static bool CanLockAndUnlock(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            LockAndUnlockEventArgs e = This.LockAndUnlock(player);//上锁开锁

            return OnCanLockAndUnlock.Call(e, () =>
            {
                NetMessage.SendTileSquare(This.whoAmI, e.x, e.y, 2);
            });
        }

        private static bool CanBugCatching(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            BugCatchingEventArgs e = This.BugCatching(player);//抓住动物

            return OnCanBugCatching.Call(e);
        }

        private static bool CanBugReleasing(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            BugReleasingEventArgs e = This.BugReleasing(player);//释放动物

            return OnCanBugReleasing.Call(e);
        }

        private static bool CanSpawnBossUseLicenseStartEvent(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            SpawnBossUseLicenseStartEventEventArgs e = This.SpawnBossUseLicenseStartEvent(player);//生成boss, 使用许可证, 开始事件

            return OnCanSpawnBossUseLicenseStartEvent.Call(e);
        }

        private static bool CanRequestTeleportationByServer(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            RequestTeleportationByServerEventArgs e = This.RequestTeleportationByServer(player);//传送(随机,魔法海螺,恶魔海螺,贝壳电话世界重生点)

            return OnCanRequestTeleportationByServer.Call(e);
        }

        private static bool CanTeleportEntity(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            TeleportEntityEventArgs e = This.TeleportEntity(player);//传送实体

            return OnCanTeleportEntity.Call(e, () =>
            {
                if (e.type != 0) return;

                Vector2 pos = player.position;
                NetMessage.TrySendData(MessageID.TeleportEntity, This.whoAmI, -1, null, e.type, e.player.whoAmI, pos.X, pos.Y, e.style, 0, e.extraInfo);
            });
        }

        private static bool CanDamageNPC(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            DamageNPCEventArgs e = This.DamageNPC(player);//伤害npc

            return OnCanDamageNPC.Call(e);
        }

        private static bool CanPlayerHurtV2(Player player, MessageBuffer This, int start, int length, int messageType)
        {
            PlayerHurtV2EventArgs e = This.PlayerHurtV2(player);//伤害玩家

            return OnCanPlayerHurtV2.Call(e);
        }
    }
}
