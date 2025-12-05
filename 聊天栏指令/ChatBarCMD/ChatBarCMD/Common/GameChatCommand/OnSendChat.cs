using ChatBarCMD.PatchGame;
using Terraria;
using Terraria.Chat;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 在发送聊天时
    /// </summary>
    internal static class OnSendChat
    {
        internal static void Init()
        {
            PatchChatCommandProcessor.CanProcessIncomingMessage.Add(OnProcessIncomingMessage);
            PatchChatHelper.CanSendChatMessageFromClient.Add(OnSendChatMessageFromClient);
        }

        private static bool OnProcessIncomingMessage(ChatMessage message, int clientId)//单人和服务端处理传入消息时
        {
            if (message == null) return true;

            if (Main.netMode == 2)//服务端收到聊天时
            {
                return NetMode2.OnGot(message.Text, clientId);
            }

            if (Main.netMode != 0) return true;
            if (clientId != Main.myPlayer) return true;
            //单人发送聊天时

            return NetMode01.OnSend(message.Text);
        }

        private static bool OnSendChatMessageFromClient(ChatMessage message)//客户端发送聊天时
        {
            if (message == null) return true;

            if (Main.netMode != 1) return true;
            //客户端发送聊天时

            return NetMode01.OnSend(message.Text);
        }
    }
}
