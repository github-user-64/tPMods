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

            toolTipLine[numLines] = $"卸载物品";
            numLines++;
        }
    }
}
