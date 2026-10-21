using HarmonyLib;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace ExtenContentPatch.PatchGame
{
    internal partial class PMain : PatchMain
    {
        [HarmonyPatch(nameof(Main.GetNurseHealCost))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_GetNurseHealCost(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BuffToLoader();
        }

        [HarmonyPatch(nameof(Main.NPCChatText_DoNurseHeal))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_NPCChatText_DoNurseHeal(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BuffToLoader();
        }

        [HarmonyPatch("UpdateAudio")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_UpdateAudio(IEnumerable<CodeInstruction> instructions)
        {
            //return instructions.StaticFieldToMethod(typeof(Main).GetField("maxMusic"), typeof(PMain).GetMethod("get_MusicCount"));
            return instructions;
        }
    }
}
