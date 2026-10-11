using ExtenContent.Extens;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Terraria.ID;

namespace ExtenContentPatch
{
    internal static class Utils
    {
        public readonly static FieldInfo ArmorIDs_Head_Count = typeof(ArmorIDs.Head).GetField(nameof(ArmorIDs.Head.Count));
        public readonly static FieldInfo ArmorIDs_Body_Count = typeof(ArmorIDs.Body).GetField(nameof(ArmorIDs.Body.Count));
        public readonly static FieldInfo ArmorIDs_Legs_Count = typeof(ArmorIDs.Legs).GetField(nameof(ArmorIDs.Legs.Count));
        public readonly static MethodInfo EquipLoader_HeadCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.HeadCount)}");
        public readonly static MethodInfo EquipLoader_BodyCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.BodyCount)}");
        public readonly static MethodInfo EquipLoader_LegsCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.LegsCount)}");

        public static IEnumerable<CodeInstruction> StaticFieldToMethod(this IEnumerable<CodeInstruction> instructions, FieldInfo fi, MethodInfo mi)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            while (true)
            {
                try
                {
                    codeMatcher.MatchStartForward(
                        new CodeMatch(OpCodes.Ldsfld, fi)
                        )
                        .ThrowIfInvalid("找不到IL位置")
                        .Advance(0)
                        .RemoveInstructions(1)
                        .InsertAndAdvance(
                        new CodeMatch(OpCodes.Call, mi)
                        );
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    throw new Exception($"替换失败:{fi?.Name}->{mi?.Name}", ex);
                }
            }

            return codeMatcher.Instructions();
        }

        public static IEnumerable<CodeInstruction> HeadToEquipLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Head_Count,
                EquipLoader_HeadCount_Get);
        }

        public static IEnumerable<CodeInstruction> BodyToEquipLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Body_Count,
                EquipLoader_BodyCount_Get);
        }

        public static IEnumerable<CodeInstruction> LegsToEquipLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Legs_Count,
                EquipLoader_LegsCount_Get);
        }
    }
}
