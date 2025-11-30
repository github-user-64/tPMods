using HarmonyLib;
using Terraria.Chat;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(ChatCommandProcessor))]
    internal class PatchChatCommandProcessor
    {
        [HarmonyPatch("ProcessIncomingMessage")]
        [HarmonyPrefix]
        public static bool ProcessIncomingMessagePrefix(ChatMessage message, int clientId)//客户端和服务端处理传入消息时
        {
            try
            {
                return Common.GameChatCommand.GameChatCommand.OnProcessIncomingMessage(message, clientId);
            }
            catch { return true; }
        }
    }
}
