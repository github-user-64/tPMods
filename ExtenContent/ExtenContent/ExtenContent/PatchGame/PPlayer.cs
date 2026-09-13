using ExtenContent.Extens;
using HarmonyLib;
using Terraria;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(Player))]
    internal static class PPlayer
    {
        [HarmonyPatch("ItemCheck_Shoot")]
        [HarmonyPostfix]
        public static void ItemCheck_ShootPostfix(Player __instance, int i, Item sItem, int weaponDamage, bool withAudioVisualFeedback)
        {
            ItemLoad.ItemCheck_ShootPostfix(__instance, sItem, weaponDamage, withAudioVisualFeedback);
        }
    }
}
