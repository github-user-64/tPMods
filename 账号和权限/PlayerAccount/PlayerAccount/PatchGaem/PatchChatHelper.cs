using HarmonyLib;
using Microsoft.Xna.Framework;
using PlayerAccount.Common;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

namespace PlayerAccount.PatchGaem
{
    [HarmonyPatch(typeof(ChatHelper))]
    internal class PatchChatHelper
    {
        [HarmonyPatch("BroadcastChatMessageAs")]
        [HarmonyPrefix]
        public static bool BroadcastChatMessageAsPrefix(byte messageAuthor, NetworkText text, Color color, int excludedPlayer)
        {
            if (Main.dedServ == false) return true;
            if (messageAuthor == byte.MaxValue) return true;//只处理玩家聊天

            if (Main.player?.IndexInRange(messageAuthor) != true) return true;

            Player player = Main.player[messageAuthor];
            if (player == null) return true;

            return SetChat.a(player, text.ToString(), color, excludedPlayer);
        }
    }
}
