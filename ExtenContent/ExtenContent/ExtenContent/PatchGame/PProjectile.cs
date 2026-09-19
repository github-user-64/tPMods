using HarmonyLib;
using Microsoft.Xna.Framework;
using Terraria;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Projectile))]
    internal static class PProjectile
    {
        [HarmonyPatch("Colliding")]
        [HarmonyPostfix]
        private static void Colliding(ref bool __result, Projectile __instance, Rectangle myRect, Rectangle targetRect)
        {
            bool result = __result;

            __result = result;
        }
    }
}
