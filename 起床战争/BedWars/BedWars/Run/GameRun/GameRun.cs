using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    //加载地图数据在服务端加载模组完成后
    //运行状态分为:没有,初始化地图,准备游戏,游戏中,游戏结束
    //
    //设为幽灵状态:重生,设为幽灵,无队伍,禁用pvp,设置到进入游戏位置,清空背包
    //
    //没有:完全不做任何处理
    //-除此之外的状态:不能自然生成npc,维持时间
    //--放置图格只能在:游戏中 && 地图范围内 && (没图格 || 可交互图格)
    //--破坏图格只能在:游戏中 && 玩家放置图格 || 可交互图格
    //--未登录玩家不可交互
    //--玩家进入时设为幽灵状态
    //--不能设置队伍,pvp,幽灵状态
    //
    //初始化地图:
    //-进入时:设置玩家队伍,禁用pvp,所有玩家重生到进入游戏位置,清空玩家背包,清空玩家放置图格列表,清理图格,放置图格,进入准备游戏
    //
    //准备游戏:
    //-进入时:清空掉落物,清空玩家背包,添加登录玩家到列表
    //-玩家登录时:添加登录玩家到列表
    //-添加登录玩家到列表:
    //--处于幽灵状态:正常状态,重生到进入游戏位置
    //-不能交互图格
    //-最小玩家数量不够时等待,够时进入计时
    //-时间到进入游戏中
    //
    //游戏中:
    //-进入时:分配队伍并添加到队伍数据,生成玩家到队伍
    //-生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置队伍pvp
    //-玩家重生时:生成玩家到队伍
    //-玩家退出,队伍不可重生时死亡,时从队伍删除
    //-当有队伍删除玩家时:
    //--设为幽灵状态
    //--如果只有一个队伍有人时进入游戏结束
    //--如果所有队伍都没玩家时进入初始化地图
    //
    //游戏结束:
    //-进入时:禁用pvp,在在队伍中的所有玩家位置生成烟花
    //-一段时间后进入初始化地图
    //
    public partial class GameRun
    {
        public const int StateNone = 0;
        public const int StateMapInit = 1;
        public const int StateReadyGame = 2;
        public const int StateGameing = 3;
        public const int StateGameEnd = 4;

        public static readonly GameRun instance = new GameRun();

        public MapData Data { get; protected set; } = null;
        public MapInfoData DataInfo => Data?.Info;
        public List<SpawItemData> DataSpawItems => Data?.SpawItems;
        public List<TeamData> DataTeams => Data?.Teams;
        public List<List<TileData>> DataTile => Data?.Tile;
        public List<ChestData> DataChests => Data?.Chests;
        public List<SignData> DataSigns => Data?.Signs;
        public InventoryData DataInventory => Data?.Inventory;
        public List<ShopData> DataShops => Data?.Shops;
        public GameTeam Team { get; protected set; } = null;
        //
        protected IStateAction[] States = null;
        public IStateAction NowState { get; protected set; } = null;
        public int NowStateType { get; protected set; } = StateNone;
        private Action HasUpdateState = null;
        public bool IsLoaded { get; protected set; } = false;
        public bool IsInited { get; protected set; } = false;

        private void Init()
        {
            Common.GameAction.mapData = Data;
            Common.GameAction.CanDropTombstone = false;//不能掉落墓碑
            Common.GameAction.CanSpawnNPC = false;//不能自然生成npc
            Common.GameAction.SyncTimeCD = 60 * 10;//维持时间
            Common.GameAction.DayTime = DataInfo.dayTime;//维持时间
            Common.GameAction.Time = DataInfo.time;//维持时间

            Main.spawnTileX = DataInfo.X + DataInfo.spawPos.X;
            Main.spawnTileY = DataInfo.Y + DataInfo.spawPos.Y;
            NetMessage.TrySendData(MessageID.WorldData);//防止已经有玩家加入

            DataTile.ForEach(i =>
            {
                i.ForEach(tile =>
                {
                    if (tile.active) return;
                    tile.CanActionTile = true;//没方块
                    tile.CanActionWall = tile.wall == WallID.None;//没方块没墙
                });
            });

            SetState(StateMapInit);
        }

        public void SpawnToPos(Player player, Point pos)
        {
            player.SpawnX = pos.X;
            player.SpawnY = pos.Y;
            player.respawnTimer = 0;
            //单独发给玩家也可以, 玩家会重新发数据回来
            NetMessage.TrySendData(MessageID.PlayerSpawn, player.whoAmI, -1, null,
                player.whoAmI, (float)PlayerSpawnContext.SpawningIntoWorld);

            player.Center = pos.ToWorldCoordinates();
            NetMessage.TrySendData(MessageID.PlayerControls);
        }

        /// <summary>
        /// 重生到进入游戏位置
        /// </summary>
        public void SpawnToMapSpaw(Player player)
        {
            Point pos = new Point(DataInfo.X + DataInfo.spawPos.X, DataInfo.Y + DataInfo.spawPos.Y);

            SpawnToPos(player, pos);
        }

        /// <summary>
        /// 设为幽灵状态:重生,设为幽灵,无队伍,禁用pvp,设置到进入游戏位置,清空背包
        /// </summary>
        public void SetPlayGhost(Player player)
        {
            SpawnToMapSpaw(player);

            player.ghost = true;
            NetMessage.TrySendData(MessageID.PlayerControls);

            SetTeamPvP(player, 0, false);

            ClearInventory(player);//清空物品栏
        }

        public void ClearInventory(Player player)
        {
            Utils.SetItemsSync(player, null);
        }

        public void ResetInventory(Player player)
        {
            Utils.SetItemsSync(player, DataInventory);
        }

        public void SetTeamPvP(Player player, int team, bool pvp)
        {
            if (player.team != team)
            {
                player.team = team;
                NetMessage.TrySendData(MessageID.Unknown45);
            }
            if (player.hostile != pvp)
            {
                player.hostile = pvp;
                NetMessage.TrySendData(MessageID.TogglePVP);
            }
        }

        public void ForActivePlayer(Action<Player> action)
        {
            if (action == null) return;

            foreach (Player i in Main.player)
            {
                if (i?.active != true) continue;
                action(i);
            }
        }

        public void ForTeam(Action<GameTeamData> action)
        {
            Team.ForTeam(action);
        }

        public void ForAllTeamPlay(Action<Player> action)
        {
            if (action != null) ForTeam(i => i.ForPlay(action));
        }

        /// <summary>
        /// 清除所有队伍的玩家
        /// </summary>
        public void ClearAllTeamPlayer()
        {
            Team.ClearPlayer();
        }

        public GameTeamData GetPlayerTeam(Player player)
        {
            return Team.GetTeam(player);
        }
    }
}
