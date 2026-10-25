using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
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

            Color color2 = new Color((byte)(color.R * (Main.LogoA / 255f)), (byte)(color.G * (Main.LogoA / 255f)), (byte)(color.B * (Main.LogoA / 255f)), (byte)(color.A * (Main.LogoA / 255f)));
            Color color3 = new Color((byte)(color.R * (Main.LogoB / 255f)), (byte)(color.G * (Main.LogoB / 255f)), (byte)(color.B * (Main.LogoB / 255f)), (byte)(color.A * (Main.LogoB / 255f)));

            Vector2 pos = new Vector2(Main.screenWidth / 2, 100f);
            Vector2 origin = new Vector2(TextureAssets.Logo.Width() / 2, TextureAssets.Logo.Height() / 2);

            if (playOldTile)
            {
                Main.spriteBatch.Draw(TextureAssets.Logo3.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo4.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }
            else if (boulderLogo)
            {
                Main.spriteBatch.Draw(TextureAssets.Logo5.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo6.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }
            else
            {
                Main.spriteBatch.Draw(TextureAssets.Logo.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color2, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.Logo2.Value, pos, new Rectangle(0, 0, TextureAssets.Logo.Width(), TextureAssets.Logo.Height()), color3, logoRotation, origin, logoScale, SpriteEffects.None, 0f);
            }
        }
    }
}
