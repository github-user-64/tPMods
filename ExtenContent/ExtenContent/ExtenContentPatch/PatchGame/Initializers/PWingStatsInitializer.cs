using ExtenContent.Extens;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Terraria.ID;
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
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchStartForward(
               new CodeMatch(OpCodes.Ldsfld, typeof(ArmorIDs.Wing).GetField(nameof(ArmorIDs.Wing.Count)))
               )
               .ThrowIfInvalid("找不到IL位置")
               .Advance(0)
               .RemoveInstructions(1)
               .InsertAndAdvance(
               new CodeMatch(OpCodes.Call, typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.WingCount)}"))
               );

            return codeMatcher.Instructions();
        }
    }
}
