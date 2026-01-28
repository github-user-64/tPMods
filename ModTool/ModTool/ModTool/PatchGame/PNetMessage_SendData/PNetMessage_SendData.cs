using HarmonyLib;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ModTool.PatchGame.PNetMessage_SendData
{
    /// <summary>
    /// 修补<see cref="NetMessage.SendData(int, int, int, NetworkText, int, float, float, float, int, int, int)"/>
    /// </summary>
    [HarmonyPatch(typeof(NetMessage))]
    internal static class PNetMessage_SendData
    {
        [HarmonyPatch("SendData")]
        [HarmonyPrefix]
        internal static bool SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null,
            int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
        {
            //if (Main.netMode == 0) return true;
            if (Main.dedServ == false) return true;
            if (msgType == 21 && (Main.item[number].shimmerTime > 0f || Main.item[number].shimmered))
            {
                msgType = 145;
            }
            if (msgType == 21 && Main.item[number].type == 0)
            {
                msgType = 151;
            }
            int num = 256;
            if (text == null)
            {
                text = NetworkText.Empty;
            }
            if (Main.netMode == 2 && remoteClient >= 0)
            {
                num = remoteClient;
            }
            lock (NetMessage.buffer[num])
            {
                BinaryWriter writer = NetMessage.buffer[num].writer;
                if (writer == null)
                {
                    NetMessage.buffer[num].ResetWriter();
                    writer = NetMessage.buffer[num].writer;
                }
                writer.BaseStream.Position = 0L;
                long position = writer.BaseStream.Position;
                writer.BaseStream.Position += 2L;
                writer.Write((byte)msgType);

                switch (msgType)
                {
                    case MessageID.WorldData: PWorldData.Foo(writer); break;
                    case MessageID.ShopOverride: PShopOverride.Foo(writer, number, number2, number3, number4, number5, number6); break;
                    default: return true;
                }

                int num21 = (int)writer.BaseStream.Position;
                if (num21 > 65535)
                {
                    throw new Exception("Maximum packet length exceeded. id: " + msgType + " length: " + num21);
                }
                writer.BaseStream.Position = position;
                writer.Write((ushort)num21);
                writer.BaseStream.Position = num21;
                if (Main.netMode == 1)
                {
                    if (Netplay.Connection.IsConnected())
                    {
                        SendPacketToServer(NetMessage.buffer[num].writeBuffer);
                    }
                }
                else if (remoteClient == -1)
                {
                    switch (msgType)
                    {
                        default:
                            {
                                for (int num22 = 0; num22 < 256; num22++)
                                {
                                    if (num22 != ignoreClient && (NetMessage.buffer[num22].broadcast || (Netplay.Clients[num22].State >= 3 && msgType == 10)) && Netplay.Clients[num22].IsConnected())
                                    {
                                        SendPacket(NetMessage.buffer[num].writeBuffer, num22);
                                    }
                                }
                                break;
                            }
                    }
                }
                else if (Netplay.Clients[remoteClient].IsConnected())
                {
                    switch (msgType)
                    {
                        default: break;
                    }
                    SendPacket(NetMessage.buffer[num].writeBuffer, remoteClient);
                }
                if (Main.verboseNetplay)
                {
                    for (int num29 = 0; num29 < num21; num29++)
                    {
                    }
                    for (int num30 = 0; num30 < num21; num30++)
                    {
                        _ = NetMessage.buffer[num].writeBuffer[num30];
                    }
                }
                NetMessage.buffer[num].writeLocked = false;
                if (msgType == 2 && Main.netMode == 2)
                {
                    Netplay.Clients[num].PendingTermination = true;
                }

                return false;
            }
        }

        private static void SendPacketToServer(byte[] data)
        {
            SendPacket(data, 256);
        }

        private static void SendPacket(byte[] data, int remoteClient)
        {
            try
            {
                ushort num = BitConverter.ToUInt16(data, 0);
                byte messageId = data[2];
                NetMessage.buffer[remoteClient].spamCount++;
                Main.ActiveNetDiagnosticsUI.CountSentMessage(messageId, num);
                if (!Main.dedServ)
                {
                    Netplay.Connection.Socket.AsyncSend(data, 0, num, Netplay.Connection.ClientWriteCallBack);
                }
                else
                {
                    Netplay.Clients[remoteClient].Socket.AsyncSend(data, 0, num, Netplay.Clients[remoteClient].ServerWriteCallBack);
                }
            }
            catch
            {
                _ = Main.dedServ;
            }
        }
    }
}
