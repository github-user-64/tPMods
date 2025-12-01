using System.Reflection;
using Terraria;
using Terraria.ID;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 客户端uuid
    /// </summary>
    public static class ClientUUID
    {
        private static string[] uuid = null;
        private static string[] name = null;

        internal static void Init()
        {
            uuid = new string[Main.player.Length];
            name = new string[uuid.Length];
            PatchGame.PatchMessageBuffer.OnGetDataPo += GetData;
            PatchGame.PatchNetMessage.OnSyncDisconnectedPlayer += SyncDisconnectedPlayer;
        }

        private static void SyncDisconnectedPlayer(int ply)
        {
            if (uuid?.IndexInRange(ply) != true) return;

            uuid[ply] = null;
            name[ply] = null;
        }

        private static void GetData(MessageBuffer This, int start, int length, int messageType)
        {
            if (Main.netMode != 2) return;

            int index = This.whoAmI;

            if (uuid?.IndexInRange(index) != true) return;

            if (messageType != MessageID.Unknown68) return;

            Player p = ServerUtils.GetPlay(index);
            if (p == null) return;

            uuid[index] = This.reader.ReadString();
            name[index] = p.name;
        }

        /// <summary>
        /// 获取玩家的uuid, 不存在返回<see langword="null"/>
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static string GetClientUUID(this Player player)
        {
            RemoteClient c = player.GetClient();
            if (c == null) return null;

            int index = player.whoAmI;

            if (uuid?.IndexInRange(index) != true) return null;

            if (player.name != name[index]) return null;

            return uuid[index];
        }
    }
}
