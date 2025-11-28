using Terraria;
using Terraria.Chat;

namespace ChatBarCMD.Common.GameChatCommand
{
    public partial class GameChatCommand
    {
        private static string oldChatText = null;

        internal static void DoUpdate_HandleChatPrefix()
        {
            oldChatText = Main.chatText;
        }

        internal static void DoUpdate_HandleChatPostfix()
        {
            UpdateTip(Main.chatText, oldChatText != Main.chatText);
        }

        internal static bool OnProcessIncomingMessage(ChatMessage message, int clientId)//玩家输入文本时
        {
            if (clientId != Main.myPlayer) return true;

            return OnSendChat(message?.Text);
        }

        internal static bool OnSendChatMessageFromClient(ChatMessage message)//玩家输入文本时
        {
            return OnSendChat(message?.Text);
        }

        internal static bool OnSendChat(string chat)//在发送消息时
        {
            if (CanUse() == false) return true;

            string cmd = ChatToCMD(chat);
            if (cmd == null) return true;

            try
            {
                InputCMD(cmd);
            }
            catch { }

            return false;//不发送
        }
    }
}
