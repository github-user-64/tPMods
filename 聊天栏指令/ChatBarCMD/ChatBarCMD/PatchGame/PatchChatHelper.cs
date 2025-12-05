using ChatBarCMD.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(ChatHelper))]
    internal class PatchChatHelper
    {
        /// <summary>
        /// 客户端是否可以发送聊天
        /// </summary>
        public static List<Func<ChatMessage, bool>> CanSendChatMessageFromClient { get; } = new List<Func<ChatMessage, bool>>();

        [HarmonyPatch("SendChatMessageFromClient")]
        [HarmonyPrefix]
        public static bool SendChatMessageFromClientPrefix(ChatMessage message)//客户端发送聊天时
        {
            if (Main.netMode != 1) return true;

            return CanSendChatMessageFromClient.A1(message);
        }
    }
}
