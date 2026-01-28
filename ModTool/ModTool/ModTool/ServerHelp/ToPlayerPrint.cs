using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.NetModules;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 输出到玩家
    /// </summary>
    public static class ToPlayerPrint
    {
        /// <summary>
        /// 字符串转包
        /// </summary>
        public static NetPacket StrintToPacket(string text, Color color)
        {
            NetworkText nText = NetworkText.FromLiteral(text);
            NetPacket packet = NetTextModule.SerializeServerMessage(nText, color);
            return packet;
        }

        /// <summary>
        /// 输出到玩家
        /// </summary>
        public static void PrintToPlay(int clientId, string text, Color color)
        {
            if (Main.dedServ == false) return;

            SendData(clientId, StrintToPacket(text, color), ChatHelper.OnlySendToPlayersWhoAreLoggedIn);
        }

        /// <summary>
        /// 输出到全部玩家
        /// </summary>
        public static void PrintToPlayAll(string text, Color color, int ignoreClient = -1)
        {
            if (Main.dedServ == false) return;

            NetPacket packet = StrintToPacket(text, color);

            for (int i = 0; i < Main.player?.Length; ++i)
            {
                if (i == ignoreClient) continue;

                SendData(i, packet, ChatHelper.OnlySendToPlayersWhoAreLoggedIn);
            }
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        public static void SendData(int clientId, NetPacket packet, Func<int, bool> canSend = null)
        {
            if (Main.dedServ == false) return;

            if (Netplay.Clients?.IndexInRange(clientId) != true) return;
            if (Netplay.Clients[clientId].IsConnected() == false) return;
            if (canSend != null && canSend.Invoke(clientId) == false) return;

            SendData(Netplay.Clients[clientId].Socket, packet);
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        public static void SendData(ISocket socket, NetPacket packet)
        {
            if (Main.dedServ == false) return;

            packet.ShrinkToFit();
            try
            {
                socket.AsyncSend(packet.Buffer.Data, 0, packet.Length, new SocketSendCallback(EmptyCallback), packet);
            }
            catch
            {
            }

            Main.ActiveNetDiagnosticsUI.CountSentModuleMessage(packet.Id, packet.Length);
        }

        public static void EmptyCallback(object state) { }
    }
}
