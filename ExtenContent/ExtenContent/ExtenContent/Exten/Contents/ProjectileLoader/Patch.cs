using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ExtenContent.Exten
{
    public static partial class ProjectileLoader
    {
        internal static void SetDefaultsPostfix(Projectile proj, int Type)
        {
            ExtenProjectile ep = GetExtenProj(Type);
            if (ep == null) return;

            ep.SetDefault(proj);

            proj.active = Type != ProjectileID.None;

            proj.width = (int)(proj.width * proj.scale);
            proj.height = (int)(proj.height * proj.scale);
            proj.maxPenetrate = proj.penetrate;
        }

        internal static void AIPostfix(Projectile proj)
        {
            GetExtenProj(proj.type)?.AI(proj);
        }

        internal static void KillPostfix(Projectile proj)
        {
            GetExtenProj(proj.type)?.OnKill(proj);
        }

        internal static void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            GetExtenProj(proj.type)?.NewProjectilePostfix(proj, spawnSource);
        }

        internal static void CollidingPostfix(ref bool result, Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            bool? v = GetExtenProj(proj.type)?.Colliding(proj, myRect, targetRect);
            if (v != null) result = v.Value;
        }

        internal static void GetAlphaPostfix(ref Color result, Projectile proj, Color newColor)
        {
            Color? v = GetExtenProj(proj.type)?.GetAlpha(proj, newColor);
            if (v != null) result = v.Value;
        }

        internal static bool DrawProjDirectPrefix(Projectile proj, Color lightColor, Player player = null)
        {
            return GetExtenProj(proj.type)?.PreDraw(proj, lightColor, player) ?? true;
        }

        internal static void DrawProjDirectPostfix(Projectile proj, Color lightColor, Player player = null)
        {
            GetExtenProj(proj.type)?.PostDraw(proj, lightColor, player);
        }

        internal static bool UpdatePositionPrefix(Projectile proj, Vector2 wetVelocity)
        {
            return GetExtenProj(proj.type)?.CanUpdatePosition(proj, wetVelocity) ?? true;
        }
    }
}
