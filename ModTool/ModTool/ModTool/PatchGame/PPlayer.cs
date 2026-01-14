using HarmonyLib;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Player"/>
    /// </summary>
    public class PPlayer
    {
        /// <summary>
        /// 能否掉落墓碑
        /// </summary>
        public static event DropTombstoneEvent OnCanDropTombstone
        {
            add
            {
                if (value == null) return;
                onCanDropTombstone.Add(value);
            }
            remove => onCanDropTombstone.Remove(value);
        }
        /// <summary/>
        public delegate bool DropTombstoneEvent(Player This, long coinsOwned, NetworkText deathText, int hitDirection);
        private readonly static List<DropTombstoneEvent> onCanDropTombstone = new List<DropTombstoneEvent>();

        [HarmonyPatch(typeof(Player), "DropTombstone")]
        private static class PatchDropTombstone
        {
            internal static bool Prefix(Player __instance, long coinsOwned, NetworkText deathText, int hitDirection)
            {
                bool ok = true;
                onCanDropTombstone.ForEach(i => ok &= i(__instance, coinsOwned, deathText, hitDirection));

                return ok;
            }
        }
    }
}
