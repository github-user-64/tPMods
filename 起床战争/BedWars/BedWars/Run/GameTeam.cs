using BedWars.BedWarsData;
using ModTool.AdditionalData;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Run
{
    public class GameTeam : PlayerAdditionalData<GameTeamData>
    {
        public int TeamCount => teams.Count;
        public readonly List<GameTeamData> teams = new List<GameTeamData>();

        public GameTeam(MapData data)
        {
            data.Teams.ForEach(i =>
            {
                teams.Add(new GameTeamData(data, i));
            });
        }

        //不允许设置值
        public override bool SetData(int index, GameTeamData val) => false;
        protected override bool UpdateDataItem(int index, bool clearOld = false) => false;

        protected override void ClearDataItem(int index)
        {
            GetData(index)?.DelPlayer(index);

            base.ClearDataItem(index);
        }

        public void PlayerAddToTeam(Player player, GameTeamData team)
        {
            if (Utils.PlayerHasServer(player) == false) return;
            if (team == null) return;

            if (IndexInRange(player.whoAmI) == false) return;

            if (teams.Contains(team) == false) return;//没有这个队伍

            team.AddPlayer(player);

            data[player.whoAmI] = team;
            hasData[player.whoAmI] = true;
        }

        public void ForTeam(Action<GameTeamData> action)
        {
            if (action != null) teams.ForEach(action);
        }

        public GameTeamData GetTeam(Player player)
        {
            return GetData(player.whoAmI, null);
        }

        public void ClearPlayer()
        {
            ClearData();
        }

        public void DelPlayerTeam(Player player)
        {
            ClearDataItem(player.whoAmI);
        }
    }
}
