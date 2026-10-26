using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;

namespace ExtenContent.Extens
{
    public static partial class SurfaceBackgroundLoader
    {
        internal static bool DrawSunAndMoon(Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
        {
            if (ExtenManag.IsLoaded != true) return true;

            ExtenSurfaceBackground back = GetExtenBack(Main.bgStyle);
            if (back == null) return true;

            Texture2D value = TextureAssets.Sun.Value;
            int num = Main.moonType;
            if (!TextureAssets.Moon.IndexInRange(num))
            {
                num = Terraria.Utils.Clamp(num, 0, 8);
            }
            Texture2D value2 = TextureAssets.Moon[num].Value;
            int num2 = sceneArea.bgTopY;
            int num3 = (int)(Main.time / 54000.0 * (double)(sceneArea.totalWidth + (float)(value.Width * 2))) - value.Width;
            int num4 = 0;
            float num5 = 1f;
            float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
            int num6 = (int)(Main.time / 32400.0 * (double)(sceneArea.totalWidth + (float)(value2.Width * 2))) - value2.Width;
            int num7 = 0;
            float num8 = 1f;
            float num9 = (float)(Main.time / 32400.0) * 2f - 7.3f;
            if (Main.dayTime)
            {
                double num10 = Math.Pow(Math.Abs(Main.time / 54000.0 * 2.0 - 1.0), 2.0);
                num4 = (int)((double)num2 + num10 * 250.0 + 180.0);
                num5 = (float)(1.2 - num10 * 0.4);
            }
            else
            {
                double num11 = Math.Pow(Math.Abs(Main.time / 32400.0 * 2.0 - 1.0), 2.0);
                num7 = (int)((double)num2 + num11 * 250.0 + 180.0);
                num8 = (float)(1.2 - num11 * 0.4);
            }
            if (Main.starGame)
            {
                if (WorldGen.generatingWorld)
                {
                    Main.alreadyGrabbingSunOrMoon = true;
                    if (Main.rand.Next(60) == 0)
                    {
                        for (int i = 0; i < Main.numStars; i++)
                        {
                            if (Main.star[i].hidden)
                            {
                                Star.SpawnStars(i);
                            }
                        }
                    }
                    if (Main.dayTime)
                    {
                        Main.dayTime = false;
                        Main.time = 0.0;
                    }
                }
                else
                {
                    Main.starGame = false;
                }
            }
            else
            {
                Main.starsHit = 0;
            }
            if (Main.dayTime)
            {
                if ((Main.remixWorld && !Main.gameMenu) || WorldGen.remixWorldGen)
                {
                    return false;
                }
                num5 *= 1.1f;
                float num12 = 1f - tempMushroomInfluence;
                num12 -= Main.cloudAlpha * 1.5f * Main.atmo;
                if (num12 < 0f)
                {
                    num12 = 0f;
                }
                Color color = new Color((byte)(255f * num12), (byte)((float)(int)sunColor.G * num12), (byte)((float)(int)sunColor.B * num12), (byte)(255f * num12));
                Color color2 = new Color((byte)((float)(int)sunColor.R * num12), (byte)((float)(int)sunColor.G * num12), (byte)((float)(int)sunColor.B * num12), (byte)((float)(int)sunColor.B * num12));
                if (!Main.eclipse)
                {
                    color = Color.White;
                }
                bool flag = false;
                bool flag2 = false;
                if (Main.eclipse)
                {
                    value = TextureAssets.Sun3.Value;
                    flag = true;
                }
                else if (!Main.gameMenu && Main.player[Main.myPlayer].head == 12)
                {
                    value = TextureAssets.Sun2.Value;
                    flag2 = true;
                    flag = true;
                }
                if (flag)
                {
                    color2 = new Color((byte)((float)(int)sunColor.R * num12), (byte)((float)(int)sunColor.G * num12), (byte)((float)(int)sunColor.B * num12), (byte)((float)(sunColor.B - 60) * num12));
                }
                Vector2 origin = value.Size() / 2f;
                Vector2 vector = new Vector2(num3, num4 + Main.sunModY);

                if (back.DrawSunPrefix(Main.spriteBatch, ref vector, ref rotation, ref num5, ref color))
                {
                    Texture2D sunimg = back.Sun?.Value;
                    if (sunimg == null)
                    {
                        Main.spriteBatch.Draw(value, vector, null, color, rotation, origin, num5, SpriteEffects.None, 0f);
                        Main.spriteBatch.Draw(value, vector, null, color2, rotation, origin, num5, SpriteEffects.None, 0f);
                        Main.instance.HorizonRenderer.DrawSun(vector);
                        if (flag2)
                        {
                            Texture2D value3 = TextureAssets.Extra[280].Value;
                            Color color3 = new Color(color.R, color.G, color.B, 100);
                            Main.spriteBatch.Draw(value3, vector, null, color3, rotation, origin, num5, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        Main.spriteBatch.Draw(sunimg, vector, null, color, rotation, origin, num5, SpriteEffects.None, 0f);
                    }
                }
                back.DrawSunPostfix(Main.spriteBatch, vector, rotation, num5, color);
            }
            if (!Main.dayTime)
            {
                float num13 = 1f - Main.cloudAlpha * 1.5f * Main.atmo;
                if (num13 < 0f)
                {
                    num13 = 0f;
                }
                moonColor *= num13;
                Vector2 position = new Vector2(num6, num7 + Main.moonModY);

                if (back.DrawMoonPrefix(Main.spriteBatch, ref position, ref num9, ref num8, ref moonColor))
                {
                    Texture2D moonimg = back.Moon?.Value;
                    if (moonimg == null)
                    {
                        if (WorldGen.drunkWorldGen)
                        {
                            Main.spriteBatch.Draw(TextureAssets.SmileyMoon.Value, position, new Rectangle(0, 0, TextureAssets.SmileyMoon.Width(), TextureAssets.SmileyMoon.Height()), moonColor, num9 / 2f + (float)Math.PI, new Vector2(TextureAssets.SmileyMoon.Width() / 2, TextureAssets.SmileyMoon.Height() / 2), num8, SpriteEffects.None, 0f);
                        }
                        else if (Main.pumpkinMoon)
                        {
                            Main.spriteBatch.Draw(TextureAssets.PumpkinMoon.Value, position, new Rectangle(0, TextureAssets.PumpkinMoon.Width() * Main.moonPhase, TextureAssets.PumpkinMoon.Width(), TextureAssets.PumpkinMoon.Width()), moonColor, num9, new Vector2(TextureAssets.PumpkinMoon.Width() / 2, TextureAssets.PumpkinMoon.Width() / 2), num8, SpriteEffects.None, 0f);
                        }
                        else if (Main.snowMoon)
                        {
                            Main.spriteBatch.Draw(TextureAssets.SnowMoon.Value, position, new Rectangle(0, TextureAssets.SnowMoon.Width() * Main.moonPhase, TextureAssets.SnowMoon.Width(), TextureAssets.SnowMoon.Width()), moonColor, num9, new Vector2(TextureAssets.SnowMoon.Width() / 2, TextureAssets.SnowMoon.Width() / 2), num8, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            Main.spriteBatch.Draw(TextureAssets.Moon[num].Value, position, new Rectangle(0, TextureAssets.Moon[num].Width() * Main.moonPhase, TextureAssets.Moon[num].Width(), TextureAssets.Moon[num].Width()), moonColor, num9, new Vector2(TextureAssets.Moon[num].Width() / 2, TextureAssets.Moon[num].Width() / 2), num8, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        Main.spriteBatch.Draw(moonimg, position, null, moonColor, num9, moonimg.Size() / 2f, num8, SpriteEffects.None, 0f);
                    }
                }
                back.DrawMoonPostfix(Main.spriteBatch, position, num9, num8, moonColor);
            }
            Rectangle value4 = (Main.dayTime ? new Rectangle((int)((double)num3 - (double)TextureAssets.Sun.Width() * 0.5 * (double)num5), (int)((double)num4 - (double)TextureAssets.Sun.Height() * 0.5 * (double)num5 + (double)Main.sunModY), (int)((float)TextureAssets.Sun.Width() * num5), (int)((float)TextureAssets.Sun.Width() * num5)) : new Rectangle((int)((double)num6 - (double)TextureAssets.Moon[num].Width() * 0.5 * (double)num8), (int)((double)num7 - (double)TextureAssets.Moon[num].Width() * 0.5 * (double)num8 + (double)Main.moonModY), (int)((float)TextureAssets.Moon[num].Width() * num8), (int)((float)TextureAssets.Moon[num].Width() * num8)));
            Rectangle rectangle = new Rectangle(Main.mouseX, Main.mouseY, 1, 1);
            Main.sunModY = (short)((double)Main.sunModY * 0.999);
            Main.moonModY = (short)((double)Main.moonModY * 0.999);
            if (Main.gameMenu && Main.netMode != 1)
            {
                if (rectangle.Intersects(value4) || Main.alreadyGrabbingSunOrMoon)
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                }
                if (Main.mouseLeft || Main.starGame)
                {
                    if (rectangle.Intersects(value4) || Main.alreadyGrabbingSunOrMoon)
                    {
                        if (Main.dayTime)
                        {
                            Main.time = 54000.0 * (double)((float)(Main.mouseX + TextureAssets.Sun.Width()) / ((float)Main.screenWidth + (float)(TextureAssets.Sun.Width() * 2)));
                            Main.sunModY = (short)(Main.mouseY - num4);
                            if (Main.time > 53990.0)
                            {
                                Main.time = 53990.0;
                            }
                        }
                        else
                        {
                            Main.time = 32400.0 * (double)((float)(Main.mouseX + TextureAssets.Moon[num].Width()) / ((float)Main.screenWidth + (float)(TextureAssets.Moon[num].Width() * 2)));
                            Main.moonModY = (short)(Main.mouseY - num7);
                            if (Main.time > 32390.0)
                            {
                                Main.time = 32390.0;
                            }
                        }
                        if (Main.time < 10.0)
                        {
                            Main.time = 10.0;
                        }
                        Main.alreadyGrabbingSunOrMoon = true;
                        AchievementsHelper.DoClassicTitleScreenAchievement();
                    }
                }
                else
                {
                    Main.alreadyGrabbingSunOrMoon = false;
                }
            }
            Main.LastCelestialBodyPosition = (Main.dayTime ? new Vector2(num3, num4 + Main.sunModY) : new Vector2(num6, num7 + Main.moonModY));
            Main.LastCelestialBodyPosition /= Main.ScreenSize.ToVector2();

            return false;
        }
    }
}
