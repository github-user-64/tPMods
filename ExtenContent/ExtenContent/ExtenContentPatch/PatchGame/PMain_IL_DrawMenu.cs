using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using tContentPatch;
using Terraria;
using Terraria.GameContent;

namespace ExtenContentPatch.PatchGame
{
    internal partial class PMain : PatchMain
    {
        //[HarmonyDebug]
        [HarmonyPatch("DrawMenu")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler_DrawMenu(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchStartForward(
                new CodeMatch(OpCodes.Ldarg_0),//this
                new CodeMatch(OpCodes.Ldfld, typeof(Main).GetField("playOldTile", BindingFlags.Instance | BindingFlags.NonPublic)),
                new CodeMatch(OpCodes.Brfalse),

                new CodeMatch(OpCodes.Ldsfld, typeof(Main).GetField(nameof(Main.spriteBatch))),
                new CodeMatch(OpCodes.Ldsfld, typeof(TextureAssets).GetField(nameof(TextureAssets.Logo3)))
                );
            codeMatcher.ThrowIfInvalid("找不到IL位置");

            codeMatcher.Advance(1);
            codeMatcher.InsertAndAdvance(new CodeMatch(OpCodes.Pop));//从堆栈移除Ldarg_0//this

            codeMatcher.RemoveUntilForward(
                new CodeMatch(OpCodes.Ldsfld, typeof(Main).GetField(nameof(Main.dayTime))),
                new CodeMatch(OpCodes.Brfalse_S),
                new CodeMatch(OpCodes.Ldsfld, typeof(WorldGen).GetField(nameof(WorldGen.remixWorldGen))),
                new CodeMatch(OpCodes.Brtrue_S)
                );

            codeMatcher.InsertAndAdvance(
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld, typeof(Main).GetField("playOldTile", BindingFlags.Instance | BindingFlags.NonPublic)),
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld, typeof(Main).GetField("boulderLogo", BindingFlags.Instance | BindingFlags.NonPublic)),
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld, typeof(Main).GetField("logoRotation", BindingFlags.Instance | BindingFlags.NonPublic)),
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld, typeof(Main).GetField("logoScale", BindingFlags.Instance | BindingFlags.NonPublic)),
                new CodeMatch(OpCodes.Call, typeof(PMain).GetMethod(nameof(DrawLogo), BindingFlags.Static | BindingFlags.NonPublic))
            );

            return codeMatcher.Instructions();
        }

        private static void DrawLogo(bool playOldTile, bool boulderLogo, float logoRotation, float logoScale)
        {
            ThisMod.Api.MenuStyle.DrawLogo(playOldTile, boulderLogo, logoRotation, logoScale);
        }
    }
}
