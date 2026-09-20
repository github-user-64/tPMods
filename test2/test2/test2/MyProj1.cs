using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace test2
{
    internal class MyProj1 : ExtenProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_454";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "蛇胆1");

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
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefault(Projectile proj)
        {
            proj.width = 62;
            proj.height = 62;
        }

        public override void PostDraw(Projectile proj, Player player = null)
        {
            if (proj.velocity == Vector2.Zero) return;

            Texture2D img = Asset.Request<Texture2D>(Texture).Value;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; ++i)
            {
                Rectangle size = proj.getRect();
                Vector2 pos = proj.oldPos[i];
                float scale = 1 - (i * 0.1f);
                Color color = Color.White;
                color.A = (byte)MathHelper.Clamp(color.A - scale * 10, 0, byte.MaxValue);

                pos += Main.screenPosition;

                Main.EntitySpriteDraw(img, pos, new Rectangle(size), color,
                    proj.velocity.ToRotation(),  size, scale, SpriteEffects.None);
            }

            //int rect = glow.Height;
            //int rect2 = 0;
            //Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect);
            //Vector2 gloworigin2 = glowrectangle.Size() / 2f;
            //for (int i = 0; i < 8; i++)
            //{
            //    Color glowcolor = Color.Lerp(new Color(196, 247, 255, 0), Color.Transparent, 0.9f);
            //    glowcolor *= proj.Opacity;
            //    float increment = MathHelper.Lerp(1f, 0.05f, (float)i / 8f);
            //    for (float j = 0f; j < (float)ProjectileID.Sets.TrailCacheLength[proj.type]; j += increment)
            //    {
            //        Color color27 = glowcolor;
            //        color27 *= ((float)ProjectileID.Sets.TrailCacheLength[proj.type] - j) / (float)ProjectileID.Sets.TrailCacheLength[proj.type];
            //        float scale = proj.scale * ((float)ProjectileID.Sets.TrailCacheLength[proj.type] - j) / (float)ProjectileID.Sets.TrailCacheLength[proj.type];
            //        int max0 = (int)j - 1;
            //        bool flag2 = max0 < 0;
            //        if (!flag2)
            //        {
            //            Vector2 oldPos = Vector2.Lerp(proj.oldPos[(int)j], proj.oldPos[max0], 1f - j % 1f);
            //            float oldRot = MathHelper.Lerp(proj.oldRot[(int)j], proj.oldRot[max0], 1f - j % 1f);
            //            Vector2 trailOffset = Vector2.Normalize(oldRot.ToRotationVector2()) * 80f * proj.scale * (float)i;
            //            Main.EntitySpriteDraw(glow, oldPos + proj.Size / 2f + trailOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY), new Rectangle?(glowrectangle), color27, proj.velocity.ToRotation() + 1.5707964f, gloworigin2, scale * 1.5f, 0, 0f);
            //        }
            //    }
            //    glowcolor = Color.Lerp(new Color(255, 255, 255, 0), Color.Transparent, 0.85f);
            //    Vector2 offset = Vector2.Normalize(proj.velocity) * 80f * proj.scale * (float)i;
            //    Main.EntitySpriteDraw(glow, proj.position + proj.Size / 2f + offset - Main.screenPosition + new Vector2(0f, proj.gfxOffY), new Rectangle?(glowrectangle), glowcolor, proj.velocity.ToRotation() + 1.5707964f, gloworigin2, proj.scale * 1.5f, 0, 0f);
        }
    }
}
