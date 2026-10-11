using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(PlayerDrawHeadSet))]
    internal static class PPlayerDrawHeadSet
    {
        [HarmonyPatch(nameof(PlayerDrawHeadSet.BoringSetup))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_BoringSetup(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToEquipLoader();
        }
    }
}
