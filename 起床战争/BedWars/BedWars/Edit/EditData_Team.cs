using BedWars.BedWarsData;
using Microsoft.Xna.Framework;

namespace BedWars.Edit
{
    public partial class EditData
    {
        public string AddTeam()
        {
            if (Data == null) return "地图数据为null";

            TeamData team = new TeamData();
            team.mapData = Data;

            DataTeams.Add(team);

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SetTeamSpawTilePos(TeamData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.spawTilePos.X = pos.X;

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string SetTeamSpawPos(TeamData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.spawPos.X = pos.X;

            return null;
        }
    }
}
