using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(PlayerDrawSet))]
    internal static class PPlayerDrawSet
    {
        [HarmonyPatch(nameof(PlayerDrawSet.BoringSetup))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_BoringSetup(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToLoader().BodyToLoader().LegsToLoader();
        }

        [HarmonyPatch("CreateCompositeData")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_CreateCompositeData(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToLoader();
        }
    }
}
