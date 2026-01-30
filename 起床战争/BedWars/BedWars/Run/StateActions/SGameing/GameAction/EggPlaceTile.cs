using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions.GameAction
{
    /// <summary>
    /// 搭桥蛋
    /// </summary>
    public class EggPlaceTile : IGameAction
    {
        public Func<int, int, bool> CanPlace = null;

        public EggPlaceTile(Func<int, int, bool> CanPlace = null)
        {
            this.CanPlace = CanPlace;
        }

        public override void UpdateProjectile(Projectile proj, Player player)
        {
            if (proj.type != ProjectileID.StarAnise) return;

            Vector2 p = Vector2.Normalize(proj.velocity) * -24;//在射弹后面
            p += proj.Center;

            Point pos = p.ToTileCoordinates();

            if (CanPlace?.Invoke(pos.X, pos.Y) == false) return;

            WorldGen.PlaceTile(pos.X, pos.Y, TileID.Cloud);
            NetMessage.SendTileSquare(-1, pos.X, pos.Y, 1);
        }
    }
}
