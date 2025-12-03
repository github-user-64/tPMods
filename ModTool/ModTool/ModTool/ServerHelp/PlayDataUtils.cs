using Terraria;
using Terraria.Net;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 玩家数据工具
    /// </summary>
    public static class PlayDataUtils
    {
        /// <summary>
        /// 获取数据正常的玩家, 不存在返回<see langword="null"/>
        /// </summary>
        public static Player GetPlay(int index)
        {
            if (Main.netMode != 2) return null;
            if (Main.player?.IndexInRange(index) != true) return null;

            Player player = Main.player[index];
            if (player?.whoAmI != index) return null;

            if (GetClient(player) == null) return null;

            return player;
        }

        /// <summary>
        /// 获取正常玩家, 不存在返回<see langword="null"/>
        /// </summary>
        public static Player GetPlay(this Player player)
        {
            if (GetClient(player) == null) return null;
            return player;
        }

        /// <summary>
        /// 获取玩家客户端, 不存在返回<see langword="null"/>
        /// </summary>
        public static RemoteClient GetClient(this Player player)
        {
            if (Main.netMode != 2) return null;

            if (player == null) return null;
            //if (player.active == false) return null;
            if (player.name == null) return null;
            if (Main.player?.IndexInRange(player.whoAmI) != true) return null;
            if (Main.player[player.whoAmI] != player) return null;
            if (Netplay.Clients?.IndexInRange(player.whoAmI) != true) return null;

            RemoteClient c = Netplay.Clients[player.whoAmI];
            if (c == null) return null;

            return c.IsConnected() ? c : null;
        }

        /// <summary>
        /// 获取玩家Tcp地址, 不存在返回<see langword="null"/>
        /// </summary>
        public static TcpAddress GetTcpAddress(this Player player)
        {
            RemoteClient c = GetClient(player);
            if (c == null) return null;

            RemoteAddress ra = c.Socket?.GetRemoteAddress();
            TcpAddress ta = ra as TcpAddress;

            return ta;
        }

        /// <summary>
        /// 获取玩家地址, 不存在返回<see langword="null"/>
        /// </summary>
        public static string GetIP(this Player player)
        {
            TcpAddress t = GetTcpAddress(player);
            if (t == null) return null;

            return t.Address?.ToString();
        }

        /// <summary>
        /// 获取玩家端口, 不存在返回-1
        /// </summary>
        public static int GetPort(this Player player)
        {
            TcpAddress t = GetTcpAddress(player);
            if (t == null) return -1;

            return t.Port;
        }

        /// <summary>
        /// 获取玩家uuid, 不存在返回<see langword="null"/>
        /// </summary>
        public static string GetUUID(this Player player)
        {
            if (Main.netMode != 2) return null;

            return ClientUUID.GetUUID(player);
        }
    }
}
