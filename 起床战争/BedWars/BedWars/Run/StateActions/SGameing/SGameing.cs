using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SGameing : IStateAction
    {
        public SGameing(GameRun game) : base(game) { }

        //游戏中:
        //-进入时:分配队伍并添加到队伍数据,生成玩家到队伍,启用pvp
        //-生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置玩家队伍
        //-玩家重生时:生成玩家到队伍
        //-玩家退出,队伍不可重生时死亡,时从队伍删除
        //-当有队伍删除玩家时:
        //--设为幽灵状态
        //--如果只有一个队伍有人时进入游戏结束
        //--如果所有队伍都没玩家时进入初始化地图
        public override void OnStart(object arg)
        {
            if (game.Teams.Count < 1)
            {
                string text = "无法开始游戏,没有可分配队伍";
                tContentPatch.ContentPatch.PrintTry(text);
                ToPlayerPrint.PrintToPlayAll(text, Color.White);

                game.SetState(GameRun.StateNone);
                return;
            }

            //分配队伍
            AssignTeam(arg as List<Player>, p =>
            {
                ToPlayerPrint.PrintToPlay(p.whoAmI, "队伍满了现在你只能看着,太可惜了:(", Color.White);
            });

            //生成玩家到队伍
            game.ForActivePlayer(i => SpawPlayToTeam(i));
        }

        private void AssignTeam(List<Player> ps = null, Action<Player> thisPlayNoTeam = null)//分配队伍
        {
            game.ClearAllTeamPlayer();

            if (ps == null) return;
            ps.RemoveAll(i => Utils.PlayerHasServer(i) == false);
            if (ps.Count < 1) return;

            //

            int teamI = 0;

            List<TeamPlayerData> canAssignTeam = game.Teams.ToList();//可分配队伍

            for (int i = 0; i < ps.Count; ++i)
            {
                if (teamI >= canAssignTeam.Count)//没有可分配队伍
                {
                    thisPlayNoTeam?.Invoke(ps[i]);
                    continue;
                }

                TeamPlayerData team = canAssignTeam[teamI];

                if (team.ps.Count < team.team.maxPlay)//如果该队伍没到最大玩家数量
                {
                    team.ps.Add(ps[i]);
                }

                teamI++;
            }
        }

        //生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置玩家队伍
        private void SpawPlayToTeam(Player player)
        {
            TeamPlayerData team = game.GetPlayerTeam(player);
            if (team == null)
            {
                game.SetPlayGhost(player);//没队伍设为幽灵状态
                return; 
            }

            //设为正常状态
            if (player.ghost)
            {
                player.ghost = false;
                NetMessage.TrySendData(MessageID.PlayerControls);
            }

            game.SpawnToPos(player, team.team.spawPos);

            //设置属性
        }

        public override void Update(uint gametime)
        {
            game.ClearAllTeamLeftPlayer();//清除离线玩家

            OnTeamPlayerUpdate(out bool stateUpdate);
            if (stateUpdate) return;

            //NetMessage.SendPlayerHurt(i, reason, 6, 0, false, false, -1, -1, -1);
        }

        private void OnTeamPlayerUpdate(out bool stateUpdate)
        {
            stateUpdate = false;

            List<TeamPlayerData> hasPlayTeam = new List<TeamPlayerData>();//有玩家的队伍

            game.ForTeam(i =>
            {
                if (i.ps.Count > 0) hasPlayTeam.Add(i);
            });

            if (hasPlayTeam.Count < 1)
            {
                ToPlayerPrint.PrintToPlayAll("没有玩家在队伍中,重新开始游戏", Color.White);

                stateUpdate = true;
                game.SetStateUpdate(GameRun.StateMapInit);
                return;
            }
        }
    }
}
