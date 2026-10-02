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
            ProjectileLoad.SetDefaultsPostfix(proj, Type);
        }

        /// <summary/>
        public void AIPostfix(Projectile proj)
        {
            ProjectileLoad.AIPostfix(proj);
        }

        /// <summary/>
        public void KillPostfix(Projectile proj)
        {
            ProjectileLoad.KillPostfix(proj);
        }

        /// <summary/>
        public void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            ProjectileLoad.NewProjectilePostfix(proj, spawnSource);
        }

        /// <summary/>
        public void CollidingPostfix(ref bool result, Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            ProjectileLoad.CollidingPostfix(ref result, proj, myRect, targetRect);
        }

        /// <summary/>
        public void GetAlphaPostfix(ref Color result, Projectile proj, Color newColor)
        {
            ProjectileLoad.GetAlphaPostfix(ref result, proj, newColor);
        }

        /// <summary/>
        public bool DrawProjDirectPrefix(Projectile proj, Color lightColor, Player player = null)
        {
            return ProjectileLoad.DrawProjDirectPrefix(proj, lightColor, player);
        }

        /// <summary/>
        public void DrawProjDirectPostfix(Projectile proj, Color lightColor, Player player = null)
        {
            ProjectileLoad.DrawProjDirectPostfix(proj, lightColor, player);
        }
    }
}
