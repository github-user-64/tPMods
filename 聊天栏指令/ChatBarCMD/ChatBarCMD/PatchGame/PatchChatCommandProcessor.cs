using ChatBarCMD.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;

namespace ChatBarCMD.PatchGame
{
    [HarmonyPatch(typeof(ChatCommandProcessor))]
    internal class PatchChatCommandProcessor
    {
        /// <summary>
        /// 单人和服务端是否可以处理传入消息
        /// </summary>
        public static List<Func<ChatMessage, int, bool>> CanProcessIncomingMessage { get; } = new List<Func<ChatMessage, int, bool>>();

        [HarmonyPatch("ProcessIncomingMessage")]
        [HarmonyPrefix]
        public static bool ProcessIncomingMessagePrefix(ChatMessage message, int clientId)//单人和服务端处理传入消息时
        {
            if (Main.netMode != 1 && Main.netMode != 2) return true;

            return CanProcessIncomingMessage.A2(message, clientId);
        }
    }
}
