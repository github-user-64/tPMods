using HarmonyLib;
using Terraria;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;

namespace PlayerAccount.PatchGame
{
    /// <summary>
    /// 修补<see cref="MessageBuffer"/>
    /// </summary>
    [HarmonyPatch(typeof(MessageBuffer))]
    public class PatchMessageBuffer
    {
        [HarmonyPatch("GetData")]
        [HarmonyPostfix]
        internal static void GetData(MessageBuffer __instance, int start, int length, int messageType)
        {
            if (Main.netMode != 2) return;

            int num = start + 1;

            __instance.reader.BaseStream.Position = num;

            switch (messageType)
            {
                case 68:
                    tContentPatch.ContentPatch.PrintTry($"i:{__instance.whoAmI}");
                    tContentPatch.ContentPatch.PrintTry($"uuid:{__instance.reader.ReadString()}");

                    ISocket s = Netplay.Clients[__instance.whoAmI].Socket;
                    RemoteAddress ra = s.GetRemoteAddress();
                    TcpAddress ta = ra as TcpAddress;

                    tContentPatch.ContentPatch.PrintTry($"ip:{ra.GetIdentifier()}");
                    tContentPatch.ContentPatch.PrintTry($"ip:{ta.Address}:{ta.Port}");
                    break;
                default: break;
            }
        }
    }
}
