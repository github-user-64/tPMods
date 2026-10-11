using HarmonyLib;
using System.Collections.Generic;
using Terraria.Initializers;

namespace ExtenContentPatch.PatchGame.Initializers
{
    [HarmonyPatch(typeof(WingStatsInitializer))]
    internal static class PWingStatsInitializer
    {
        [HarmonyPatch(nameof(WingStatsInitializer.Load))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_Load(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.WingToEquipLoader();
        }
    }
}
