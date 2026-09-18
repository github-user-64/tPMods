using ExtenContent.Extens;
using HarmonyLib;
using tContentPatch;
using Terraria;
using Terraria.IO;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Player))]
    internal class PPlayer : PatchPlayer
    {
        public override void ItemCheck_ShootPostfix(Player This, Item item, int weaponDamage, bool withAudioVisualFeedback)
        {
            ItemLoad.ItemCheck_ShootPostfix(This, item, weaponDamage, withAudioVisualFeedback);
        }

        public override void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            ItemLoad.LoadPlayerPostfix(result, playerPath, cloudSave);
        }

        public override void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            ItemLoad.SavePlayerPrefix(playerFile, skipMapSave);
        }

        [HarmonyPatch("ApplyItemAnimation")]
        [HarmonyPostfix]
        private static void ApplyItemAnimationPostfix(Player __instance, Item sItem)
        {
            ItemLoad.ApplyItemAnimationPostfix(__instance, sItem);
        }

        [HarmonyPatch("ApplyEquipFunctional")]
        [HarmonyPostfix]
        private static void ApplyEquipFunctionalPostfix(Player __instance, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipFunctionalPostfix(__instance, itemSlot, currentItem);
            EquipLoader.ApplyEquipFunctionalPostfix(__instance, itemSlot, currentItem);
        }

        [HarmonyPatch("ApplyEquipVanity")]
        [HarmonyPostfix]
        private static void ApplyEquipVanityPostfix(Player __instance, int itemSlot, Item currentItem)
        {
            ItemLoad.ApplyEquipVanityPostfix(__instance, itemSlot, currentItem);
            EquipLoader.ApplyEquipVanityPostfix(__instance, itemSlot, currentItem);
        }
    }
}
