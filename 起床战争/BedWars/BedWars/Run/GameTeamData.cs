using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    public class GameTeamData
    {
        public bool SpawTileActive { get; protected set; } = false;
        public bool CanSpaw => team.canSpaw && SpawTileActive;
        public int PlayerCount => ps.Count;
        public readonly TeamData team = null;
        private readonly List<Player> ps = new List<Player>();

        public GameTeamData(TeamData team)
        {
            this.team = team ?? throw new ArgumentNullException(nameof(team));
        }

        public void ClearPlayer()
        {
            ps.Clear();
        }

        public void DelPlayer(int index)
        {
            for (int i = 0; i < ps.Count; i++)
            {
                if (ps[i].whoAmI == index)
                {
                    ps.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddPlayer(Player player)
        {
            if (player == null) return;

            DelPlayer(player.whoAmI);

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

        public void UpdateSpawTile(out bool hasUpdate)
        {
            Point pos = team.spawTilePos;

            if (WorldGen.InWorld(pos.X, pos.Y) == false)
            {
                hasUpdate = SpawTileActive;//有更新, 如果之前是活动的
                SpawTileActive = false;
                return;
            }

            Tile tile = Main.tile[pos.X, pos.Y];
            bool tileA = tile?.active() ?? false;

            hasUpdate = SpawTileActive != tileA;

            SpawTileActive = tileA;
        }
    }
}
