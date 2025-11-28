using HarmonyLib;
using Terraria.Chat;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(ChatHelper))]
    internal class PatchChatHelper
    {
        [HarmonyPatch("SendChatMessageFromClient")]
        [HarmonyPrefix]
        public static bool SendChatMessageFromClientPrefix(ChatMessage message)
        {
            try
            {
                return Common.GameChatCommand.GameChatCommand.OnSendChatMessageFromClient(message);
            }
            catch { return true; }
        }
    }
}
