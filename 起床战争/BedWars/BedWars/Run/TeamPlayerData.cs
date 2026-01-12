using BedWars.BedWarsData;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    public class TeamPlayerData
    {
        public readonly TeamData team = null;
        public readonly List<Player> ps = new List<Player>();
        
        public TeamPlayerData(TeamData team)
        {
            this.team = team ?? throw new ArgumentNullException(nameof(team));
        }

        public void ClearPlayer()
        {
            ps.Clear();
        }

        public void ClearLefyPlayer()
        {
            ps.RemoveAll(i => Utils.PlayerHasServer(i) == false);
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
}
