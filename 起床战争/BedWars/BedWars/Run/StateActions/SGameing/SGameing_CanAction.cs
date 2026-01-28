using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using ModTool.Utils.GetDataEventArgs;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing
    {
        public override bool PlayCanAction(GetDataEventArgs e)
        {
            if (base.PlayCanAction(e)) return true;

            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team == null) return false;

            if (e is TogglePVPEventArgs) return false;//不能改pvp
            if (e is TeamChangeEventArgs) return false;//不能改队伍

            return true;
        }

        public override bool PlayCanActionTile(ClassTileEventArgs e)
        {
            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team == null) return false;//没队伍

            Point mapPos = new Point(e.x, e.y);
            mapPos.X -= game.DataInfo.X;
            mapPos.Y -= game.DataInfo.Y;

            //

            if (game.Data.InMapRelative(mapPos) == false) return false;//不在地图里

            //

            foreach (GameTeamData i in game.Team.teams)
            {
                if (i.SpawTileActive == false) continue;//床还在
                if (i.spawTile.Contains(e.x, e.y) == false) continue;//在床的范围内
                if (i != team) return true;//不是自己的床

                ToPlayerPrint.PrintToPlay(e.player.whoAmI, "你不能破坏自己的床", Color.Red);
                return false;
            }

            //

            TileData data = game.DataTile[mapPos.Y][mapPos.X];
            if (data.canAction == true) return true;//是可交互图格

            if (e is TileManipulationEventArgs tm) return OnTileManipulation(tm, data);

            return data.CanActionTile;
        }

        private bool OnTileManipulation(TileManipulationEventArgs e, TileData data)
        {
            bool isWall = false;

            switch (e.manipulationType)
            {
                case 2: isWall = true; break;
                case 3: isWall = true; break;
                case 22: isWall = true; break;
                default: break;
            }

            //是墙类型的操作
            if (isWall) return data.CanActionWall;

            return data.CanActionTile;
        }
    }
}
