using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace PlayerAccount.PatchGame
{
    /// <summary>
    /// 修补<see cref="Player"/>
    /// </summary>
    [HarmonyPatch(typeof(Player))]
    public class PatchPlayer
    {
        /// <summary>
        /// 是否可以掉落墓碑
        /// </summary>
        public static List<Func<Player, bool>> OnCanDropTombstone { get; } = new List<Func<Player, bool>>();

        [HarmonyPatch("DropTombstone")]
        [HarmonyPrefix]
        internal static bool CanDropTombstone(Player __instance, long coinsOwned, NetworkText deathText, int hitDirection)
        {
            return Utils.Utils.A1(OnCanDropTombstone, __instance);
        }
    }
}
