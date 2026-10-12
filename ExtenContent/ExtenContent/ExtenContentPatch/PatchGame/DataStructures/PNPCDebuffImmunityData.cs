using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(NPCDebuffImmunityData))]
    internal static class PNPCDebuffImmunityData
    {
        [HarmonyPatch(nameof(NPCDebuffImmunityData.ApplyToNPC))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_ApplyToNPC(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BuffToLoader();
        }
    }
}
