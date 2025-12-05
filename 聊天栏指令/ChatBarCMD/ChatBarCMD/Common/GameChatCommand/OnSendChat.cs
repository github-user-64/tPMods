using ChatBarCMD.PatchGame;
using Microsoft.Xna.Framework;
using System;
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
                return OnGot(message.Text, clientId);
            }

            if (Main.netMode != 0) return true;
            if (clientId != Main.myPlayer) return true;
            //单人发送聊天时

            return OnSend(message.Text);
        }

        private static bool OnSendChatMessageFromClient(ChatMessage message)//客户端发送聊天时
        {
            if (Main.netMode != 1) return true;
            //客户端发送聊天时

            return OnSend(message?.Text);
        }

        private static bool OnSend(string chat)//单人和客户端发送聊天时
        {
            if (NetMode01.CanUse() == false) return true;

            string cmd = Utils.ChatToCMD(chat, out bool iss);
            if (iss) return true;//是发送到服务端则不处理
            if (cmd == null) return true;//不是指令则不处理

            try
            {
                Action<string> print = s => Utils.MainNewTextTry(s, B: 0);
                Utils.InputCMD(cmd, NetMode01.GetCMD(print), print);
            }
            catch { }

            return false;//不发送
        }

        private static bool OnGot(string chat, int clientId)//服务端收到聊天时
        {
            if (NetMode2.CanUse() == false) return true;

            string cmd = Utils.ChatToCMD(chat, NetMode2.Head.val);
            if (cmd == null) return true;

            try
            {
                Action<string> print = s => ModTool.ServerHelp.PrintTo.PrintToPlay(clientId, s, Color.Yellow);
                Utils.InputCMD(cmd, NetMode2.GetCMD(clientId, print), print);
            }
            catch { }

            return false;//不处理
        }
    }
}
