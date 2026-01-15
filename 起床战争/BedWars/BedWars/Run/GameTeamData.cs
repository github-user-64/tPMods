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
        /// <summary>
        /// 世界位置
        /// </summary>
        public readonly Rectangle spawTile = Rectangle.Empty;
        public readonly MapData data = null;
        public readonly TeamData team = null;
        private readonly List<Player> ps = new List<Player>();

        public GameTeamData(MapData data, TeamData team)
        {
            this.data = data;
            this.team = team;

            spawTile = team.spawTile;
            spawTile.X += data.Info.X;
            spawTile.Y += data.Info.Y;
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

        public void UpdateSpawTileActive()
        {
            int xlen = spawTile.X + spawTile.Width;
            int ylen = spawTile.Y + spawTile.Height;

            for (int y = spawTile.Y; y < ylen; ++y)
            {
                for (int x = spawTile.X; x < xlen; ++x)
                {
                    Tile tile = Main.tile[x, y];
                    bool tileA = tile?.active() ?? false;

                    if (tileA) continue;//缺一个图格就视为床没了

                    SpawTileActive = false;
                    return;
                }
            }

            SpawTileActive = true;
        }
    }
}
