using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace NewContents.Content.Equips
{
    internal class EEquip1 : ExtenEquip
    {
        public override EquipType EquipType => EquipType.Wings;
        public override string Texture => "Wings_1";
        protected Vector2 oldVelocity = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            //飞行时间,速度,加速倍数,悬浮,悬浮水平速度,悬浮水平加速倍数
            ArmorIDs.Wing.Sets.Stats[Slot] = new WingStats(60 * 5, 16f, 8f, true, 16f * 2, 8f * 2);
        }

        public override void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
        {
            //ArmorIDs.Wing.Sets.Stats[Slot] = new WingStats(60 * 5, 16f, 8f, true, 16f * 2, 8f * 2);

            Effects(player);

            if (player.controlJump == false || player.controlDown == false) return;

            float num2 = 0.95f;
            float num5 = 0.15f;
            float num4 = 1f;
            float num3 = 4.5f;

            if (player.gravDir != 1f)
            {
                if (player.velocity.Y > 0f)
                {
                    player.velocity.Y -= num2;
                }
                else if (player.velocity.Y > (0f - Player.jumpSpeed) * num4)
                {
                    player.velocity.Y -= num5;
                }
                if (player.velocity.Y < (0f - Player.jumpSpeed) * num3)
                {
                    player.velocity.Y = (0f - Player.jumpSpeed) * num3;
                }
            }
            else
            {
                if (player.velocity.Y < 0f)
                {
                    player.velocity.Y += num2;
                }
                else if (player.velocity.Y < Player.jumpSpeed * num4)
                {
                    player.velocity.Y += num5;
                }
                if (player.velocity.Y > Player.jumpSpeed * num3)
                {
                    player.velocity.Y = Player.jumpSpeed * num3;
                }
            }
        }

        public override void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
        {
            Effects(player);
        }

        protected void Effects(Player player)
        {
            if (player != Main.LocalPlayer) return;
            if (player.mount.Active) return;

            Vector2 oldVelocity = this.oldVelocity;
            this.oldVelocity = player.velocity;

            if ((player.velocity.Length() > 16 && oldVelocity.Length() <= 16) != true) return;

            Vector2 vector = Vector2.Normalize(player.velocity);
            Vector2 pos = player.Center + vector * 100;
            vector *= -20f;
            ParticleOrchestraType type = (ParticleOrchestraType)76;

            for (int i = 0; i < 5; ++i)
            {
                ParticleOrchestrator.BroadcastOrRequestParticleSpawn(type, new ParticleOrchestraSettings
                {
                    PositionInWorld = pos,
                    MovementVector = vector,
                });
            }

            SoundEngine.PlaySound(SoundID.Item14, player.position);
        }
    }
}
