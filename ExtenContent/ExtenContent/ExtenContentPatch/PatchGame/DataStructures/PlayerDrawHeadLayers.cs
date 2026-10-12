using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(PlayerDrawHeadLayers))]
    internal static class PPlayerDrawHeadLayers
    {
        [HarmonyPatch(nameof(PlayerDrawHeadLayers.DrawPlayer_00_BackHelmet))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_00_BackHelmet(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawHeadLayers.DrawPlayer_02_DrawArmorWithFullHair))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_02_DrawArmorWithFullHair(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawHeadLayers.DrawPlayer_08_FaceAcc))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_08_FaceAcc(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToLoader();
        }
    }
}
