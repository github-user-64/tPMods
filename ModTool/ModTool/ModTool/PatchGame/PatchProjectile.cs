using HarmonyLib;
using Terraria;
using Terraria.DataStructures;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Projectile"/>
    /// </summary>
    [HarmonyPatch(typeof(Projectile))]
    public class PatchProjectile : tContentPatch.PatchProjectile
    {
        /// <summary/>
        public delegate void NewProjectileEvent(int result, IEntitySource spawnSource,
            float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2);
        /// <summary>在创建射弹后</summary>
        public static event NewProjectileEvent OnNewProjectilePos = null;


        /// <inheritdoc/>
        public override void NewProjectilePostfix(int result, IEntitySource spawnSource,
            float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2)
        {
            OnNewProjectilePos?.Invoke(result, spawnSource,
                X, Y, SpeedX, SpeedY, Type, Damage, KnockBack, Owner, ai0, ai1, ai2);
        }

        /// <summary/>
        public delegate void SetDefaultsEvent(Projectile This, int Type);
        /// <summary>在设置默认前</summary>
        public static event SetDefaultsEvent OnSetDefaults = null;

        [HarmonyPatch("SetDefaults")]
        [HarmonyPrefix]
        internal static void SetDefaultsPrefix(Projectile __instance, int Type)
        {
            OnSetDefaults?.Invoke(__instance, Type);
        }
    }
}
