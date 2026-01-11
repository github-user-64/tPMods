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
        public readonly int statLifeMax = 500;
        public readonly int statManaMax = 20 * 3;

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

        public void asd()
        {
            ps.ForEach(i =>
            {
                NetMessage.TrySendData(MessageID.PlayerLifeMana, -1, -1, null, i.whoAmI, statLifeMax, statLifeMax);

                NetMessage.TrySendData(MessageID.Unknown42, -1, -1, null, i.whoAmI, statManaMax, statManaMax);

                //
            });
        }
    }
}
