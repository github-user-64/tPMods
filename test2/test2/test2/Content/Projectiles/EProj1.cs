using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace test2.Content.Projectiles
{
    internal class EProj1 : ExtenProjectile
    {
        protected const int ProjLen = 8;
        public override string Texture => "Terraria/Images/Projectile_454";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
            //存储路径的长度,用于绘制轨迹,每种射弹类型默认为10
            ///必须与<see cref ="ProjectileID.Settes.TrailingMode"/>一起使用才能正确使用
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            // 追踪模式,确定弹丸轨迹将记住哪些数据,每种投射物类型默认为 - 1，这意味着不保存任何信息
            //0：只记住位置数据
            //1：不应使用
            //2：位置、旋转和精灵方向数据被记住
            //3：与2相同，但试图通过插值来平滑旧数据
            //4：与2相同，但调整旧数据以跟随玩家所有者
            ///必须与
            ///<see cref="ProjectileID.Sets.TrailCacheLength"/>,
            ///<see cref="Projectile.oldPos"/>,
            ///<see cref="Projectile.oldRot"/>,
            ///<see cref="Projectile.oldSpriteDirection"/>一起使用
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefault(Projectile proj)
        {
            proj.width = 62;
            proj.height = 62;
            proj.scale = 1f;
            proj.aiStyle = 0;
            proj.timeLeft = 15;
            proj.tileCollide = false;//图格碰撞
            proj.ignoreWater = true;//无视水
            proj.penetrate = -1;//穿透次数, -1无限
            proj.friendly = true;//友好
        }

        public override void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            SoundEngine.PlaySound(SoundID.Item88, -1, -1);
        }

        public override void AI(Projectile proj)
        {
            Player player = Main.player[proj.owner];

            proj.velocity = proj.velocity.RotatedBy(proj.ai[0] > 0 ? 0.17f : -0.17f);

            Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, false, true);
            pos += proj.velocity.RotatedBy(proj.ai[0] > 0 ? 0.5f : -0.5f);

            proj.Center = pos;

            proj.rotation = proj.velocity.ToRotation();
        }

        public override void OnKill(Projectile proj)
        {
            For(proj.position, proj.velocity, proj, pos =>
            {
                Projectile.NewProjectile(null, pos, Vector2.Zero, ExtenManag.ProjectileType<EProj2>(),
                    proj.damage, proj.knockBack, proj.owner,
                    1.5f);
            });
        }



        public override bool? Colliding(Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            float interval = 10f;//间隔
            Vector2 posOff1 = Vector2.Normalize(proj.velocity) * ((proj.width * proj.scale + interval) * ProjLen);
            posOff1 -= Vector2.Normalize(posOff1) * interval;

            Vector2 start = proj.Center;
            Vector2 end = proj.Center + posOff1;

            float collisionPoint = 0f;//碰撞点

            return Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(),
                start, end, proj.width / 2f * proj.scale, ref collisionPoint);
        }

        public override bool PreDraw(Projectile proj, Player player = null)
        {
            Texture2D img = Asset.Request<Texture2D>("a1").Value;

            Rectangle size = new Rectangle(0, 0, proj.width, proj.height);
            int len = ProjectileID.Sets.TrailCacheLength[Type];

            for (int i = len - 1; i >= 0; --i)
            {
                Vector2 p = proj.oldPos[i] + (size.Size() / 2f);
                Vector2 v = proj.oldRot[i].ToRotationVector2();
                float bl = (1f / len) * (len - i);

                For(p, v, proj, pos =>
                {
                    float scale = (proj.scale + 0.1f) * bl;
                    if (scale <= 0f) return;
                    Color color = Color.White * 0.5f * bl;

                    //将添加到绘制位置的弹丸实际位置的偏移量,用于抵消一些持有的投射物以匹配玩家
                    //从而使投射物在视觉上与玩家保持同步
                    pos.Y += proj.gfxOffY;
                    pos -= Main.screenPosition;

                    Main.EntitySpriteDraw(img, pos, size, color,
                        v.ToRotation(), size.Size() / 2f, scale, SpriteEffects.None);
                });
            }

            return false;
        }

        public override void PostDraw(Projectile proj, Player player = null)
        {
            Texture2D img = Asset.Request<Texture2D>(Texture).Value;

            Rectangle size = new Rectangle(0, 0, proj.width, proj.height);
            Color color = Color.White;

            For(proj.Center, proj.velocity, proj, pos =>
            {
                //将添加到绘制位置的弹丸实际位置的偏移量,用于抵消一些持有的投射物以匹配玩家
                //从而使投射物在视觉上与玩家保持同步
                pos.Y += proj.gfxOffY;
                pos -= Main.screenPosition;

                Main.EntitySpriteDraw(img, pos, size, color,
                    proj.velocity.ToRotation(), size.Size() / 2f, proj.scale, SpriteEffects.None);
            });
        }

        private void For(Vector2 position, Vector2 velocity, Projectile proj, Action<Vector2> foo)
        {
            float scale = proj.scale;
            if (scale <= 0) return;

            int width = proj.width;

            for (int i = 0; i < ProjLen; ++i)
            {
                Vector2 pos = position;
                pos += Vector2.Normalize(velocity) * ((width * scale + 10) * i);

                foo(pos);
            }

            Vector2 posOff1 = Vector2.Normalize(velocity) * ((width * scale + 10) * 1);
            Vector2 posOff2 = posOff1.RotatedBy(MathHelper.TwoPi / 4f);
            posOff1 -= posOff2;

            for (int i = 0; i < 3; i += 2)
            {
                Vector2 pos = position;
                pos += posOff1 + (posOff2 * i);

                foo(pos);
            }
        }
    }
}
