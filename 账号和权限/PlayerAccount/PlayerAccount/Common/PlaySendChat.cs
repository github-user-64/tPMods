using tContentPatch;
using Terraria;
using Terraria.Chat;

namespace PlayerAccount.Common
{
    internal class PlaySendChat : Mod
    {
        public override void Load()
        {
            PatchGame.PatchChatCommandProcessor.OnCanProcessIncomingMessage.Add(a1);
        }

        //服务端收到聊天时
        public static bool a1(ChatMessage message, int clientId)
        {
            if (Main.netMode != 2) return true;

            if (Main.player?.IndexInRange(clientId) == false) return true;



            return false;
        }
    }
}
