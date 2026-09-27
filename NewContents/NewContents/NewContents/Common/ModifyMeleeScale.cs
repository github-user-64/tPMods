using ExtenContent.Extens;
using tContentPatch;
using Terraria;
using Terraria.DataStructures;

namespace NewContents.Common
{
    internal class ModifyMeleeScale : PatchMain
    {
        public static float Val = 0;

        public override void DoUpdateInWorldPrefix()
        {
            Val = 0;
        }

        private class PPlay : EPatchPlayer
        {
            public override void GetAdjustedItemScalePostfix(ref float result, Player player, Item item)
            {
                if (item.melee != true) return;

                result += Val;
            }
        }

        private class PProj : PatchProjectile
        {
            public override void NewProjectilePostfix(int result, IEntitySource spawnSource, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2, NewProjectileModifier modifer)
            {
                EntitySource_ItemUse s = spawnSource as EntitySource_ItemUse;
                if (s == null) return;
                if (s.Item.melee != true) return;

                if (Main.projectile.IndexInRange(result))
                {
                    Main.projectile[result].scale += Val;
                }
            }
        }
    }
}
