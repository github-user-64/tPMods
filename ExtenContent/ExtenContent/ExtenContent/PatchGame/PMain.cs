using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria;

namespace ExtenContent.PatchGame
{
    internal class PMain : PatchMain
    {
        public override void MouseText_DrawItemTooltip_GetLinesInfoPostfix(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            if (numLines < toolTipLine.Length == false) return;

            ItemLoad.MouseText_DrawItemTooltip_GetLinesInfoPostfix(item, ref yoyoLogo, ref oldKB, ref numLines, ref toolTipLine, ref lineColors);
        }
    }
}
