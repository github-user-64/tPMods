namespace BedWars.Run
{
    internal class SGameing : IStateAction
    {
        public SGameing(GameRun game) : base(game) { }

        //游戏中:
        //-进入时:分配队伍并添加到队伍数据,没队伍设为幽灵状态,生成玩家到队伍,启用pvp
        //-生成玩家到队伍:没队伍设为幽灵状态,重生,设置位置,设置属性,清空设置背包,设置玩家队伍
        //-玩家重生时:生成玩家到队伍
        //-玩家退出,队伍不可重生时死亡,时从队伍删除
        //-当有队伍删除玩家时:
        //--设为幽灵状态
        //--如果只有一个队伍有人时进入游戏结束
        //--如果所有队伍都没玩家时进入初始化地图
        public override void OnStart()
        {
            
        }
    }
}
