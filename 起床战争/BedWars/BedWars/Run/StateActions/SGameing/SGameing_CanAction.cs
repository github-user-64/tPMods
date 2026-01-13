using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using ModTool.Utils.GetDataEventArgs;
using Terraria;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing
    {
        public override bool PlayCanAction(GetDataEventArgs e)
        {
            if (e is ControlsEventArgs ce)
            {
                return ce.ghost == ce.player.ghost;//是否修改了幽灵状态
            }

            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team != null) return true;

            if (e is TogglePVPEventArgs) return false;
            if (e is ToggleTeamEventArgs) return false;

            return false;
        }

        public override bool PlayCanActionTile(ClassTileEventArgs e)
        {
            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team == null) return false;//没队伍

            Point mapPos = new Point(e.x, e.y);
            mapPos.X -= game.DataInfo.pos.X;
            mapPos.Y -= game.DataInfo.pos.Y;

            if (game.Data.InMapRelative(mapPos) == false) return false;//不在地图里

            TileData data = game.DataTile[mapPos.Y][mapPos.X];
            if (data.canAction == true) return true;//是可交互图格
            if (data.HasPlayerActive == true) return true;//玩家交互过了

            if (e is TileManipulationEventArgs tm) return OnTileManipulation(tm, data);

            return false;
        }

        //那里没实心方块就可以交互
        private bool OnTileManipulation(TileManipulationEventArgs e, TileData data)
        {
            Tile tile = Main.tile[e.x, e.y];
            if (tile == null) return false;//应该不可能会为空

            if (tile.active() == true) return false;//有实心方块了

            data.HasPlayerActive = true;//标记玩家碰过了
            return true;
        }
    }
}
