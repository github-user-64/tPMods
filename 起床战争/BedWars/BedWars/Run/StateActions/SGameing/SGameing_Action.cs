using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing : IStateAction
    {
        private int[] _rottenEggNPC = new int[]
        {
            NPCID.BigCrimslime,
            NPCID.Pinky,
            NPCID.Slimer,
            NPCID.IlluminantSlime,
            NPCID.IceSlime,
            NPCID.SpikedJungleSlime,
            NPCID.SlimeMasked,
            NPCID.SandSlime,
            NPCID.GoldenSlime,
            NPCID.ShimmerSlime,
            NPCID.YellowSlime,
            NPCID.RedSlime,
            NPCID.BlueSlime,
        };
        private int getRandNPCid()
        {
            int index = ModTool.Utils.Utils.GetRand(0, _rottenEggNPC.Length);

            return _rottenEggNPC[index];
        }

        public override void OnProjectileKill(Projectile proj)
        {
            if (proj.type != ProjectileID.RottenEgg) return;

            GameTeamData team = game.Team.GetData(proj.owner, null);
            if (team == null) return;

            int id = getRandNPCid();//随机npc

            //获取在范围内的玩家
            int Target = Main.maxPlayers;
            foreach (Player i in Main.player)
            {
                GameTeamData t = game.Team.GetData(i.whoAmI, null);
                if (t == null) continue;

                float dis = i.Center.Distance(proj.Center);
                if (dis > 16 * 30) continue;

                Target = i.whoAmI;
                break;
            }

            int index = NPC.NewNPC(null, (int)proj.Center.X, (int)proj.Center.Y, id, Target: Target);
            if (index >= Main.maxNPCs) return;

            Main.npc[index].life -= 1;
        }

        private void UpdateAction()
        {
            foreach (Projectile i in Main.projectile)
            {
                if (i?.active != true) continue;
                if (i.timeLeft < 1) continue;

                Player player = ModTool.ServerHelp.PlayDataUtils.GetPlay(i.owner);
                if (player == null) continue;

                GameTeamData team = game.GetPlayerTeam(player);
                if (team == null) continue;

                if (i.type == ProjectileID.StarAnise) UpdateAction_Place(i);
                else if (i.type == ProjectileID.TendonHook) UpdateAction_TendonHook(i, player);
            }
        }

        private void UpdateAction_Place(Projectile proj)
        {
            Vector2 p = Vector2.Normalize(proj.velocity) * -24;//在射弹后面
            p += proj.Center;

            Point pos = p.ToTileCoordinates();
            Point posMap = pos;
            posMap.X -= game.DataInfo.X;
            posMap.Y -= game.DataInfo.Y;
            if (game.Data.InMapRelative(posMap) == false) return;//不在地图里

            TileData data = game.DataTile[posMap.Y][posMap.X];
            if (data.CanActionTile == false) return;

            WorldGen.PlaceTile(pos.X, pos.Y, TileID.Cloud);
            NetMessage.SendTileSquare(-1, pos.X, pos.Y, 1);
        }

        private void UpdateAction_TendonHook(Projectile proj, Player player)
        {
            Vector2 v = Vector2.Normalize(proj.velocity) * 16 * 1;

            //proj.Kill();
            proj.type = ProjectileID.None;
            NetMessage.TrySendData(MessageID.SyncProjectile, number: proj.whoAmI);

            //

            //消耗沟子
            if (UpdateAction_TendonHook_DelTendonHook(player, player.miscEquips, PlayerItemSlotID.Misc0) == false &&
                UpdateAction_TendonHook_DelTendonHook(player, player.inventory, PlayerItemSlotID.Inventory0) == false)
            {
                return;
            }
            
            //

            player.velocity = v;
            NetMessage.TrySendData(MessageID.PlayerControls, number: player.whoAmI);
        }

        private bool UpdateAction_TendonHook_DelTendonHook(Player player, Item[] arr, int solt)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                Item item = arr[i];
                if (item == null) continue;
                if (item.type != ItemID.TendonHook) continue;
                if (item.stack < 1) continue;

                item.SetDefaults(ItemID.None);

                NetMessage.TrySendData(MessageID.SyncEquipment, -1, -1, null,
                    player.whoAmI, solt + i, item.prefix);

                return true;
            }

            return false;
        }
    }
}
