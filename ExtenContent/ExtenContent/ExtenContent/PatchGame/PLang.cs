using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Terraria;
using Terraria.ID;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Lang))]
    internal static class PLang
    {
        [HarmonyPatch(nameof(Lang.GetItemName))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> TranspilerDoDraw(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchStartForward(
               new CodeMatch(OpCodes.Ldsfld, typeof(ItemID).GetField(nameof(ItemID.Count)))
               )
               .ThrowIfInvalid("找不到IL位置")
               .Advance(0)
               .RemoveInstructions(1)
               .InsertAndAdvance(
               new CodeMatch(OpCodes.Ldsfld, typeof(Lang).GetField("_itemNameCache", BindingFlags.NonPublic | BindingFlags.Static)),
               new CodeInstruction(OpCodes.Ldlen)
               );

            return codeMatcher.Instructions();
        }
    }
}
