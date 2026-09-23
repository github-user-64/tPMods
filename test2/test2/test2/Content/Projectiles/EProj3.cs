using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria;

namespace test2.Content.Projectiles
{
    internal class EProj3 : ExtenProjectile
    {
        protected const int ProjLen = 8;
        public override string Texture => "Terraria/Images/Projectile_454";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }

        public override void SetDefault(Projectile proj)
        {
            proj.width = 62;
            proj.height = 62;
            proj.scale = 1f;
            proj.aiStyle = 0;
            proj.timeLeft = 60 * 8;
            proj.tileCollide = false;//图格碰撞
            proj.ignoreWater = true;//无视水
            proj.penetrate = -1;//穿透次数, -1无限
            proj.friendly = true;//友好
        }

        public override bool? Colliding(Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            return false;
        }
    }
}
