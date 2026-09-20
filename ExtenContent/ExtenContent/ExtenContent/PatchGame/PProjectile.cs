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
        [HarmonyPatch("Colliding")]
        [HarmonyPostfix]
        private static void CollidingPostfix(ref bool __result, Projectile __instance, Rectangle myRect, Rectangle targetRect)
        {
            ProjectileLoad.CollidingPostfix(ref __result, __instance, myRect, targetRect);
        }

        [HarmonyPatch("AI")]
        [HarmonyPostfix]
        private static void AIPostfix(Projectile __instance)
        {
            ProjectileLoad.AIPostfix(__instance);
        }

        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            ProjectileLoad.SetDefaultsPostfix(This, Type);
        }

        public override void KillPostfix(Projectile This)
        {
            ProjectileLoad.KillPostfix(This);
        }
    }
}
