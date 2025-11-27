using HarmonyLib;
using Terraria.Chat;

namespace PlayerAccount.PatchGame
{
    [HarmonyPatch(typeof(ChatCommandProcessor))]
    internal class PatchChatCommandProcessor
    {
        [HarmonyPatch("ProcessIncomingMessage")]
        [HarmonyPrefix]
        public static bool ProcessIncomingMessagePrefix(ChatMessage message, int clientId)
        {
            try
            {
                return Common.GameChatCommand.GameChatCommand.OnProcessIncomingMessage(message, clientId);
            }
            catch { return true; }
        }
    }
}
