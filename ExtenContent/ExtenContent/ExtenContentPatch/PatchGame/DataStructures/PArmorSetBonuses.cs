using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(ArmorSetBonuses))]
    internal static class PArmorSetBonuses
    {
        [HarmonyPatch(nameof(ArmorSetBonuses.BuildLookup))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_BuildLookup(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.ItemToItemLoader();
        }
    }
}
