using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiProjectileLoad
    {
        internal ApiProjectileLoad() { }

        /// <summary/>
        public void SetDefaultsPostfix(Projectile proj, int Type)
        {
            ProjectileLoader.SetDefaultsPostfix(proj, Type);
        }

        /// <summary/>
        public void AIPostfix(Projectile proj)
        {
            ProjectileLoader.AIPostfix(proj);
        }

        /// <summary/>
        public void KillPostfix(Projectile proj)
        {
            ProjectileLoader.KillPostfix(proj);
        }

        /// <summary/>
        public void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            ProjectileLoader.NewProjectilePostfix(proj, spawnSource);
        }

        /// <summary/>
        public void CollidingPostfix(ref bool result, Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            ProjectileLoader.CollidingPostfix(ref result, proj, myRect, targetRect);
        }

        /// <summary/>
        public void GetAlphaPostfix(ref Color result, Projectile proj, Color newColor)
        {
            ProjectileLoader.GetAlphaPostfix(ref result, proj, newColor);
        }

        /// <summary/>
        public bool DrawProjDirectPrefix(Projectile proj, Color lightColor, Player player = null)
        {
            return ProjectileLoader.DrawProjDirectPrefix(proj, lightColor, player);
        }

        /// <summary/>
        public void DrawProjDirectPostfix(Projectile proj, Color lightColor, Player player = null)
        {
            ProjectileLoader.DrawProjDirectPostfix(proj, lightColor, player);
        }
    }
}
