using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace test2.Content.Projectiles
{
    internal class EProj1 : ExtenProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_454";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
            //存储路径的长度,用于绘制轨迹,每种射弹类型默认为10
            ///必须与<see cref ="ProjectileID.Settes.TrailingMode"/>一起使用才能正确使用
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
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
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefault(Projectile proj)
        {
            proj.width = 62;
            proj.height = 62;
            proj.aiStyle = 0;
        }

        public override bool PreDraw(Projectile proj, Player player = null)
        {
            if (proj.velocity == Vector2.Zero) return true;

            Texture2D txt = Asset.Request<Texture2D>(Texture).Value;

            float offX = ((TextureAssets.Projectile[Type].Width() - proj.width) * 0.5f) + (proj.width * 0.5f);

            for (int i = ProjectileID.Sets.TrailCacheLength[Type] - 1; i >= 0; --i)
            //for (int i = 0; i < 1; ++i)
            {
                Rectangle size = new Rectangle(0, 0, proj.width, proj.height);
                Vector2 pos = proj.oldPos[i];
                float scale = 1 - ((i + 1) * 0.1f);
                if (scale <= 0) break;
                Color color = Color.White;
                //color.A = (byte)MathHelper.Clamp(color.A * (scale * 0.8f), 0, byte.MaxValue);
                color *= scale * 0.8f;

                pos.X += offX;
                pos.Y += proj.height / 2f;

                //将添加到绘制位置的弹丸实际位置的偏移量,用于抵消一些持有的投射物以匹配玩家
                //从而使投射物在视觉上与玩家保持同步
                pos.Y += proj.gfxOffY;
                pos -= Main.screenPosition;

                Main.EntitySpriteDraw(txt, pos, size, color,
                    proj.velocity.ToRotation(), size.Size() / 2f, scale, SpriteEffects.None);
            }

            return true;
        }
    }
}
