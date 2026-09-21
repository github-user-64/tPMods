using ExtenContent.Extens;
using HarmonyLib;
using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Projectile))]
    internal class PProjectile : PatchProjectile
    {
        public override void AIPostfix(Projectile This)
        {
            ProjectileLoad.AIPostfix(This);
        }

        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            ProjectileLoad.SetDefaultsPostfix(This, Type);
        }

        public override void KillPostfix(Projectile This)
        {
            ProjectileLoad.KillPostfix(This);
        }

        [HarmonyPatch("Colliding")]
        [HarmonyPostfix]
        private static void CollidingPostfix(ref bool __result, Projectile __instance, Rectangle myRect, Rectangle targetRect)
        {
            ProjectileLoad.CollidingPostfix(ref __result, __instance, myRect, targetRect);
        }
    }
}
