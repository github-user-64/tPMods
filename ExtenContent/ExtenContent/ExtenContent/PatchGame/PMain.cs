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

        public override bool DrawProjDirectPrefix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer;
            if (player == null && Main.player.IndexInRange(proj.owner) == true) player = Main.player[proj.owner];

            Color projectileColor = Lighting.GetColor((int)(proj.position.X + proj.width * 0.5) / 16, (int)((proj.position.Y + proj.height * 0.5) / 16.0));

            Main.instance.PrepareDrawnProjectileDrawing(proj);//准备绘制射弹?

            return ProjectileLoad.DrawProjDirectPrefix(proj, projectileColor, player);
        }

        public override void DrawProjDirectPostfix(Projectile proj, Player overridePlayer = null)
        {
            Player player = overridePlayer;
            if (player == null && Main.player.IndexInRange(proj.owner) == true) player = Main.player[proj.owner];

            Color projectileColor = Lighting.GetColor((int)(proj.position.X + proj.width * 0.5) / 16, (int)((proj.position.Y + proj.height * 0.5) / 16.0));

            ProjectileLoad.DrawProjDirectPostfix(proj, projectileColor, player);
        }
    }
}
