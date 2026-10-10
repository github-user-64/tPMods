using HarmonyLib;
using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame
{
    [HarmonyPatch(typeof(Projectile))]
    internal class PProjectile : PatchProjectile
    {
        public override void AIPostfix(Projectile This)
        {
            ThisMod.Api.Projectile.AIPostfix(This);
        }

        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            ThisMod.Api.Projectile.SetDefaultsPostfix(This, Type);
        }

        public override void KillPostfix(Projectile This)
        {
            ThisMod.Api.Projectile.KillPostfix(This);
        }

        public override void NewProjectilePostfix(int result, IEntitySource spawnSource, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2, NewProjectileModifier modifer)
        {
            if (Main.projectile.IndexInRange(result) == false) return;

            Projectile proj = Main.projectile[result];

            ThisMod.Api.Projectile.NewProjectilePostfix(proj, spawnSource);
        }

        [HarmonyPatch("Colliding")]
        [HarmonyPostfix]
        private static void CollidingPostfix(ref bool __result, Projectile __instance, Rectangle myRect, Rectangle targetRect)
        {
            ThisMod.Api.Projectile.CollidingPostfix(ref __result, __instance, myRect, targetRect);
        }

        [HarmonyPatch("GetAlpha")]
        [HarmonyPostfix]
        private static void GetAlphaPostfix(ref Color __result, Projectile __instance, Color newColor)
        {
            ThisMod.Api.Projectile.GetAlphaPostfix(ref __result, __instance, newColor);
        }

        [HarmonyPatch("UpdatePosition")]
        [HarmonyPrefix]
        private static bool UpdatePositionPrefix(Projectile __instance, Vector2 wetVelocity)
        {
            return ThisMod.Api.Projectile.UpdatePositionPrefix(__instance, wetVelocity);
        }
    }
}
