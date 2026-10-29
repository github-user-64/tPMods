using ExtenContent.Exten;
using HarmonyLib;
using System.Collections.Generic;
using tContentPatch;
using Terraria;

namespace ExtenContentPatch.PatchGame
{
    [HarmonyPatch(typeof(NPC))]
    internal class PNPC : PatchNPC
    {
        public override void UpdateNPC_BuffSetFlagsPostfix(NPC This, bool lowerBuffTime)
        {
            ThisMod.Api.Buff.UpdateNPC_BuffSetFlagsPostfix(This, lowerBuffTime);
        }

        public override void SetDefaultsPrefix(NPC This, int Type, NPCSpawnParams spawnparams)
        {
            if (This.buffImmune.Length >= BuffLoader.BuffCount) return;

            This.buffImmune = new bool[BuffLoader.BuffCount];
        }

        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPostfix]
        private static void ConstructorPostfix(NPC __instance)
        {
            __instance.buffImmune = new bool[BuffLoader.BuffCount];
        }

        [HarmonyPatch(nameof(NPC.SetDefaults))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_SetDefaults(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BuffToLoader();
        }

        [HarmonyPatch(nameof(NPC.RequestBuffRemoval))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_RequestBuffRemoval(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.BuffToLoader();
        }
    }
}
