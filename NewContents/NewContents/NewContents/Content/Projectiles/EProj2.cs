using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace NewContents.Content.Projectiles
{
    internal class EProj2 : ExtenProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_645";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "刻意的爆炸");

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
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

            proj.usesLocalNPCImmunity = true;//使用局部免疫框架
            proj.localNPCHitCooldown = 0;//当与usesLocalNPCImmunity结合使用时，确定此投射物必须经过多少滴答声才能再次对同一npc造成伤害。值-1表示它只能击中特定的npc一次。默认值-2无效
        }

        public override void AI(Projectile proj)
        {
            if (proj.localAI[1] == 0)
            {
                proj.localAI[1] = 1;
                proj.scale = proj.ai[0];
                proj.rotation = MathHelper.TwoPi / 360f * Utils.getRand(0, 89);
            }

            if (Main.GameUpdateCount % 4 != 0) return;

            int frame = proj.frame + 1;
            if (frame > 3 && proj.ai[1]-- > 0) frame = 0;
            if (frame >= Main.projFrames[Type])
            {
                proj.Kill();
                return;
            }
            proj.frame = frame;

            if (proj.owner != Main.myPlayer) return;
            Player player = Main.player[proj.owner];

            if (proj.localAI[0] < 1) return;
            if (proj.frame < 2) return;

            Vector2 pos = proj.Center;
            Vector2 vect = proj.velocity;
            vect = Vector2.Normalize(vect) * (proj.width / 2f * proj.scale);
            vect = vect.RotatedBy(Utils.getRand(-10, 10) * 0.01f);
            pos += vect;

            Projectile.NewProjectile(null, pos, vect, ExtenManag.ProjectileType<EProj2>(),
                proj.damage, proj.knockBack, proj.owner,
                proj.ai[0], proj.ai[1],
                modifer: p =>
                {
                    p.localAI[0] = proj.localAI[0] - 1;
                });

            proj.localAI[0] = 0;
        }

        public override bool CanUpdatePosition(Projectile proj, Vector2 wetVelocity) => false;

        public override Color? GetAlpha(Projectile proj, Color newColor) => new Color(255, 255, 255, 127);
    }
}
