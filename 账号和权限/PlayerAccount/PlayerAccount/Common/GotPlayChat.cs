using tContentPatch;
using Terraria;
using Terraria.Chat;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 收到玩家聊天
    /// </summary>
    internal class GotPlayChat : Mod
    {
        /// <summary>
        /// 指令头
        /// </summary>
        public static string CMDHead = "/";

        public override void Load()
        {
            PatchGame.PatchChatCommandProcessor.OnCanProcessIncomingMessage.Add(a1);
        }

        //服务端收到聊天时
        private static bool a1(ChatMessage message, int clientId)
        {
            if (Main.netMode != 2) return true;

            if (Main.player?.IndexInRange(clientId) == false) return true;

            if (CMDHead == null || CMDHead == string.Empty) CMDHead = "/";

            string cmd = ChatToCMD(message?.Text, CMDHead);
            if (cmd == null) return true;

            try
            {
                PlayCommand.InputCMD(cmd, Main.player[clientId]);
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 判断聊天是否是指令, 是指令就返回指令内容
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        public static string ChatToCMD(string chat, string head)
        {
            if (chat == null) return null;

            if (chat.Length < head.Length) return null;

            string chatHead = chat.Substring(0, head.Length);
            if (chatHead != head) return null;

            return chat.Substring(head.Length);
        }
    }
}
