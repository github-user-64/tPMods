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

        public override bool DrawProjDirectPrefix(Projectile proj, Player overridePlayer = null)
        {
            ExtenProjectile ep = ExtenManag.GetExtenProjectile(proj.type);
            if (ep == null) return true;

            Main.instance.PrepareDrawnProjectileDrawing(proj);//准备绘制射弹?

            Player player = overridePlayer ?? Main.player[proj.owner];

            return ep.PreDraw(proj, player);
        }

        public override void DrawProjDirectPostfix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer ?? Main.player[proj.owner];

            ProjectileLoad.DrawProjDirectPostfix(proj, player);
        }
    }
}
