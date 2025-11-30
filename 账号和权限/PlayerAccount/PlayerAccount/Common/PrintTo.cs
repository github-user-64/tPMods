using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.NetModules;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 输出到
    /// </summary>
    public class PrintTo
    {
        /// <summary>
        /// 字符串转包
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static NetPacket StrintToPacket(string text, Color color)
        {
            NetworkText nText = NetworkText.FromLiteral(text);
            NetPacket packet = NetTextModule.SerializeServerMessage(nText, color);
            return packet;
        }

        /// <summary>
        /// 输出到玩家
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void PrintToPlay(int clientId, string text, Color color)
        {
            if (Main.netMode != 2) return;

            SendData(clientId, StrintToPacket(text, color), ChatHelper.OnlySendToPlayersWhoAreLoggedIn);
        }

        /// <summary>
        /// 输出到全部玩家
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="ignoreClient"></param>
        public static void PrintToPlayAll(string text, Color color, int ignoreClient = -1)
        {
            if (Main.netMode != 2) return;

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
        /// <param name="clientId"></param>
        /// <param name="packet"></param>
        /// <param name="canSend"></param>
        public static void SendData(int clientId, NetPacket packet, Func<int, bool> canSend = null)
        {
            if (Main.netMode != 2) return;

            if (Netplay.Clients?.IndexInRange(clientId) == false) return;
            if (Netplay.Clients[clientId].IsConnected() == false) return;
            if (canSend != null && canSend.Invoke(clientId) == false) return;

            SendData(Netplay.Clients[clientId].Socket, packet);
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="packet"></param>
        public static void SendData(ISocket socket, NetPacket packet)
        {
            if (Main.netMode != 2) return;

            packet.ShrinkToFit();
            try
            {
                socket.AsyncSend(packet.Buffer.Data, 0, packet.Length, new SocketSendCallback(NetManager.SendCallback), packet);
            }
            catch
            {
            }

            Main.ActiveNetDiagnosticsUI.CountSentModuleMessage(packet.Id, packet.Length);
        }
    }
}
