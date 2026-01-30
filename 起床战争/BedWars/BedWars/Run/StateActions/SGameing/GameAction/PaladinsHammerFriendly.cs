using Microsoft.Xna.Framework;
using ModTool.EntityTag;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace BedWars.Run.StateActions.GameAction
{
    /// <summary>
    /// 圣骑士锤雷
    /// </summary>
    public class PaladinsHammerFriendly : IGameAction
    {
        private byte GetByte(byte v1, byte v2)
        {
            return (byte)ModTool.Utils.Utils.GetRand(v1, v2);
        }

        private Color GetLightningColor()
        {
            Color color = Color.Black;
            color.R = GetByte(150, 255);
            color.G = GetByte(50, 255);
            color.B = GetByte(50, 255);

            if (ModTool.Utils.Utils.GetRand(0, 2) == 0)
            {
                color.G = GetByte(0, 150);
            }
            else
            {
                color.B = GetByte(0, 150);
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

            ParticleSpawn(proj);
        }

        private void ParticleSpawn(Projectile proj)
        {
            ParticleOrchestraType type = ParticleOrchestraType.StormLightning;
            int style = ModTool.Utils.Utils.GetRand(0, 1145);
            Color color = GetLightningColor();

            ParticleOrchestrator.BroadcastOrRequestParticleSpawn(type, new ParticleOrchestraSettings
            {
                PositionInWorld = proj.Center,
                UniqueInfoPiece = (int)color.PackedValue,
                MovementVector = new Vector2(style, 0f),
            });
        }
    }
}
