using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions.GameAction
{
    /// <summary>
    /// 刷怪蛋
    /// </summary>
    public class EggSpawnNPC : IGameAction
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

            int id = getRandNPCid();//随机npc

            int index = NPC.NewNPC(null, (int)proj.Center.X, (int)proj.Center.Y, id);
            if (index >= Main.maxNPCs) return;

            Main.npc[index].life -= 1;
        }
    }
}
