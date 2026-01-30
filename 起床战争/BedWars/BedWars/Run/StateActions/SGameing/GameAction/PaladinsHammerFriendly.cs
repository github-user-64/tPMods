using Microsoft.Xna.Framework;
using ModTool.EntityTag;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions.GameAction
{
    /// <summary>
    /// 圣骑士锤雷
    /// </summary>
    public class PaladinsHammerFriendly : IGameAction
    {
        public PaladinsHammerFriendly()
        {
            ModTool.PatchGame.PProjectile.SetLightningColor += PProjectile_SetLightningColor;
        }

        private Color PProjectile_SetLightningColor(Projectile This, Color color)
        {
            if (This.type != ProjectileID.PaladinsHammerFriendly) return color;
            if (This.ai[0] != 1) return color;

            color.R = (byte)ModTool.Utils.Utils.GetRand((byte)200, (byte)255);

            if (ModTool.Utils.Utils.GetRand(0, 2) == 0)
            {
                color.G = (byte)ModTool.Utils.Utils.GetRand((byte)200, (byte)255);
                color.B = 0;
            }
            else
            {
                color.G = 0;
                color.B = (byte)ModTool.Utils.Utils.GetRand((byte)200, (byte)255);
            }

            return color;
        }

        public override void UpdateProjectile(Projectile proj, Player player)
        {
            if (proj.type != ProjectileID.PaladinsHammerFriendly) return;

            string tag = "可以生成雷";

            if (proj.ai[0] == 0)//飞出去的状态
            {
                Entitys.projectile.SetVal(proj, tag, null);
                return;
            }

            if (proj.ai[0] != 1) return;
            //飞回来的状态

            if (Entitys.projectile.DelTag(proj, tag) == false) return;
            //有标签被删除

            int style = ModTool.Utils.Utils.GetRand(0, 1145);

            Projectile.NewProjectile(null, proj.Center, Vector2.Zero, ProjectileID.StormLightning, proj.damage, 1, Main.myPlayer,
                ai1: 1, ai2: style);
        }
    }
}
