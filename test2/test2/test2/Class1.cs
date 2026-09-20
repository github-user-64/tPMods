using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace test2
{
    internal class Class1
    {
        public void DrawProjDirect(Projectile proj, Player overridePlayer = null)
        {
            PrepareDrawnProjectileDrawing(proj);
            Player player = Main.player[proj.owner];
            if (overridePlayer != null)
            {
                player = overridePlayer;
            }
            bool flag = proj.isAPreviewDisplayDoll || player.isDisplayDollOrInanimate;

            //
            float polePosX = 0f;
            float polePosY = 0f;
            LoadProjectile(proj.type);
            Vector2 mountedCenter = player.MountedCenter;
            if (player.mount.Active && player.mount.Type == 52)
            {
                mountedCenter += new Vector2(player.direction * 14, -10f);
            }
            //

            if (proj.aiStyle == 99 && proj.ai[0] != -2f)
            {
                Vector2 pos = mountedCenter;
                pos.Y += player.gfxOffY;
                player.ApplyItemPositionOffsetFromMount(ref pos);
                float num = proj.Center.X - pos.X;
                float num2 = proj.Center.Y - pos.Y;
                Math.Sqrt(num * num + num2 * num2);
                float num3 = (float)Math.Atan2(num2, num) - 1.57f;
                bool flag2 = true;
                bool flag3 = true;
                if (num == 0f && num2 == 0f)
                {
                    flag2 = false;
                }
                else
                {
                    float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                    num4 = 12f / num4;
                    num *= num4;
                    num2 *= num4;
                    pos.X -= num * 0.1f;
                    pos.Y -= num2 * 0.1f;
                    num = proj.position.X + (float)proj.width * 0.5f - pos.X;
                    num2 = proj.position.Y + (float)proj.height * 0.5f - pos.Y;
                }
                while (flag2)
                {
                    float num5 = 12f;
                    float num6 = (float)Math.Sqrt(num * num + num2 * num2);
                    float num7 = num6;
                    if (float.IsNaN(num6) || float.IsNaN(num7))
                    {
                        flag2 = false;
                        continue;
                    }
                    if (num6 < 20f)
                    {
                        num5 = num6 - 8f;
                        flag2 = false;
                    }
                    num6 = 12f / num6;
                    num *= num6;
                    num2 *= num6;
                    if (flag3)
                    {
                        flag3 = false;
                    }
                    else
                    {
                        pos.X += num;
                        pos.Y += num2;
                    }
                    num = proj.position.X + (float)proj.width * 0.5f - pos.X;
                    num2 = proj.position.Y + (float)proj.height * 0.1f - pos.Y;
                    if (num7 > 12f)
                    {
                        float num8 = 0.3f;
                        float num9 = Math.Abs(proj.velocity.X) + Math.Abs(proj.velocity.Y);
                        if (num9 > 16f)
                        {
                            num9 = 16f;
                        }
                        num9 = 1f - num9 / 16f;
                        num8 *= num9;
                        num9 = num7 / 80f;
                        if (num9 > 1f)
                        {
                            num9 = 1f;
                        }
                        num8 *= num9;
                        if (num8 < 0f)
                        {
                            num8 = 0f;
                        }
                        num8 *= num9;
                        num8 *= 0.5f;
                        if (num2 > 0f)
                        {
                            num2 *= 1f + num8;
                            num *= 1f - num8;
                        }
                        else
                        {
                            num9 = Math.Abs(proj.velocity.X) / 3f;
                            if (num9 > 1f)
                            {
                                num9 = 1f;
                            }
                            num9 -= 0.5f;
                            num8 *= num9;
                            if (num8 > 0f)
                            {
                                num8 *= 2f;
                            }
                            num2 *= 1f + num8;
                            num *= 1f - num8;
                        }
                    }
                    num3 = (float)Math.Atan2(num2, num) - 1.57f;
                    Color white = Color.White;
                    white.A = (byte)((float)(int)white.A * 0.4f);
                    white = TryApplyingPlayerStringColor(player.stringColor, white);
                    float num10 = 0.5f;
                    if (player.stringColor != 29)
                    {
                        white = Lighting.GetColor((int)pos.X / 16, (int)(pos.Y / 16f), white);
                    }
                    EntitySpriteDraw(color: new Color((byte)((float)(int)white.R * num10), (byte)((float)(int)white.G * num10), (byte)((float)(int)white.B * num10), (byte)((float)(int)white.A * num10)), texture: TextureAssets.FishingLine.Value, position: new Vector2(pos.X - screenPosition.X + (float)TextureAssets.FishingLine.Width() * 0.5f, pos.Y - screenPosition.Y + (float)TextureAssets.FishingLine.Height() * 0.5f) - new Vector2(6f, 0f), sourceRectangle: new Rectangle(0, 0, TextureAssets.FishingLine.Width(), (int)num5), rotation: num3, origin: new Vector2((float)TextureAssets.FishingLine.Width() * 0.5f, 0f), scale: 1f, effects: SpriteEffects.None);
                }
                if (proj.ai[0] == -3f)
                {
                    return;
                }
            }
            else
            {
                if (proj.aiStyle == 160)
                {
                    DrawKite(proj, player.GetArmPosition() + Main.player[proj.owner].netOffset, WindForVisuals);
                    return;
                }
                if (proj.aiStyle == 165)
                {
                    DrawWhip(proj, player);
                    return;
                }
            }
            if (proj.aiStyle == 174)
            {
                DrawMultisegmentPet(proj);
                return;
            }
            int num11 = ProjectileID.Sets.DespawnItemIcon[proj.type];
            if (num11 != 0)
            {
                TryDespawningProjectile(proj, num11);
                if (!proj.active)
                {
                    return;
                }
            }

            if (proj.bobber && player.inventory[player.selectedItem].holdStyle != 0)
            {
                DrawProj_FishingLine(proj, player, ref polePosX, ref polePosY, mountedCenter);
            }
            if (proj.aiStyle == 7)
            {
                Vector2 vector23 = new Vector2(proj.position.X + (float)proj.width * 0.5f, proj.position.Y + (float)proj.height * 0.5f);
                float num119 = mountedCenter.X - vector23.X;
                float num120 = mountedCenter.Y - vector23.Y;
                float rotation17 = (float)Math.Atan2(num120, num119) - 1.57f;
                bool flag23 = true;
                while (flag23)
                {
                    float num121 = (float)Math.Sqrt(num119 * num119 + num120 * num120);
                    if (num121 < 25f)
                    {
                        flag23 = false;
                        continue;
                    }
                    if (float.IsNaN(num121))
                    {
                        flag23 = false;
                        continue;
                    }
                    num121 = 12f / num121;
                    num119 *= num121;
                    num120 *= num121;
                    vector23.X += num119;
                    vector23.Y += num120;
                    num119 = mountedCenter.X - vector23.X;
                    num120 = mountedCenter.Y - vector23.Y;
                    Color color30 = Lighting.GetColor((int)vector23.X / 16, (int)(vector23.Y / 16f));
                    EntitySpriteDraw(TextureAssets.Chain.Value, new Vector2(vector23.X - screenPosition.X, vector23.Y - screenPosition.Y), new Rectangle(0, 0, TextureAssets.Chain.Width(), TextureAssets.Chain.Height()), color30, rotation17, new Vector2((float)TextureAssets.Chain.Width() * 0.5f, (float)TextureAssets.Chain.Height() * 0.5f), 1f, SpriteEffects.None);
                }
            }
            if (proj.aiStyle == 13)
            {
                float num139 = proj.position.X + 8f;
                float num140 = proj.position.Y + 2f;
                float x10 = proj.velocity.X;
                float num141 = proj.velocity.Y;
                if (x10 == 0f && num141 == 0f)
                {
                    num141 = 0.0001f;
                    num141 = 0.0001f;
                }
                float num142 = (float)Math.Sqrt(x10 * x10 + num141 * num141);
                num142 = ((proj.type != 23) ? (20f / num142) : (40f / num142));
                if (proj.ai[0] == 0f)
                {
                    num139 -= proj.velocity.X * num142;
                    num140 -= proj.velocity.Y * num142;
                }
                else
                {
                    num139 += proj.velocity.X * num142;
                    num140 += proj.velocity.Y * num142;
                }
                Vector2 vector29 = new Vector2(num139, num140);
                if (proj.type == 23)
                {
                    if (proj.ai[0] == 0f)
                    {
                        proj.localAI[1] = (0f - proj.velocity.X + proj.localAI[1]) / 2f;
                        proj.localAI[2] = (0f - proj.velocity.Y + proj.localAI[2]) / 2f;
                    }
                    else
                    {
                        proj.localAI[1] = (proj.velocity.X + proj.localAI[1] * 4f) / 5f;
                        proj.localAI[2] = (proj.velocity.Y + proj.localAI[2] * 4f) / 5f;
                    }
                }
                if (proj.type == 23)
                {
                    Vector2 vector30 = vector29 - mountedCenter;
                    if (vector30.Length() > 20f)
                    {
                        vector30.Normalize();
                        vector30 *= 20f;
                        mountedCenter.X += vector30.X;
                        mountedCenter.Y += vector30.Y;
                    }
                    x10 = mountedCenter.X - vector29.X;
                    num141 = mountedCenter.Y - vector29.Y;
                    float num143 = (float)Math.Sqrt(x10 * x10 + num141 * num141);
                    num143 = 12f / num143;
                    x10 *= num143;
                    num141 *= num143;
                    vector29.X -= x10 / 2f;
                    vector29.Y -= num141 / 2f;
                }
                x10 = mountedCenter.X - vector29.X;
                num141 = mountedCenter.Y - vector29.Y;
                float rotation22 = (float)Math.Atan2(num141, x10) - 1.57f;
                bool flag28 = true;
                while (flag28)
                {
                    float num144 = (float)Math.Sqrt(x10 * x10 + num141 * num141);
                    if (num144 < 25f)
                    {
                        flag28 = false;
                        continue;
                    }
                    if (float.IsNaN(num144))
                    {
                        flag28 = false;
                        continue;
                    }
                    float num145 = num144;
                    num144 = 12f / num144;
                    x10 *= num144;
                    num141 *= num144;
                    vector29.X += x10;
                    vector29.Y += num141;
                    x10 = mountedCenter.X - vector29.X;
                    num141 = mountedCenter.Y - vector29.Y;
                    if (proj.type == 23 && num145 > 12f)
                    {
                        float num146 = num145 / 1000f;
                        if ((double)num146 > 0.5)
                        {
                            num146 = 0.5f;
                        }
                        Vector2 vector31 = mountedCenter - vector29;
                        Vector2 vector32 = new Vector2(proj.localAI[1], proj.localAI[2]);
                        float num147 = vector31.Length();
                        vector31.Normalize();
                        vector32.Normalize();
                        vector31 *= num147;
                        vector32 *= num147;
                        x10 = vector31.X * (1f - num146) + vector32.X * num146;
                        num141 = vector31.Y * (1f - num146) + vector32.Y * num146;
                        rotation22 = (float)Math.Atan2(num141, x10) - 1.57f;
                    }
                    Color color35 = Lighting.GetColor((int)vector29.X / 16, (int)(vector29.Y / 16f));
                    EntitySpriteDraw(TextureAssets.Chain.Value, new Vector2(vector29.X - screenPosition.X, vector29.Y - screenPosition.Y), new Rectangle(0, 0, TextureAssets.Chain.Width(), TextureAssets.Chain.Height()), color35, rotation22, new Vector2((float)TextureAssets.Chain.Width() * 0.5f, (float)TextureAssets.Chain.Height() * 0.5f), 1f, SpriteEffects.None);
                }
            }
            else if (proj.aiStyle == 15)
            {
                DrawProj_FlailChains(proj, player, mountedCenter);
            }

            //
            Color projectileColor = Lighting.GetColor((int)((double)proj.position.X + (double)proj.width * 0.5) / 16, (int)(((double)proj.position.Y + (double)proj.height * 0.5) / 16.0));
            if (proj.usesOwnerLight)
            {
                projectileColor = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            //

            int num148 = 0;
            int num149 = 0;
            

            SpriteEffects dir = SpriteEffects.None;
            if (proj.spriteDirection == -1)
            {
                dir = SpriteEffects.FlipHorizontally;
            }

            if (proj.aiStyle == 1 && new Projectile.AI_001_Features(proj.ai[2]).PhoenixQuivered)
            {
                Texture2D value17 = TextureAssets.Extra[91].Value;
                Rectangle value18 = value17.Frame();
                Vector2 origin7 = new Vector2((float)value18.Width / 2f, 10f);
                Vector2 value19 = new Vector2(0.6f, 0.8f);
                float num151 = 0.5f + 0.05f * (float)Math.Sin(GlobalTimeWrappedHourly % 1f * ((float)Math.PI * 2f));
                Vector2 value20 = new Vector2(0f, proj.gfxOffY);
                new Vector2(0f, -10f);
                _ = (float)timeForVisualEffects / 60f;
                Vector2 value21 = proj.Center + proj.velocity.SafeNormalize(Vector2.Zero) * 2f;
                Color color36 = Color.Lerp(Color.Orange, OurFavoriteColor, Utils.PingPongFrom01To010(GlobalTimeWrappedHourly % 3f));
                color36.A = 127;
                float num152 = 0f;
                float rotation23 = proj.velocity.ToRotation() + (float)Math.PI / 2f;
                EntitySpriteDraw(value17, value21 - screenPosition + value20, value18, new Color(255, 40, 0, 180) * 0.75f, rotation23, origin7, value19 * (1.5f + num152) * (0.8f + num151 * 0.1f), SpriteEffects.None);
                EntitySpriteDraw(value17, value21 - screenPosition + value20, value18, color36, rotation23, origin7, value19 * (1.5f + num152) * num151, SpriteEffects.None);
            }

                if (projFrames[proj.type] > 1)
                {
                    Texture2D value204 = TextureAssets.Projectile[proj.type].Value;
                    bool flag42 = false;

                    int num456 = value204.Width;
                    int num457 = value204.Height / projFrames[proj.type];
                    int y25 = num457 * proj.frame;
                    int x15 = 0;
                    if (flag && flag42)
                    {
                        num457 = value204.Height;
                    }

                    Color alpha14 = proj.GetAlpha(projectileColor);

                    bool flag43 = false;

                    Vector2 origin39 = new Vector2(num150, proj.height / 2 + num148);
                    if (flag)
                    {
                        int type = proj.type;

                    }
                    if (!flag43)
                    {
                        EntitySpriteDraw(value204, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(x15, y25, num456, num457 - 1), alpha14, proj.rotation, origin39, proj.scale, dir);
                    }

                    if (proj.type != 525 && proj.type != 960)
                    {
                        return;
                    }
                    int num470 = TryInteractingWithMoneyTrough(proj);
                    if (num470 == 0)
                    {
                        return;
                    }
                    int num471 = (projectileColor.R + projectileColor.G + projectileColor.B) / 3;
                    if (num471 > 10)
                    {
                        int num472 = 94;
                        if (proj.type == 960)
                        {
                            num472 = 244;
                        }
                        Color selectionGlowColor = Colors.GetSelectionGlowColor(num470 == 2, num471);
                        EntitySpriteDraw(TextureAssets.Extra[num472].Value, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, y25, num456, num457 - 1), selectionGlowColor, proj.rotation, new Vector2(num150, proj.height / 2 + num148), 1f, dir);
                    }
                    return;
                }
                
                if (proj.aiStyle == 27)
                {
                    EntitySpriteDraw(TextureAssets.Projectile[proj.type].Value, new Vector2(proj.position.X - screenPosition.X + (float)(proj.width / 2), proj.position.Y - screenPosition.Y + (float)(proj.height / 2)), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), proj.GetAlpha(projectileColor), proj.rotation, new Vector2(TextureAssets.Projectile[proj.type].Width(), 0f), proj.scale, dir);
                    return;
                }
                if (proj.aiStyle == 19)
                {
                    DrawProj_Spear(proj, player, ref projectileColor, ref dir);
                    return;
                }

                if (proj.bobber)
                {
                    if (proj.ai[1] > 0f && proj.ai[1] < (float)ItemID.Count && proj.ai[0] == 1f)
                    {
                        int num503 = (int)proj.ai[1];
                        Vector2 center8 = proj.Center;
                        float rotation35 = proj.rotation;
                        Vector2 vector75 = center8;
                        float num504 = polePosX - vector75.X;
                        float num505 = polePosY - vector75.Y;
                        rotation35 = (float)Math.Atan2(num505, num504);
                        if (proj.velocity.X > 0f)
                        {
                            dir = SpriteEffects.None;
                            rotation35 = (float)Math.Atan2(num505, num504);
                            rotation35 += 0.785f;
                            if (proj.ai[1] == 2342f)
                            {
                                rotation35 -= 0.785f;
                            }
                        }
                        else
                        {
                            dir = SpriteEffects.FlipHorizontally;
                            rotation35 = (float)Math.Atan2(0f - num505, 0f - num504);
                            rotation35 -= 0.785f;
                            if (proj.ai[1] == 2342f)
                            {
                                rotation35 += 0.785f;
                            }
                        }
                        instance.LoadItem(num503);
                        Texture2D value218 = TextureAssets.Item[num503].Value;
                        Rectangle value219 = value218.Frame();
                        if (ItemID.Sets.IsFood[num503] && itemAnimations[num503] != null)
                        {
                            value219 = itemAnimations[num503].GetFrame(value218, 0);
                        }
                        EntitySpriteDraw(value218, new Vector2(center8.X - screenPosition.X, center8.Y - screenPosition.Y), value219, projectileColor, rotation35, new Vector2(value219.Width / 2, value219.Height / 2), proj.scale, dir);
                    }
                    else if (proj.ai[0] <= 1f)
                    {
                        EntitySpriteDraw(TextureAssets.Projectile[proj.type].Value, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), proj.GetAlpha(projectileColor), proj.rotation, new Vector2(num150, proj.height / 2 + num148), proj.scale, dir);
                        if (proj.glowMask != -1)
                        {
                            Texture2D value220 = TextureAssets.GlowMask[proj.glowMask].Value;
                            Color newColor5 = Color.White;
                            if (proj.type == 993)
                            {
                                newColor5 = new Color(DiscoR, DiscoG, DiscoB);
                            }
                            EntitySpriteDraw(value220, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, value220.Width, value220.Height), proj.GetAlpha(newColor5), proj.rotation, new Vector2(num150, proj.height / 2 + num148), proj.scale, dir);
                        }
                    }
                }
                else
                {
                    if (proj.ownerHitCheck && player.gravDir == -1f)
                    {
                        if (player.direction == 1)
                        {
                            dir = SpriteEffects.FlipHorizontally;
                        }
                        else if (player.direction == -1)
                        {
                            dir = SpriteEffects.None;
                        }
                    }
                    Texture2D value221 = TextureAssets.Projectile[proj.type].Value;
                    Vector2 origin41 = new Vector2(num150, proj.height / 2 + num148);
                    if (flag)
                    {
                        if (proj.aiStyle == 2)
                        {
                            origin41 = new Vector2(value221.Width / 2, value221.Height - 4);
                        }
                        else if (proj.aiStyle == 3)
                        {
                            origin41 = new Vector2(value221.Width / 2, value221.Height / 2);
                            switch (proj.type)
                            {
                                case 6:
                                case 19:
                                case 52:
                                case 113:
                                case 867:
                                    origin41 = new Vector2((player.direction == -1) ? 4 : (value221.Width - 4), value221.Height - 4);
                                    break;
                                case 272:
                                    origin41 = new Vector2((player.direction == -1) ? 8 : (value221.Width - 8), value221.Height - 4);
                                    break;
                                case 1052:
                                    origin41 = new Vector2(value221.Width / 2, value221.Height - 4);
                                    break;
                                case 182:
                                case 320:
                                    origin41 = new Vector2((player.direction == 1) ? 4 : (value221.Width - 4), value221.Height - 4);
                                    break;
                            }
                        }
                        else if (proj.aiStyle == 9 && proj.type == 491)
                        {
                            origin41 = new Vector2(value221.Width / 2, value221.Height - 4);
                        }
                    }
                    EntitySpriteDraw(value221, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), proj.GetAlpha(projectileColor), proj.rotation, origin41, proj.scale, dir);
                    if (proj.glowMask != -1)
                    {
                        Color color114 = new Color(250, 250, 250, proj.alpha);
                        if (proj.type == 1101 || proj.type == 1102)
                        {
                            color114 = Item.GetPhaseColor(proj.type);
                            EntitySpriteDraw(TextureAssets.GlowMask[proj.glowMask].Value, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), color114, proj.rotation, origin41, proj.scale, dir);
                            color114 = Item.GetPhaseColor(proj.type, drawColor: true);
                        }
                        EntitySpriteDraw(TextureAssets.GlowMask[proj.glowMask].Value, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), color114, proj.rotation, origin41, proj.scale, dir);
                    }
                    if (proj.type == 473)
                    {
                        EntitySpriteDraw(value221, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), new Color(255, 255, 0, 0), proj.rotation, origin41, proj.scale, dir);
                    }
                    if (proj.type >= 511 && proj.type <= 513)
                    {
                        EntitySpriteDraw(value221, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY), new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), proj.GetAlpha(projectileColor) * 0.25f, proj.rotation, origin41, proj.scale * (1f + proj.Opacity * 1.75f), dir);
                    }
                    if (proj.type == 312)
                    {
                        ulong seed4 = TileFrameSeed;
                        for (int num506 = 0; num506 < 4; num506++)
                        {
                            Vector2 value222 = new Vector2(Utils.RandomInt(ref seed4, -2, 3), Utils.RandomInt(ref seed4, -2, 3));
                            EntitySpriteDraw(TextureAssets.GlowMask[proj.glowMask].Value, new Vector2(proj.position.X - screenPosition.X + num150 + (float)num149, proj.position.Y - screenPosition.Y + (float)(proj.height / 2) + proj.gfxOffY) + value222, new Rectangle(0, 0, TextureAssets.Projectile[proj.type].Width(), TextureAssets.Projectile[proj.type].Height()), new Color(255, 255, 255, 255) * 0.2f, proj.rotation, origin41, proj.scale, dir);
                        }
                    }
                }
            
        }
    }
}
