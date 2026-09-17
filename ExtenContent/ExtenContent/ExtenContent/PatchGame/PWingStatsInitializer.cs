using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Terraria.ID;
using Terraria.Initializers;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(WingStatsInitializer))]
    internal static class PWingStatsInitializer
    {
        [HarmonyPatch(nameof(WingStatsInitializer.Load))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> TranspilerLoad(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchStartForward(
               new CodeMatch(OpCodes.Ldsfld, typeof(ArmorIDs.Wing).GetField(nameof(ArmorIDs.Wing.Count)))
               )
               .ThrowIfInvalid("找不到IL位置")
               .Advance(0)
               .RemoveInstructions(1)
               .InsertAndAdvance(
               new CodeMatch(OpCodes.Ldsfld, typeof(ArmorIDs.Wing.Sets).GetField(nameof(ArmorIDs.Wing.Sets.AlwaysAnimated))),
               new CodeInstruction(OpCodes.Ldlen)
               );

            return codeMatcher.Instructions();
        }
    }
}
