using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ProjectileLoad
    {
        internal static void SetDefaultsPostfix(Projectile proj, int Type)
        {
            ExtenProjectile ep = GetProj(Type);
            if (ep == null) return;

            proj.active = Type != ProjectileID.None;

            ep.SetDefault(proj);
        }

        internal static void AIPostfix(Projectile proj)
        {
            GetProj(proj.type)?.AI(proj);
        }

        internal static void KillPostfix(Projectile proj)
        {
            GetProj(proj.type)?.OnKill(proj);
        }

        internal static void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            GetProj(proj.type)?.NewProjectilePostfix(proj, spawnSource);
        }

        internal static void CollidingPostfix(ref bool result, Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            bool? v = GetProj(proj.type)?.Colliding(proj, myRect, targetRect);
            if (v != null) result = v.Value;
        }

        internal static void GetAlphaPostfix(ref Color result, Projectile proj, Color newColor)
        {
            Color? v = GetProj(proj.type)?.GetAlpha(proj, newColor);
            if (v != null) result = v.Value;
        }

        internal static bool DrawProjDirectPrefix(Projectile proj, Color lightColor, Player player = null)
        {
            return GetProj(proj.type)?.PreDraw(proj, lightColor, player) ?? true;
        }

        internal static void DrawProjDirectPostfix(Projectile proj, Color lightColor, Player player = null)
        {
            GetProj(proj.type)?.PostDraw(proj, lightColor, player);
        }
    }
}
