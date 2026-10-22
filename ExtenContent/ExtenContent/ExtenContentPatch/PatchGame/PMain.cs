using HarmonyLib;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using tContentPatch;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace ExtenContentPatch.PatchGame
{
    [HarmonyPatch(typeof(Main))]
    internal partial class PMain : PatchMain
    {
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

        //[HarmonyPatch(nameof(Main.GetPreferredBGStyleForPlayer))]
        //[HarmonyPostfix]
        //private static void GetPreferredBGStyleForPlayerPostfix(ref int __result)
        //{

        //}

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

        public static List<int> soundSlot = new List<int>();
        public static int MusicCount { get; set; }
        [HarmonyPatch("UpdateAudio_DecideOnNewMusic")]
        [HarmonyPrefix]
        private static bool UpdateAudio_DecideOnNewMusic()
        {
            return true;
        }

        public static void ASD()
        {
            return;

            LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
            if (legacyAudioSystem == null) return;

            int MusicCount = MusicID.Count;
            soundSlot.Clear();
            soundSlot.Add(MusicCount++);

            Array.Resize(ref legacyAudioSystem.AudioTracks, MusicCount);
            Array.Resize(ref legacyAudioSystem.TrackLoopCounts, MusicCount);
            Array.Resize(ref legacyAudioSystem.PlayCallbacks, MusicCount);
            Array.Resize(ref Main.musicFade, MusicCount);
            Array.Resize(ref Main.musicNoCrossFade, MusicCount);

            FileStream read = File.OpenRead("F:/F_MY1/快速存储(记得清理)/sound_long.mp3");
            foreach (int slot in soundSlot)
            {
                if (Main.audioSystem is DisabledAudioSystem) return;
                legacyAudioSystem.AudioTracks[slot] = new MP3AudioTrack(read);
            }

            Main.audioSystem = legacyAudioSystem;

            PMain.MusicCount = MusicCount;
        }
    }
}
