using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria.Chat;

namespace PlayerAccount.PatchGame
{
    /// <summary>
    /// 修补<see cref="ChatCommandProcessor"/>
    /// </summary>
    [HarmonyPatch(typeof(ChatCommandProcessor))]
    internal class PatchChatCommandProcessor
    {
        /// <summary>
        /// 是否可以处理传入消息
        /// </summary>
        public static List<Func<ChatMessage, int, bool>> OnCanProcessIncomingMessage { get; } = new List<Func<ChatMessage, int, bool>>();

        [HarmonyPatch("ProcessIncomingMessage")]
        [HarmonyPrefix]
        internal static bool CanProcessIncomingMessage(ChatMessage message, int clientId)//客户端和服务端处理传入消息时
        {
            return Utils.Utils.A2(OnCanProcessIncomingMessage, message, clientId);
        }
    }
}
