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

            ExtenItem EItem = ExtenManag.GetExtenItem(item.type);
            if (EItem is ExtenItemUnload != true) return;

            toolTipLine[numLines] = $"卸载物品{ExtenItemUnload.GetUnloadItemFullName(item)}";
            numLines++;
        }
    }
}
