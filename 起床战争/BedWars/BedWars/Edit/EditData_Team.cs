using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;

namespace BedWars.Edit
{
    public partial class EditData
    {
        public string TeamAdd()
        {
            if (Data == null) return "地图数据为null";

            TeamData team = new TeamData();
            team.spawTile = new Rectangle(0, 0, 1, 1);

            DataTeams.Add(team);

            return null;
        }

        public string TeamDel(TeamData data)
        {
            if (Data == null) return "地图数据为null";

            DataTeams.Remove(data);

            return null;
        }

        /// <summary>
        /// <paramref name="rect"/>为世界位置
        /// </summary>
        public string TeamSpawTileSet(TeamData data, Rectangle rect)
        {
            if (Data == null) return "地图数据为null";

            if (rect.Width < 1) throw new Exception("重生方块不能小于1");
            if (rect.Height < 1) throw new Exception("重生方块不能小于1");

            rect.X -= DataInfo.X;
            rect.Y -= DataInfo.Y;

            if (Data.InMapRelative(rect) == false) return "超出地图";

            data.spawTile = rect;

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string TeamSpawSetPos(TeamData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.spawPos = pos;

            return null;
        }
    }
}
