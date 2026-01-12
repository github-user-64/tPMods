using BedWars.BedWarsData;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    public class TeamAndPlayer
    {
        public class TeamPlayer
        {
            public int PlayerCount => ps.Count;
            public readonly TeamData team = null;
            private readonly List<Player> ps = new List<Player>();

            public TeamPlayer(TeamData team)
            {
                this.team = team ?? throw new ArgumentNullException(nameof(team));
            }

            public void ClearPlayer()
            {
                ps.Clear();
            }

            public void DelPlayer(Player player)
            {
                ps.Remove(player);
            }

            public void AddPlayer(Player player)
            {
                if (ps.Contains(player)) return;

                ps.Add(player);
            }

            public void ForPlay(Action<Player> action)
            {
                if (action != null) ps.ForEach(action);
            }

            /// <summary>
            /// 不判断是否是这个队伍的
            /// </summary>
            public void SetPlayerAttributes(Player player)
            {
                if (player == null) return;

                player.statLifeMax = team.statLifeMax;
                player.statLife = player.statLifeMax;
                NetMessage.TrySendData(MessageID.PlayerLifeMana, -1, -1, null, player.whoAmI);

                player.statManaMax = team.statManaMax;
                player.statMana = player.statManaMax;
                NetMessage.TrySendData(MessageID.Unknown42, -1, -1, null, player.whoAmI);
            }
        }

        public int TeamCount => teams.Count;
        public readonly List<TeamPlayer> teams = new List<TeamPlayer>();
        private readonly TeamPlayer[] playerTeam = null;

        public TeamAndPlayer(List<TeamData> teams)
        {
            if (teams == null) throw new ArgumentNullException(nameof(teams));

            teams.ForEach(i =>
            {
                this.teams.Add(new TeamPlayer(i));
            });

            playerTeam = new TeamPlayer[Main.player.Length - 1];
        }

        public void PlayerAddToTeam(Player player, TeamPlayer team)
        {
            if (Utils.PlayerHasServer(player) == false) return;
            if (team == null) return;

            if (playerTeam.IndexInRange(player.whoAmI) != true) return;
            if (playerTeam[player.whoAmI] != null) return;//已经有队伍

            team.AddPlayer(player);
            playerTeam[player.whoAmI] = team;
        }

        public TeamPlayer GetTeam(Player player)
        {
            if (Utils.PlayerHasServer(player) == false) return null;
            if (playerTeam.IndexInRange(player.whoAmI) != true) return null;

            return playerTeam[player.whoAmI];
        }

        public void ClearPlayer()
        {
            teams.ForEach(i => i.ClearPlayer());

            for (int i = 0; i < playerTeam.Length; ++i)
            {
                playerTeam[i] = null;
            }
        }

        public void ForTeam(Action<TeamPlayer> action)
        {
            if (action != null) teams.ForEach(action);
        }

        /// <summary>
        /// 清除不在线玩家
        /// </summary>
        public bool ClearLeftPlayer()
        {
            bool hasDel = false;

            for (int i = 0; i < playerTeam.Length; ++i)
            {
                TeamPlayer team = playerTeam[i];
                if (team == null) continue;//没队伍

                Player player = Main.player[i];

                if (Utils.PlayerHasServer(player) == true) continue;//在服务器

                hasDel = true;

                DelPlayerTeam(player, team);
            }

            return hasDel;
        }

        private void DelPlayerTeam(Player player, TeamPlayer team)
        {
            if (player == null) return;
            if (team == null) return;

            team.DelPlayer(player);
            playerTeam[player.whoAmI] = null;
        }

        public void DelPlayerTeam(Player player)
        {
            DelPlayerTeam(player, GetTeam(player));
        }
    }
}
