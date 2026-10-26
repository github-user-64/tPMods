using System;
using Terraria;
using Terraria.DataStructures;
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
    }
}
