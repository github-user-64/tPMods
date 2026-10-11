using HarmonyLib;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContentPatch.PatchGame.DataStructures
{
    [HarmonyPatch(typeof(PlayerDrawLayers))]
    internal static class PPlayerDrawLayers
    {
        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_01_3_BackHead))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_01_3_BackHead(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_21_Head))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_21_Head(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_22_FaceAcc))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_22_FaceAcc(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.HeadToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_12_SkinComposite_BackArmShirt(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_15_SkinLongCoat))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_15_SkinLongCoat(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_17_Torso))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_17_Torso(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_17_TorsoComposite))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_17_TorsoComposite(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_28_ArmOverItem))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_28_ArmOverItem(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_28_ArmOverItemComposite))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_28_ArmOverItemComposite(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BodyToEquipLoader();
        }

        [HarmonyPatch(nameof(PlayerDrawLayers.DrawPlayer_13_Leggings))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawPlayer_13_Leggings(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.LegsToEquipLoader();
        }
    }
}
