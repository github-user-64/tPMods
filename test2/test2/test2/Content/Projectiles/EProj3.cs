using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using test2.Content.Items;

namespace test2.Content.Projectiles
{
    internal class EProj3 : ExtenProjectile
    {
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
            proj.timeLeft = 60 * 5;
            proj.tileCollide = false;//图格碰撞
            proj.ignoreWater = true;//无视水
            proj.penetrate = -1;//穿透次数, -1无限
            proj.friendly = true;//友好
        }

        public override bool? Colliding(Projectile proj, Rectangle myRect, Rectangle targetRect)
        {
            return false;
        }

        public override void NewProjectilePostfix(Projectile proj, IEntitySource spawnSource)
        {
            SoundEngine.PlaySound(SoundID.Item84);
        }

        public override void AI(Projectile proj)
        {
            if (Main.player.IndexInRange(proj.owner) != true) return;
            Player player = Main.player[proj.owner];

            if (player == Main.LocalPlayer)
            {
                if (player.active != true || player.dead == true ||
                    (player.HeldItem.type == ExtenManag.ItemType<EItem1>() && Main.mouseRight) != true)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath6, proj.Center);
                    proj.Kill();
                    return;
                }

                AI(proj, player);
            }

            Vector2 v = new Vector2(proj.localAI[0], proj.localAI[1]);
            if (v == Vector2.Zero) v = Vector2.UnitX;
            v = v.RotatedBy(5f * (MathHelper.TwoPi / v.Length()));
            v += Vector2.Normalize(v) * 5;
            proj.localAI[0] = v.X;
            proj.localAI[1] = v.Y;

            Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, false, true);
            proj.Center = pos;
        }

        private void AI(Projectile proj, Player player)
        {
            if (Main.GameUpdateCount % 3 != 0) return;

            float len = new Vector2(proj.localAI[0], proj.localAI[1]).Length();

            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy() == false && npc.type != NPCID.TargetDummy) continue;//是敌怪
                if (npc.Center.Distance(proj.Center) > len) continue;

                Projectile.SpawnMoonLordWhipProc(proj, npc, proj.damage, 0);
            }
        }

        public override bool PreDraw(Projectile proj, Color lightColor, Player player = null)
        {
            Texture2D img = Asset.Request<Texture2D>("a1").Value;

            Vector2 velocity = new Vector2(proj.localAI[0], proj.localAI[1]);
            Rectangle size = new Rectangle(0, 0, proj.width, proj.height);
            int len = 4;

            for (int i = len - 1; i >= 0; --i)
            {
                Vector2 v = velocity.RotatedBy((i * -3f) * (MathHelper.TwoPi / velocity.Length()));
                float bl = (1f / len) * (len - i);

                For(proj.Center, v, proj, pos =>
                {
                    float scale = (proj.scale + 0.1f) * bl;
                    if (scale <= 0f) return;
                    Color color = proj.GetAlpha(lightColor) * 0.5f * bl;

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

        public override void PostDraw(Projectile proj, Color lightColor, Player player = null)
        {
            Texture2D img = Asset.Request<Texture2D>(Texture).Value;

            Vector2 velocity = new Vector2(proj.localAI[0], proj.localAI[1]);
            Rectangle size = new Rectangle(0, 0, proj.width, proj.height);
            Color color = proj.GetAlpha(lightColor);

            For(proj.Center, velocity, proj, pos =>
            {
                //将添加到绘制位置的弹丸实际位置的偏移量,用于抵消一些持有的投射物以匹配玩家
                //从而使投射物在视觉上与玩家保持同步
                pos.Y += proj.gfxOffY;
                pos -= Main.screenPosition;

                Main.EntitySpriteDraw(img, pos, size, color,
                    proj.velocity.ToRotation(), size.Size() / 2f, proj.scale, SpriteEffects.None);
            });
        }

        public override Color? GetAlpha(Projectile proj, Color newColor)
        {
            return Color.White;
        }

        protected void For(Vector2 position, Vector2 velocity, Projectile proj, Action<Vector2> foo)
        {
            float count = 8;

            for (int i = 0; i < count; ++i)
            {
                Vector2 v = velocity.RotatedBy(MathHelper.TwoPi / count * i);

                Vector2 pos = position + v;
                foo(pos);

                v.X *= -1;
                pos = position + v;
                foo(pos);
            }
        }
    }
}
