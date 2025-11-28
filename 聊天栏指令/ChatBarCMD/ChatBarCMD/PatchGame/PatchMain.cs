using HarmonyLib;
using Terraria;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(Main))]
    internal class PatchMain
    {
        [HarmonyPatch("DoUpdate_HandleChat")]
        [HarmonyPrefix]
        public static void DoUpdate_HandleChatPrefix()
        {
            Common.GameChatCommand.GameChatCommand.DoUpdate_HandleChatPrefix();
        }

        [HarmonyPatch("DoUpdate_HandleChat")]
        [HarmonyPostfix]
        public static void DoUpdate_HandleChatPostfix()
        {
            Common.GameChatCommand.GameChatCommand.DoUpdate_HandleChatPostfix();
        }
    }
}
