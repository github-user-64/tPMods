using BedWars.BedWarsData;
using BedWars.Run.StateActions.GameAction;
using Terraria;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing
    {
        private IGameAction[] actions = null;
        private bool isInitActioned = false;

        private void InitAction()
        {
            if (isInitActioned) return;
            isInitActioned = true;

            EggPlaceTile a1 = new EggPlaceTile((x, y) =>
            {
                int mapX = x - game.DataInfo.X;
                int mapY = y - game.DataInfo.Y;

                if (game.Data.InMapRelative(mapX, mapY) == false) return false;//不在地图里

                TileData data = game.DataTile[mapY][mapX];
                if (data.CanActionTile == false) return false;

                return true;
            });

            EggSpawnNPC a2 = new EggSpawnNPC();

            HookGo a3 = new HookGo();

            PaladinsHammerFriendly a4 = new PaladinsHammerFriendly();

            actions = new IGameAction[] { a1, a2, a3, a4 };
        }

        public override void OnProjectileKill(Projectile proj)
        {
            if (actions == null) return;

            GameTeamData team = game.Team.GetData(proj.owner, null);
            if (team == null) return;

            foreach (IGameAction ga in actions) ga.OnProjectileKill(proj);
        }

        private void UpdateAction()
        {
            if (actions == null) return;

            foreach (Projectile i in Main.projectile)
            {
                if (i?.active != true) continue;
                if (i.timeLeft < 1) continue;

                Player player = ModTool.ServerHelp.PlayDataUtils.GetPlay(i.owner);
                if (player == null) continue;

                GameTeamData team = game.GetPlayerTeam(player);
                if (team == null) continue;

                foreach (IGameAction ga in actions) ga.UpdateProjectile(i, player);
            }
        }
    }
}
