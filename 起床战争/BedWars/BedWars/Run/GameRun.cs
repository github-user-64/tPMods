using BedWars.BedWarsData;
using ModTool.Utils;
using ModTool.Utils.GetDataEventArgs;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    //加载地图数据在服务端加载模组完成后
    //运行状态分为:没有,初始化地图,准备游戏,游戏中,游戏结束
    //
    //设为幽灵状态:设为幽灵,无队伍,禁用pvp,设置到进入游戏位置,清空背包
    //
    //没有:完全不做任何处理
    //-除此之外的状态:不生成任何npc,维持时间,给玩家添加创意震撼buff
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
    //-进入时:分配队伍并添加到队伍数据,没队伍设为幽灵状态,生成玩家到队伍,启用pvp,取消添加buff
    //-生成玩家到队伍:没队伍设为幽灵状态,重生,设置位置,设置属性,清空设置背包,设置玩家队伍
    //-玩家重生时:生成玩家到队伍
    //-玩家退出,队伍不可重生时死亡,时从队伍删除
    //-当有队伍删除玩家时:
    //--设为幽灵状态
    //--如果只有一个队伍有人时进入游戏结束
    //--如果所有队伍都没玩家时进入初始化地图
    //
    //游戏结束:
    //-进入时:禁用pvp,在在队伍中的所有玩家位置生成烟花
    //-有玩家死亡时:生成玩家到队伍
    //-一段时间后进入初始化地图
    //
    internal class GameRun : IGameControl
    {
        public const int StateNone = 0;
        public const int StateMapInit = 1;

        public static readonly GameRun instance = new GameRun();

        protected IStateAction[] States = null;
        public IStateAction NowState { get; protected set; } = null;
        public int NowStateType { get; protected set; } = StateNone;
        public bool IsLoaded { get; protected set; } = false;
        //
        public MapData Data { get; protected set; } = null;
        public MapInfoData DataInfo => Data?.Info;
        public List<SpawItemData> DataSpawItems => Data?.SpawItems;
        public List<TeamData> DataTeams => Data?.Teams;
        public List<List<TileData>> DataTile => Data?.Tile;
        public List<ChestData> DataChests => Data?.Chests;
        public List<SignData> DataSigns => Data?.Signs;
        public InventoryData DataInventory => Data?.Inventory;
        public List<ShopData> DataShops => Data?.Shops;

        public void SetState(int key)
        {
            if (States?.IndexInRange(key) != true) return;

            NowState?.OnEnd();
            NowState = null;

            NowStateType = key;
            NowState = States[key];
            NowState?.OnStart();
        }

        void IGameControl.LoadTry(Action<string> print)
        {
            try
            {
                if (Main.netMode != 2)
                {
                    print?.Invoke("不是服务端");
                    return;
                }
                if (IsLoaded)
                {
                    print?.Invoke("已加载");
                    return;
                }

                print?.Invoke($"地图目录:{ThisMod.DirMapData}");
                print?.Invoke("加载地图数据");
                MapData temp = DataFileHelp.ReadData(ThisMod.DirMapData);

                print?.Invoke("检查数据");
                DataCheck.Repair(temp);
                DataCheck.CheckMapData(temp);

                ModTool.ServerHelp.Utils.ServerSideCharacter(true);

                States = new IStateAction[]
                {
                    null,
                    new SMapInit(),
                };

                Data = temp;

                IsLoaded = true;

                print?.Invoke("加载完成");

                Init();
                print?.Invoke("初始化完成");
            }
            catch (Exception ex)
            {
                IsLoaded = false;
                print?.Invoke($"加载失败:{ex.Message}");
            }
        }

        private void Init()
        {
            PatchNPC.CanNewNPC = false;
            Common.GameAction.DayTime = DataInfo.dayTime;
            Common.GameAction.Time = DataInfo.time;

            SetState(StateMapInit);
        }

        //没有:完全不做任何处理
        //-除此之外的状态:不生成任何npc,维持时间,给玩家添加创意震撼buff
        //--放置图格只能在:游戏中 && 地图范围内 && (没图格 || 可交互图格)
        //--破坏图格只能在:游戏中 && 玩家放置图格 || 可交互图格
        //--未登录玩家不可交互
        //--玩家进入时设为幽灵状态
        //--不能设置队伍,pvp,幽灵状态
        void IGameControl.Update()
        {
            if (IsLoaded == false) return;
            if (NowStateType == StateNone) return;

            uint time = Main.GameUpdateCount;

            if (time % 60 * 10 == 0)//同步时间
            {
                NetMessage.TrySendData(MessageID.SetTime);
            }

            int buffTime = 60 * 8;
            if (time % buffTime == 0)//添加创意震撼buff
            {
                if (NowState == null || NowState.CanAddBuffNoBuilding())
                {
                    for (int i = 0; i < Main.player.Length; ++i)
                    {
                        if (Main.player[i]?.active != true) continue;

                        NetMessage.SendData(MessageID.AddPlayerBuff,
                            number: i, number2: BuffID.NoBuilding, number3: buffTime + 10);
                    }
                }
            }
        }

        void IGameControl.OnPlayJoinGame(Player player)
        {
            if (IsLoaded == false) return;

            NowState?.OnPlayJoinGame(player);
        }

        void IGameControl.OnPlayLeftGame(int plr)
        {
            if (IsLoaded == false) return;

            NowState?.OnPlayLeftGame(plr);
        }

        void IGameControl.OnPlayLogin(Player player)
        {
            if (IsLoaded == false) return;

            NowState?.OnPlayLogin(player);
        }

        bool IGameControl.PlayCanActionTile(ClassTileEventArgs e)
        {
            if (IsLoaded == false) return true;

            if (NowState == null) return true;
            return NowState.PlayCanActionTile(e);
        }

        bool IGameControl.PlayCanAction(GetDataEventArgs e)
        {
            if (IsLoaded == false) return true;

            if (NowState == null) return true;
            return NowState.PlayCanAction(e);
        }
    }
}
