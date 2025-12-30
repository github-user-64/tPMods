using BedWars.BedWarsData;
using System;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace BedWars.Run
{
    //加载地图数据在服务端加载模组完成后
    //运行状态分为:没有,初始化地图,准备游戏,游戏中,游戏结束
    //
    //设为幽灵状态:设为幽灵,设置到进入游戏位置,清空背包
    //
    //没有:完全不做任何处理
    //-除此之外的状态:不生成任何npc,维持时间,
    //--放置图格只能在:地图范围内 && (没图格 || 可交互图格)
    //--破坏图格只能在:玩家放置图格 || 可交互图格
    //--未登录玩家不可交互
    //--玩家进入时设为幽灵状态
    //--不能设置队伍,pvp,幽灵状态
    //
    //初始化地图:
    //-进入时:所有玩家重生到进入游戏位置,清空玩家背包,清空玩家放置图格列表,清理图格,放置图格,进入准备游戏
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
    //-进入时:分配队伍并添加到队伍数据,没队伍设为幽灵状态,生成玩家到队伍,启用pvp
    //-生成玩家到队伍:没队伍设为幽灵状态,重生,设置位置,设置属性,清空设置背包
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
    internal class GameRun
    {
        public static readonly GameRun instance = new GameRun();
        public MapData Data { get; private set; } = null;
        public MapInfoData DataInfo => Data?.Info;
        public List<SpawItemData> DataSpawItems => Data?.SpawItems;
        public List<TeamData> DataTeams => Data?.Teams;
        public List<CanTileData> DataCanTiles => Data?.CanTileDatas;

        private class asd : Mod
        {
            public override void Loaded()
            {
                try
                {
                    if (Main.netMode != 2) return;

                    ContentPatch.PrintTry($"起床战争:地图目录:{ThisMod.DirMapData}");
                    ContentPatch.PrintTry("起床战争:加载地图数据");
                    MapData temp = DataFileHelp.ReadData(ThisMod.DirMapData);

                    ContentPatch.PrintTry("起床战争:检查数据");
                    DataCheck.Repair(temp);
                    DataCheck.CheckMapData(temp);

                    ModTool.ServerHelp.Utils.ServerSideCharacter(true);

                    instance.Data = temp;

                    ContentPatch.PrintTry("起床战争:加载完成");
                }
                catch (Exception ex)
                {
                    ContentPatch.PrintTry($"起床战争:加载失败:{ex.Message}");
                }
            }
        }
    }
}
