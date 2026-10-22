using ExtenContent.Extens;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Terraria;
using Terraria.ID;

namespace ExtenContentPatch
{
    internal static class ILUtils
    {
        public readonly static FieldInfo ItemID_Count = typeof(ItemID).GetField(nameof(ItemID.Count));
        public readonly static FieldInfo ArmorIDs_Head_Count = typeof(ArmorIDs.Head).GetField(nameof(ArmorIDs.Head.Count));
        public readonly static FieldInfo ArmorIDs_Body_Count = typeof(ArmorIDs.Body).GetField(nameof(ArmorIDs.Body.Count));
        public readonly static FieldInfo ArmorIDs_Legs_Count = typeof(ArmorIDs.Legs).GetField(nameof(ArmorIDs.Legs.Count));
        public readonly static FieldInfo ArmorIDs_Wing_Count = typeof(ArmorIDs.Wing).GetField(nameof(ArmorIDs.Wing.Count));
        public readonly static FieldInfo BuffID_Count = typeof(BuffID).GetField(nameof(BuffID.Count));
        public readonly static FieldInfo Main_maxMusic = typeof(Main).GetField(nameof(Main.maxMusic));

        public readonly static MethodInfo ItemLoader_ItemCount_Get = typeof(ItemLoader).GetMethod($"get_{nameof(ItemLoader.ItemCount)}");
        public readonly static MethodInfo EquipLoader_HeadCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.HeadCount)}");
        public readonly static MethodInfo EquipLoader_BodyCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.BodyCount)}");
        public readonly static MethodInfo EquipLoader_LegsCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.LegsCount)}");
        public readonly static MethodInfo EquipLoader_WingCount_Get = typeof(EquipLoader).GetMethod($"get_{nameof(EquipLoader.WingCount)}");
        public readonly static MethodInfo BuffLoader_BuffCount_Get = typeof(BuffLoader).GetMethod($"get_{nameof(BuffLoader.BuffCount)}");
        public readonly static MethodInfo MusicLoader_MusicCount_Get = typeof(MusicLoader).GetMethod($"get_{nameof(MusicLoader.MusicCount)}");

        public static IEnumerable<CodeInstruction> StaticFieldToMethod(this IEnumerable<CodeInstruction> instructions, FieldInfo fi, MethodInfo mi)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            int count = 0;
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

                    ++count;
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

            if (count < 1) throw new Exception($"替换失败:没有可替换:{fi?.Name}->{mi?.Name}");

            return codeMatcher.Instructions();
        }

        public static IEnumerable<CodeInstruction> ItemToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ItemID_Count,
                ItemLoader_ItemCount_Get);
        }

        public static IEnumerable<CodeInstruction> HeadToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Head_Count,
                EquipLoader_HeadCount_Get);
        }

        public static IEnumerable<CodeInstruction> BodyToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Body_Count,
                EquipLoader_BodyCount_Get);
        }

        public static IEnumerable<CodeInstruction> LegsToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Legs_Count,
                EquipLoader_LegsCount_Get);
        }

        public static IEnumerable<CodeInstruction> WingToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                ArmorIDs_Wing_Count,
                EquipLoader_WingCount_Get);
        }

        public static IEnumerable<CodeInstruction> BuffToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                BuffID_Count,
                BuffLoader_BuffCount_Get);
        }

        public static IEnumerable<CodeInstruction> MaxMusicToLoader(this IEnumerable<CodeInstruction> instructions)
        {
            return instructions.StaticFieldToMethod(
                Main_maxMusic,
                MusicLoader_MusicCount_Get);
        }
    }
}
