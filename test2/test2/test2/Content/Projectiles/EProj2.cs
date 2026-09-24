using ExtenContent.Extens;
using Microsoft.Xna.Framework;
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
            proj.timeLeft = 60;
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
            if (Main.GameUpdateCount % 4 != 0) return;

            int frame = proj.frame + 1;
            if (frame > 3 && proj.ai[1]-- > 0) frame = 0;
            if (frame >= Main.projFrames[Type])
            {
                proj.Kill();
                return;
            }
            proj.frame = frame;

            proj.scale = proj.ai[0];

            if (Main.player.IndexInRange(proj.owner) != true) return;
            Player player = Main.player[proj.owner];
            if (player != Main.LocalPlayer) return;

            if (proj.localAI[0] < 1) return;
            if (proj.frame < 2) return;

            Vector2 pos = proj.Center;
            Vector2 vect = proj.localAI[1].ToRotationVector2();
            vect = Vector2.Normalize(vect) * (proj.width / 2f * proj.scale);
            vect = vect.RotatedBy(Utils.getRand(-10, 10) * 0.01f);
            pos += vect;

            Projectile.NewProjectile(null, pos, Vector2.Zero, ExtenManag.ProjectileType<EProj2>(),
                proj.damage, proj.knockBack, proj.owner,
                proj.ai[0], proj.ai[1],
                modifer: p =>
                {
                    p.localAI[0] = proj.localAI[0] - 1;
                    p.localAI[1] = proj.localAI[1] + (Utils.getRand(-10, 10) * 0.01f);
                });

            proj.localAI[0] = 0;
        }

        public override Color? GetAlpha(Projectile proj, Color newColor)
        {
            return Color.White;
        }
    }
}
