using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions.GameAction
{
    /// <summary>
    /// 冲锋钩
    /// </summary>
    public class HookGo : IGameAction
    {
        public override void UpdateProjectile(Projectile proj, Player player)
        {
            if (proj.type != ProjectileID.Hook) return;

            if (proj.ai[0] != 0) return;//不是射出状态的钩子

            Vector2 v = Vector2.Normalize(proj.velocity) * 16 * 1;

            if (v.HasNaNs())
            {
                v = Vector2.Normalize(proj.Center - player.Center) * 16 * 1;
                if (v.HasNaNs()) return;
            }

            proj.type = ProjectileID.None;
            NetMessage.TrySendData(MessageID.SyncProjectile, number: proj.whoAmI);

            //

            //消耗沟子
            if (UpdateAction_Hook_DelGrapplingHook(player, player.miscEquips, PlayerItemSlotID.Misc0) == false &&
                UpdateAction_Hook_DelGrapplingHook(player, player.inventory, PlayerItemSlotID.Inventory0) == false)
            {
                return;
            }

            //

            player.velocity = v;
            NetMessage.TrySendData(MessageID.PlayerControls, number: player.whoAmI);
        }

        private bool UpdateAction_Hook_DelGrapplingHook(Player player, Item[] arr, int solt)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                Item item = arr[i];
                if (item == null) continue;
                if (item.type != ItemID.GrapplingHook) continue;
                if (item.stack < 1) continue;

                if (item.stack > 1) item.stack--;
                else item.SetDefaults(ItemID.None);

                NetMessage.TrySendData(MessageID.SyncEquipment, -1, -1, null,
                    player.whoAmI, solt + i, item.prefix);

                return true;
            }

            return false;
        }
    }
}
