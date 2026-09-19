using ExtenContent.Extens;
using HarmonyLib;
using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Main))]
    internal class PMain : PatchMain
    {
        public override void MouseText_DrawItemTooltip_GetLinesInfoPostfix(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            if (numLines < toolTipLine.Length == false) return;

            ItemLoad.MouseText_DrawItemTooltip_GetLinesInfoPostfix(item, ref yoyoLogo, ref oldKB, ref numLines, ref toolTipLine, ref lineColors);
        }

        [HarmonyPatch("DrawProjDirect")]
        [HarmonyPostfix]
        private static void DrawProjDirectPostfix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer ?? Main.player[proj.owner];
        }
    }
}
