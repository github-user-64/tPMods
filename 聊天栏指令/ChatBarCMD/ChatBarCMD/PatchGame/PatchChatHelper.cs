using HarmonyLib;
using Terraria.Chat;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(ChatHelper))]
    internal class PatchChatHelper
    {
        [HarmonyPatch("SendChatMessageFromClient")]
        [HarmonyPrefix]
        public static bool SendChatMessageFromClientPrefix(ChatMessage message)//客户端发送聊天时
        {
            try
            {
                return Common.GameChatCommand.GameChatCommand.OnSendChatMessageFromClient(message);
            }
            catch { return true; }
        }
    }
}
