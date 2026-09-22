using ExtenContent.Extens;
using Terraria;
using Terraria.DataStructures;

namespace test2.Content.Projectiles
{
    internal class EProj2 : ExtenProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_645";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = Main.projFrames[645];
        }

        public override void SetDefault(Projectile proj)
        {
            proj.width = 98;
            proj.height = 98;
            proj.scale = 1f;
            proj.aiStyle = 0;
            //proj.timeLeft = 0;
            proj.tileCollide = false;//图格碰撞
            proj.ignoreWater = true;//无视水
            proj.penetrate = -1;//穿透次数, -1无限
            proj.friendly = true;//友好
        }

        public override void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            proj.scale = proj.ai[0];
        }

        public override void AI(Projectile proj)
        {
            int frame = proj.frame + 1;
            if (frame >= Main.projFrames[Type])
            {
                proj.Kill();
                return;
            }
            proj.frame = frame;

            proj.scale = proj.ai[0];
        }
    }
}
