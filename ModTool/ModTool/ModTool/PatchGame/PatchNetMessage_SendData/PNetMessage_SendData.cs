using HarmonyLib;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ModTool.PatchGame.PatchNetMessage_SendData
{
    /// <summary>
    /// 修补<see cref="NetMessage.SendData(int, int, int, NetworkText, int, float, float, float, int, int, int)"/>
    /// </summary>
    [HarmonyPatch(typeof(NetMessage))]
    internal class PNetMessage_SendData : tContentPatch.PatchNetMessage
    {
        [HarmonyPatch("SendData")]
        [HarmonyPrefix]
        internal static bool SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null,
            int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
        {
            //if (Main.netMode == 0) return true;
            if (Main.netMode != 2) return true;
            if (msgType == 21 && (Main.item[number].shimmerTime > 0f || Main.item[number].shimmered)) msgType = 145;

            if (text == null) text = NetworkText.Empty;

            int num = 256;
            if (Main.netMode == 2 && remoteClient >= 0) num = remoteClient;

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
                    if (Netplay.Connection.Socket.IsConnected())
                    {
                        try
                        {
                            NetMessage.buffer[num].spamCount++;
                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                            Netplay.Connection.Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Connection.ClientWriteCallBack);
                        }
                        catch
                        {
                        }
                    }
                }
                else if (remoteClient == -1)
                {
                    switch (msgType)
                    {
                        case 34:
                        case 69:
                            {
                                for (int num23 = 0; num23 < 256; num23++)
                                {
                                    if (num23 != ignoreClient && NetMessage.buffer[num23].broadcast && Netplay.Clients[num23].IsConnected())
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num23].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num23].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num23].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;
                            }
                        case 20:
                            {
                                for (int num27 = 0; num27 < 256; num27++)
                                {
                                    if (num27 != ignoreClient && NetMessage.buffer[num27].broadcast && Netplay.Clients[num27].IsConnected() && Netplay.Clients[num27].SectionRange((int)Math.Max(number3, number4), number, (int)number2))
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num27].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num27].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num27].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;
                            }
                        case 23:
                            {
                                NPC nPC4 = Main.npc[number];
                                for (int num28 = 0; num28 < 256; num28++)
                                {
                                    if (num28 == ignoreClient || !NetMessage.buffer[num28].broadcast || !Netplay.Clients[num28].IsConnected())
                                    {
                                        continue;
                                    }
                                    bool flag6 = false;
                                    if (nPC4.boss || nPC4.netAlways || nPC4.townNPC || !nPC4.active)
                                    {
                                        flag6 = true;
                                    }
                                    else if (nPC4.netSkip <= 0)
                                    {
                                        Rectangle rect5 = Main.player[num28].getRect();
                                        Rectangle rect6 = nPC4.getRect();
                                        rect6.X -= 2500;
                                        rect6.Y -= 2500;
                                        rect6.Width += 5000;
                                        rect6.Height += 5000;
                                        if (rect5.Intersects(rect6))
                                        {
                                            flag6 = true;
                                        }
                                    }
                                    else
                                    {
                                        flag6 = true;
                                    }
                                    if (flag6)
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num28].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num28].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num28].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                nPC4.netSkip++;
                                if (nPC4.netSkip > 4)
                                {
                                    nPC4.netSkip = 0;
                                }
                                break;
                            }
                        case 28:
                            {
                                NPC nPC3 = Main.npc[number];
                                for (int num25 = 0; num25 < 256; num25++)
                                {
                                    if (num25 == ignoreClient || !NetMessage.buffer[num25].broadcast || !Netplay.Clients[num25].IsConnected())
                                    {
                                        continue;
                                    }
                                    bool flag5 = false;
                                    if (nPC3.life <= 0)
                                    {
                                        flag5 = true;
                                    }
                                    else
                                    {
                                        Rectangle rect3 = Main.player[num25].getRect();
                                        Rectangle rect4 = nPC3.getRect();
                                        rect4.X -= 3000;
                                        rect4.Y -= 3000;
                                        rect4.Width += 6000;
                                        rect4.Height += 6000;
                                        if (rect3.Intersects(rect4))
                                        {
                                            flag5 = true;
                                        }
                                    }
                                    if (flag5)
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num25].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num25].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num25].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;
                            }
                        case 13:
                            {
                                for (int num26 = 0; num26 < 256; num26++)
                                {
                                    if (num26 != ignoreClient && NetMessage.buffer[num26].broadcast && Netplay.Clients[num26].IsConnected())
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num26].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num26].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num26].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                Main.player[number].netSkip++;
                                if (Main.player[number].netSkip > 2)
                                {
                                    Main.player[number].netSkip = 0;
                                }
                                break;
                            }
                        case 27:
                            {
                                Projectile projectile2 = Main.projectile[number];
                                for (int num24 = 0; num24 < 256; num24++)
                                {
                                    if (num24 == ignoreClient || !NetMessage.buffer[num24].broadcast || !Netplay.Clients[num24].IsConnected())
                                    {
                                        continue;
                                    }
                                    bool flag4 = false;
                                    if (projectile2.type == 12 || Main.projPet[projectile2.type] || projectile2.aiStyle == 11 || projectile2.netImportant)
                                    {
                                        flag4 = true;
                                    }
                                    else
                                    {
                                        Rectangle rect = Main.player[num24].getRect();
                                        Rectangle rect2 = projectile2.getRect();
                                        rect2.X -= 5000;
                                        rect2.Y -= 5000;
                                        rect2.Width += 10000;
                                        rect2.Height += 10000;
                                        if (rect.Intersects(rect2))
                                        {
                                            flag4 = true;
                                        }
                                    }
                                    if (flag4)
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num24].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num24].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num24].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                for (int num22 = 0; num22 < 256; num22++)
                                {
                                    if (num22 != ignoreClient && (NetMessage.buffer[num22].broadcast || (Netplay.Clients[num22].State >= 3 && msgType == 10)) && Netplay.Clients[num22].IsConnected())
                                    {
                                        try
                                        {
                                            NetMessage.buffer[num22].spamCount++;
                                            Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                                            Netplay.Clients[num22].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[num22].ServerWriteCallBack);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
                else if (Netplay.Clients[remoteClient].IsConnected())
                {
                    try
                    {
                        NetMessage.buffer[remoteClient].spamCount++;
                        Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num21);
                        Netplay.Clients[remoteClient].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num21, Netplay.Clients[remoteClient].ServerWriteCallBack);
                    }
                    catch
                    {
                    }
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
                    Netplay.Clients[num].PendingTerminationApproved = true;
                }
            }

            return false;
        }
    }
}
