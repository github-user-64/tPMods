using ExtenContent.Extens;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using tContentPatch;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.IO;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Player))]
    internal partial class PPlayer : PatchPlayer
    {
        public override void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            ItemLoad.LoadPlayerPostfix(result, playerPath, cloudSave);
        }

        public override void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            ItemLoad.SavePlayerPrefix(playerFile, skipMapSave);
        }

        public override void ApplyEquipFunctionalPostfix(Player This, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipFunctionalPostfix(This, itemSlot, currentItem);
            EquipLoad.ApplyEquipFunctionalPostfix(This, itemSlot, currentItem);
        }

        public override void ApplyEquipVanityPostfix(Player This, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipVanityPostfix(This, itemSlot, currentItem);
            EquipLoad.ApplyEquipVanityPostfix(This, itemSlot, currentItem);
        }

        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPostfix]
        private static void PlayerPostfix(Player __instance)
        {
            __instance.ownedProjectileCounts = new int[ProjectileLoad.ProjectileCount];
        }

        [HarmonyPatch("ApplyItemAnimation")]
        [HarmonyPostfix]
        private static void ApplyItemAnimationPostfix(Player __instance, Item sItem)
        {
            ItemLoad.ApplyItemAnimationPostfix(__instance, sItem);
        }

        [HarmonyPatch("ItemCheck_CheckCanUse_Inner")]
        [HarmonyPostfix]
        private static void ItemCheck_CheckCanUse_InnerPostfix(ref bool __result, Player __instance, Item sItem, bool ignoreCursed = false)
        {
            ItemLoad.CanUseItem(ref __result, __instance, sItem);
        }

        [HarmonyPatch("ItemCheck_ManageRightClickFeatures")]
        [HarmonyPostfix]
        private static void ItemCheck_ManageRightClickFeaturesPostfix(Player __instance)
        {
            Player player = __instance;

            bool flag = player.selectedItem != 58 && player.controlUseTile && Main.myPlayer == player.whoAmI &&
                !player.tileInteractionHappened && player.releaseUseItem && !player.controlUseItem && !player.mouseInterface &&
                !CaptureManager.Instance.Active && (!Main.mouseRightRelease || !Main.HoveringAnInteractable) && !Main.LocalPlayerHasPendingInventoryActions();

            Item item = player.inventory[player.selectedItem];

            if (!ItemID.Sets.ItemsThatAllowRepeatedRightClick[item.type] && !Main.mouseRightRelease)
            {
                flag = false;
            }

            if (flag && player.altFunctionUse == 0 && ItemLoad.AltFunctionUse(player, item))
            {
                player.altFunctionUse = 1;
                player.controlUseItem = true;
            }
        }

        [HarmonyPatch("ProcessHitAgainstNPC")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> TranspilerProcessHitAgainstNPC(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions);

            codeMatcher.MatchStartForward(
               new CodeMatch(OpCodes.Ldarg_0),//0是Player应该是this的意思
               new CodeMatch(OpCodes.Ldarg_1),
               new CodeMatch(OpCodes.Ldarg_2),
               new CodeMatch(OpCodes.Ldloc_S),
               new CodeMatch(OpCodes.Ldarg_S),
               new CodeMatch(OpCodes.Ldloc_0),
               new CodeMatch(OpCodes.Ldloc_S),
               new CodeMatch(OpCodes.Ldloc_S),
               new CodeMatch(OpCodes.Call, typeof(Player).GetMethod("ApplyNPCOnHitEffects", BindingFlags.NonPublic | BindingFlags.Instance)),
               new CodeMatch(OpCodes.Ldloc_0)
               )
               .ThrowIfInvalid("找不到IL位置")
               .Advance(0)
               .RemoveInstructions(0)
               .InsertAndAdvance(
               new CodeMatch(OpCodes.Ldarg_0),
               new CodeMatch(OpCodes.Ldarg_1),
               new CodeMatch(OpCodes.Ldarg_2),
               new CodeMatch(OpCodes.Ldarg_3),
               new CodeMatch(OpCodes.Ldarg_S, 4),
               new CodeMatch(OpCodes.Ldarg_S, 5),
               new CodeMatch(OpCodes.Call, typeof(PPlayer).GetMethod(nameof(OnHitNPC), BindingFlags.NonPublic | BindingFlags.Static))
               );

            return codeMatcher.Instructions();
        }

        private static void OnHitNPC(Player player, Item sItem, Rectangle itemRectangle, int originalDamage, float knockBack, int npcIndex)
        {
            NPC npc = Main.npc[npcIndex];

            ItemLoad.OnHitNPC(player, sItem, itemRectangle, originalDamage, knockBack, npc);
        }
    }
}
