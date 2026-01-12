using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SGameing : IStateAction
    {
        public SGameing(GameRun game) : base(game) { }

        //游戏中:
        //-进入时:分配队伍并添加到队伍数据,生成玩家到队伍
        //-生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置队伍pvp
        //-玩家重生时:生成玩家到队伍
        //-玩家退出,队伍不可重生时死亡,时从队伍删除
        //-当有队伍删除玩家时:
        //--设为幽灵状态
        //--如果只有一个队伍有人时进入游戏结束
        //--如果所有队伍都没玩家时进入初始化地图
        public override void OnStart(object arg)
        {
            if (game.Team.TeamCount < 1)
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
            List<TeamAndPlayer.TeamPlayer> canAssignTeam = game.Team.teams.ToList();//可分配队伍

            for (int i = 0; i < ps.Count; ++i)
            {
                if (canAssignTeam.Count < 1)//没有可分配队伍
                {
                    thisPlayNoTeam?.Invoke(ps[i]);
                    continue;
                }

                TeamAndPlayer.TeamPlayer team = canAssignTeam[i];

                if (team.PlayerCount < team.team.maxPlay)//如果该队伍没到最大玩家数量
                {
                    game.Team.PlayerAddToTeam(ps[i], team);

                    teamI++;
                }
                else
                {
                    canAssignTeam.RemoveAt(teamI);
                }

                if (teamI >= canAssignTeam.Count) teamI = 0;
            }
        }

        //生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置队伍pvp
        private void SpawPlayToTeam(Player player)
        {
            TeamAndPlayer.TeamPlayer team = game.GetPlayerTeam(player);
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

            game.SpawnToPos(player, team.team.spawPos);//重生,设置位置

            team.SetPlayerAttributes(player);//设置玩家属性

            game.ResetInventory(player);//重置物品栏

            game.SetTeamPvP(player, team.team.team, true);//设置队伍pvp
        }

        public override void Update(uint gametime)
        {
            if (game.ClearLeftPlayer())
            {
                OnTeamPlayerUpdate(out bool stateUpdate);
                if (stateUpdate) return;
            }
            
            UpdateVoid(gametime);
        }

        private void UpdateVoid(uint gametime)//更新虚空
        {
            if (gametime % 30 != 0) return;

            MapInfoData info = game.DataInfo;
            if (info.voidHeight < 1) return;

            Vector2 voidPos = new Point(0, info.pos.Y + info.size.Y - 1 - info.voidHeight).ToWorldCoordinates(0, 0);
            float voidY = voidPos.Y;

            game.ForAllTeamPlay(i =>
            {
                if (i.Center.Y < voidY) return;

                string text = null;
                switch (ModTool.Utils.Utils.GetRand(0, 8))
                {
                    case 0: text = $"{i.name}选择与虚空为伍"; break;
                    case 1: text = $"{i.name}失足掉入了虚空"; break;
                    case 2: text = $"{i.name}失去了他的双脚"; break;
                    case 3: text = $"{i.name}的内脏变成了外脏,凶手是{i.name}"; break;
                    case 4: text = $"{i.name}变成了液态"; break;
                    case 5: text = $"{i.name}成为了一维生物"; break;
                    case 6: text = $"{i.name}被斩杀了,凶手是虚空"; break;
                    case 7: text = $"{i.name}成为了虚空的敌人"; break;
                    default: text = $"{i.name}死得有点蹊跷"; break;
                }

                PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(text);

                NetMessage.SendPlayerHurt(i.whoAmI, reason, 20 * 5, 0, false, false, -1, -1, -1);
            });
        }

        private void OnTeamPlayerUpdate(out bool stateUpdate)
        {
            stateUpdate = false;

            List<TeamAndPlayer.TeamPlayer> hasPlayTeam = new List<TeamAndPlayer.TeamPlayer>();//有玩家的队伍

            game.ForTeam(i =>
            {
                if (i.PlayerCount > 0) hasPlayTeam.Add(i);
            });

            //如果所有队伍都没玩家时进入初始化地图
            if (hasPlayTeam.Count < 1)
            {
                ToPlayerPrint.PrintToPlayAll("没有玩家在队伍中,重新开始游戏", Color.White);

                stateUpdate = true;
                game.SetStateUpdate(GameRun.StateMapInit);
                return;
            }

            //如果只有一个队伍有人时进入游戏结束
            if (hasPlayTeam.Count == 1)
            {
                return;
            }
        }
    }
}
