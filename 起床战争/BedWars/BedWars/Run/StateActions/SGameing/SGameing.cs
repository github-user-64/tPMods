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
    internal partial class SGameing : IStateAction
    {
        private readonly List<ShopNPC> shopNPC = new List<ShopNPC>();

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

            shopNPC.Clear();

            game.ForTeam(i => i.UpdateSpawTileActive());//更新重生方块活动状态

            //分配队伍
            AssignTeam(arg as List<Player>, p =>
            {
                ToPlayerPrint.PrintToPlay(p.whoAmI, "队伍满了现在你只能看着,太可惜了:(", Color.White);
            });

            //生成玩家到队伍
            game.ForActivePlayer(i => SpawPlayToTeam(i));

            ToPlayerPlayNetSound.ToPlayAll(SoundID.Item4);
            ToPlayerPrint.PrintToPlayAll("游戏开始", Color.YellowGreen);
            ToPlayerCombatText.ToPlayAllOff("游戏开始", 0, -40, Color.YellowGreen);

            game.DataSpawItems.ForEach(i => Common.GameAction.SpawItem.Add(i));//生成物品
            game.DataShops.ForEach(i => shopNPC.Add(new ShopNPC(game.DataInfo, i)));//商店
        }

        public override void OnEnd()
        {
            Common.GameAction.SpawItem.Clear();//生成物品
            shopNPC.ForEach(i => i.DelNPC());//商店
        }

        private void AssignTeam(List<Player> ps = null, Action<Player> thisPlayNoTeam = null)//分配队伍
        {
            game.ClearAllTeamPlayer();

            if (ps == null) return;
            ps.RemoveAll(i => Utils.PlayerHasServer(i) == false);
            if (ps.Count < 1) return;

            //

            int teamI = 0;
            List<GameTeamData> canAssignTeam = game.Team.teams.ToList();//可分配队伍

            for (int i = 0; i < ps.Count; ++i)
            {
                if (canAssignTeam.Count < 1)//没有可分配队伍
                {
                    thisPlayNoTeam?.Invoke(ps[i]);
                    continue;
                }

                GameTeamData team = canAssignTeam[i];

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

        /// <summary>
        /// 生成玩家到队伍:没队伍设为幽灵状态[退出],重生,设置位置,设置属性,清空设置背包,设置队伍pvp
        /// </summary>
        private void SpawPlayToTeam(Player player)
        {
            GameTeamData team = game.GetPlayerTeam(player);
            if (team == null)
            {
                game.SetPlayGhostState(player);//没队伍设为幽灵状态
                return; 
            }

            //设为正常状态
            game.SetPlayGhost(player, false);

            MapInfoData info = game.DataInfo;
            Point pos = new Point(info.X + team.team.spawPos.X, info.Y + team.team.spawPos.Y);

            game.SpawnToPos(player, pos);//重生,设置位置

            team.SetPlayerAttributes(player);//设置玩家属性

            game.ResetInventory(player);//重置物品栏

            game.SetTeamPvP(player, team.team.team, true);//设置队伍pvp
        }

        public override void Update(uint gametime)
        {
            UpdateOnlinePlayer(out bool stateUpdate);
            if (stateUpdate) return;

            UpdateVoid(gametime);

            UpdateSpawTile(null);

            UpdateShop();

            UpdateAction();
        }

        private void UpdateVoid(uint gametime)//更新虚空
        {
            if (gametime % 30 != 0) return;

            MapInfoData info = game.DataInfo;
            if (info.voidHeight < 1) return;

            Vector2 voidPos = new Point(0, info.Y + info.Height - 1 - info.voidHeight).ToWorldCoordinates(0, 0);
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

                NetMessage.SendPlayerHurt(i.whoAmI, reason, 20 * 8, 0, false, false, -1, -1, -1);
            });
        }

        private void UpdateOnlinePlayer(out bool stateUpdate)
        {
            stateUpdate = false;

            List<GameTeamData> hasPlayTeam = new List<GameTeamData>();//有玩家的队伍

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
                stateUpdate = true;
                game.SetStateUpdate(GameRun.StateGameEnd, hasPlayTeam[0]);
                return;
            }
        }

        private void UpdateSpawTile(Player player = null)//更新重生方块活动状态
        {
            game.ForTeam(team =>
            {
                if (team.SpawTileActive == false) return;
                team.UpdateSpawTileActive();
                if (team.SpawTileActive == true) return;

                if (player == null) return;

                ToPlayerPlayNetSound.ToPlayAll(SoundID.DD2_BetsyDeath);
                ToPlayerPlayNetSound.ToPlayAll(SoundID.DD2_BetsyDeath);
                ToPlayerPlayNetSound.ToPlayAll(SoundID.DeerclopsDeath);
                ToPlayerPlayNetSound.ToPlayAll(SoundID.DeerclopsDeath);

                ToPlayerPrint.PrintToPlayAll($"{player.name}破坏了{team.team.name}队的床", Color.Yellow);

                team.ForPlay(i =>
                {
                    ToPlayerCombatText.ToPlay(i.whoAmI, "队伍被摧毁,你将不能重生", i.Center.X, i.Center.Y - 40, Color.Red);
                });
            });
        }

        private void OnPlayerDead(Player player, PlayerDeathReason reason = null)
        {
            GameTeamData team = game.GetPlayerTeam(player);
            if (team == null) return;

            if (ModTool.Utils.Utils.GetRand(0, 2) == 0)//声音
            {
                ToPlayerPlayNetSound.ToPlayAll(SoundID.DSTMaleHurt, player.Center);
            }
            else
            {
                ToPlayerPlayNetSound.ToPlayAll(SoundID.DSTFemaleHurt, player.Center);
            }

            if (reason?.TryGetCausingEntity(out Entity causingEntity) == true)//彩纸
            {
                Vector2 v = Vector2.Normalize(player.Center - causingEntity.Center);

                Projectile.NewProjectile(null, player.Center, v * 8, ProjectileID.ConfettiGun, 0, 0);
            }

            if (team.CanSpaw) SpawPlayToTeam(player);//可以重生就重生
            else DelPlayerTeam(player);//从队伍删除
        }

        private void DelPlayerTeam(Player player)//删除玩家队伍
        {
            game.Team.DelPlayerTeam(player);//从队伍删除

            game.SetPlayGhostState(player);//设为幽灵状态

            UpdateOnlinePlayer(out _);
        }

        public override void OnGetDataPo(Player player, int messageType, MessageBuffer buffer)
        {
            if (messageType == MessageID.TileManipulation)
            {
                UpdateSpawTile(player);
            }
            else if (messageType == MessageID.PlayerDeathV2)
            {
                PlayerDeathReason reason = null;

                try
                {
                    int _whoAmI = buffer.reader.ReadByte();
                    reason = PlayerDeathReason.FromReader(buffer.reader);
                }
                catch { }

                OnPlayerDead(player, reason);
            }
        }

        public override void OnPlayJoinGame(Player player)
        {
            ToPlayerPrint.PrintToPlay(player.whoAmI, "正在游戏中,请等待游戏结束", Color.YellowGreen);
        }
    }
}
