using ExtenContent.Extens;
using tContentPatch;
using Terraria;
using Terraria.DataStructures;

namespace NewContents.Common
{
    internal class ModifyMeleeScale : PatchMain
    {
        protected static float[] Vals = new float[Main.player.Length];

        public override void DoUpdateInWorldPrefix()
        {
            for (int i = 0; i < Vals.Length; ++i) Vals[i] = 0;
        }

        public static void Add(Player player, float val)
        {
            Vals[player.whoAmI] += val;
        }

        private class PPlay : EPatchPlayer
        {
            public override void GetAdjustedItemScalePostfix(ref float result, Player player, Item item)
            {
                if (item.melee != true) return;

                result += Vals[player.whoAmI];
            }
        }

        private class PProj : PatchProjectile
        {
            public override void NewProjectilePostfix(int result, IEntitySource spawnSource, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2, NewProjectileModifier modifer)
            {
                EntitySource_ItemUse s = spawnSource as EntitySource_ItemUse;
                if (s == null) return;
                if (s.Item.melee != true) return;

                if (Main.projectile.IndexInRange(result) != true) return;
                Projectile proj = Main.projectile[result];
                if (Vals.IndexInRange(proj.owner) != true) return;

                proj.scale += Vals[proj.owner];
            }
        }
    }
}
