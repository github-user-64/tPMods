using HarmonyLib;
using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame
{
    [HarmonyPatch(typeof(Main))]
    internal partial class PMain : PatchMain
    {
        public override void UpdatePrefix(GameTime gameTime)
        {
            ThisMod.Api.PatchMain.UpdatePrefix(gameTime);
        }

        public override void UpdatePostfix(GameTime gameTime)
        {
            ThisMod.Api.PatchMain.UpdatePostfix(gameTime);
        }

        public override void DoUpdateInWorldPrefix()
        {
            ThisMod.Api.PatchMain.DoUpdateInWorldPrefix();
        }

        public override void DoUpdateInWorldPostfix()
        {
            ThisMod.Api.PatchMain.DoUpdateInWorldPostfix();
        }

        public override void MouseText_DrawItemTooltip_GetLinesInfoPostfix(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            if (numLines < toolTipLine.Length == false) return;

            ThisMod.Api.Item.MouseText_DrawItemTooltip_GetLinesInfoPostfix(item, ref yoyoLogo, ref oldKB, ref numLines, ref toolTipLine, ref lineColors);
        }

        public override bool DrawProjDirectPrefix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer;
            if (player == null && Main.player.IndexInRange(proj.owner) == true) player = Main.player[proj.owner];

            Color projectileColor = Lighting.GetColor((int)(proj.position.X + proj.width * 0.5) / 16, (int)((proj.position.Y + proj.height * 0.5) / 16.0));

            Main.instance.PrepareDrawnProjectileDrawing(proj);//准备绘制射弹?

            return ThisMod.Api.Projectile.DrawProjDirectPrefix(proj, projectileColor, player);
        }

        public override void DrawProjDirectPostfix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer;
            if (player == null && Main.player.IndexInRange(proj.owner) == true) player = Main.player[proj.owner];

            Color projectileColor = Lighting.GetColor((int)(proj.position.X + proj.width * 0.5) / 16, (int)((proj.position.Y + proj.height * 0.5) / 16.0));

            ThisMod.Api.Projectile.DrawProjDirectPostfix(proj, projectileColor, player);
        }

        public override void DrawMenuPostfix(GameTime gameTime)
        {
            ThisMod.Api.MenuStyle.DrawMenuPostfix(gameTime);
        }

        [HarmonyPatch(nameof(Main.GetPreferredBGStyleForPlayer))]
        [HarmonyPostfix]
        private static void GetPreferredBGStyleForPlayerPostfix(ref int __result)
        {
            ThisMod.Api.PatchMain.GetPreferredBGStyleForPlayerPostfix(ref __result);
        }

        [HarmonyPatch("UpdateBGVisibility_BackLayer")]
        [HarmonyPrefix]
        private static bool UpdateBGVisibility_BackLayerPrefix(int? targetBiomeOverride, float? transitionAmountOverride)
        {
            return ThisMod.Api.Back.UpdateBGVisibility_BackLayerPrefix(targetBiomeOverride, transitionAmountOverride);
        }

        [HarmonyPatch("DrawSurfaceBG")]
        [HarmonyPostfix]
        private static void DrawSurfaceBGPostfix(SpriteBatchBeginner parentSpriteBatchBeginner, float? verticalParallaxOverride)
        {
            ThisMod.Api.Back.DrawSurfaceBGPostfix(parentSpriteBatchBeginner, verticalParallaxOverride);
        }

        [HarmonyPatch("DrawBG")]
        [HarmonyPrefix]
        private static bool DrawBGPrefix(SpriteBatchBeginner parentSpriteBatchBeginner)
        {
            return ThisMod.Api.MenuStyle.DrawBGPrefix(parentSpriteBatchBeginner);
        }

        [HarmonyPatch("UpdateAudio_DecideOnNewMusic")]
        [HarmonyPrefix]
        private static bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            //string s = string.Empty;

            //for (int i = 0; i < Main.musicFade.Length; ++i)
            //{
            //    float v = Main.musicFade[i];

            //    if (v > 0) s += $"[{i}:{v}],";
            //}

            //Debug.WriteLine(s);

            bool v = ThisMod.Api.MenuStyle.UpdateAudio_DecideOnNewMusicPrefix();
            if (v) v &= ThisMod.Api.PatchMain.UpdateAudio_DecideOnNewMusicPrefix();
            return v;
        }

        [HarmonyPatch("DrawSunAndMoon")]
        [HarmonyPrefix]
        private static bool DrawSunAndMoon(Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
        {
            return ThisMod.Api.MenuStyle.DrawSunAndMoon(sceneArea, moonColor, sunColor, tempMushroomInfluence);
        }
    }
}
