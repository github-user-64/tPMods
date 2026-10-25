using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using static ExtenContent.Utils.ReflectionUtils;

namespace ExtenContent.Extens
{
    public static partial class MenuStyleLoader
    {
        internal static bool DrawBGPrefix(SpriteBatchBeginner parentSpriteBatchBeginner)
        {
            if (Main.gameMenu != true) return true;
            if (ExtenManag.IsLoaded != true) return true;

            ExtenMenuStyle style = CurrentStyle;
            if (style == null) return true;

            int back = style.Back;
            if (back < SurfaceBackgroundID.Forest1 || SurfaceBackgroundLoader.BackCount <= back) return true;

            MainR.scAdj = MainR.ComputeScAdj(Main.screenPosition.Y, Main.screenHeight);
            if (Main.gameMenu)
            {
                MainR.scAdj = 0f;
            }

            if (Main.resetClouds)
            {
                Cloud.resetClouds();
                Main.resetClouds = false;
            }

            MainR.bgScale = 1f;
            MainR.bgWidthScaled = (int)(Main.backgroundWidth[Main.treeMntBGSet1[0]] * MainR.bgScale);
            Main.ColorOfSurfaceBackgroundsModified = Main.ColorOfTheSkies;
            Main.ColorOfSurfaceBackgroundsBase = Main.ColorOfTheSkies;
            int preferredBGStyleForPlayer = Main.GetPreferredBGStyleForPlayer();
            MainR.backgroundLayerTransitionSpeed = 0.05f;

            if (Main.bgDelay < 0)
            {
                Main.bgDelay++;
            }
            else if (preferredBGStyleForPlayer != Main.bgStyle)
            {
                MainR.DrawBG_HandleBackgroundTransition(preferredBGStyleForPlayer);
            }
            else if (Main.bgDelay > 0)
            {
                Main.bgDelay--;
            }

            if (Main.gameMenu)
            {
                Main.bgStyle = back;
            }

            if (Main.instantBGTransitionCounter > 0)
            {
                Main.instantBGTransitionCounter--;
                Main.bgStyle = preferredBGStyleForPlayer;
                MainR.backgroundLayerTransitionSpeed = 1f;
            }

            MainR.UpdateBGVisibility_BackLayer(null, null);
            MainR.UpdateBGVisibility_FrontLayer(null, null);

            try
            {
                MainR.DrawSurfaceBG(parentSpriteBatchBeginner);
                if (Main.BackgroundEnabled)
                {
                    MainR.DrawUnderworldBackground(Main.GameZoomTarget);
                }
            }
            catch (Exception e)
            {
                if (!Main.ignoreErrors)
                {
                    throw;
                }
                TimeLogger.DrawException(e);
            }

            return false;
        }

        internal static void DrawMenuPostfix()
        {
            CurrentStyle?.Update();
        }

        internal static bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            if (Main.gameMenu != true) return true;
            if (ExtenManag.IsLoaded != true) return true;

            ExtenMenuStyle style = CurrentStyle;
            if (style == null) return true;

            int music = style.Music;
            if (music < MusicID.None || MusicLoader.MusicCount <= music) return true;

            Main.newMusic = music;

            return false;
        }

        internal static void DrawLogo(bool playOldTile, bool boulderLogo, float logoRotation, float logoScale)
        {
            byte b = (byte)((255 + Main.tileColor.R * 2) / 3);
            Color color = new Color(b, b, b, 255);

            Vector2 pos = new Vector2(Main.screenWidth / 2, 100f);

            Vector2 origin = default;
            Rectangle rect = default;
            Set(TextureAssets.Logo, ref origin, ref rect);

            ExtenMenuStyle style = ExtenManag.IsLoaded ? CurrentStyle : null;

            if (style == null)
            {
                DrawLogo(playOldTile, boulderLogo, logoRotation, logoScale, rect, pos, origin, color);
                return;
            }

            if (style.DrawLogoPrefix(Main.spriteBatch, ref pos, ref logoRotation, ref logoScale, ref color))
            {
                Asset<Texture2D> logo = style.Logo;
                if (logo == null)
                {
                    DrawLogo(playOldTile, boulderLogo, logoRotation, logoScale, rect, pos, origin, color);
                }
                else
                {
                    Set(logo, ref origin, ref rect);
                    Main.spriteBatch.Draw(logo.Value, pos, rect, color, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                }
            }

            style.DrawLogoPostfix(Main.spriteBatch, pos, logoRotation, logoScale, color);
        }

        private static void Set(Asset<Texture2D> img, ref Vector2 origin, ref Rectangle rect)
        {
            int width = img.Width();
            int height = img.Height();
            rect = new Rectangle(0, 0, width, height);
            origin = new Vector2(width / 2, height / 2);
        }

        private static void DrawLogo(bool playOldTile, bool boulderLogo, float logoRotation, float logoScale,
            Rectangle rect, Vector2 pos, Vector2 origin, Color color)
        {
            Color color2 = new Color((byte)(color.R * (Main.LogoA / 255f)), (byte)(color.G * (Main.LogoA / 255f)), (byte)(color.B * (Main.LogoA / 255f)), (byte)(color.A * (Main.LogoA / 255f)));
            Color color3 = new Color((byte)(color.R * (Main.LogoB / 255f)), (byte)(color.G * (Main.LogoB / 255f)), (byte)(color.B * (Main.LogoB / 255f)), (byte)(color.A * (Main.LogoB / 255f)));

            if (playOldTile)
            {
                Main.spriteBatch.Draw(TextureAssets.Logo3.Value, pos, rect, color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo4.Value, pos, rect, color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }
            else if (boulderLogo)
            {
                Main.spriteBatch.Draw(TextureAssets.Logo5.Value, pos, rect, color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo6.Value, pos, rect, color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }
            else
            {
                Main.spriteBatch.Draw(TextureAssets.Logo.Value, pos, rect, color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo2.Value, pos, rect, color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }

            if (LogoExten == null) return;

            Vector2 off = -logoRotation.ToRotationVector2() * 193f;
            off -= (logoRotation + MathHelper.TwoPi / -4).ToRotationVector2() * 11f;
            Vector2 p = pos + off * logoScale;

            if ((p - Main.MouseScreen).Length() < (LogoExten.Width() + LogoExten.Height()) * logoScale / 2f / 2f)
            {
                if (Main.mouseLeftRelease)
                {
                    if (Main.mouseLeft)
                    {
                        SoundEngine.PlaySound(Main.rand.Next(0, 2) == 0 ? SoundID.Item57 : SoundID.Item58);
                    }
                    else
                    {
                        color = Color.White;
                    }
                }
            }

            Set(LogoExten, ref origin, ref rect);

            Main.spriteBatch.Draw(LogoExten.Value, p, rect, color, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
        }
    }
}
