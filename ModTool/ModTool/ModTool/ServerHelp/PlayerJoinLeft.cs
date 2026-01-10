using System;
using Terraria;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 玩家加入离开
    /// </summary>
    public static class PlayerJoinLeft
    {
        /// <summary>服务端在同步已连接玩家前</summary>
        public static event Action<int> OnSyncConnectedPlayerPr = null;
        /// <summary>服务端在同步已连接玩家后</summary>
        public static event Action<int> OnSyncConnectedPlayerPo = null;
        /// <summary>服务端在玩家客户端断开连接时</summary>
        public static event Action<Player> OnPlayerClientDisconnected = null;

        private class PNetMessage : tContentPatch.PatchNetMessage
        {
            public override void SyncConnectedPlayerPrefix(int plr)
            {
                OnSyncConnectedPlayerPr?.Invoke(plr);
            }

            public override void SyncConnectedPlayerPostfix(int plr)
            {
                OnSyncConnectedPlayerPo?.Invoke(plr);
            }
        }

        private class PRemoteClient : tContentPatch.PatchRemoteClient
        {
            public override void ResetPrefix(RemoteClient This)
            {
                if (This.IsActive == false) return;
                if (Main.dedServ == false) return;
                if (Main.player?.IndexInRange(This.Id) != true) return;

                Player player = Main.player[This.Id];
                if (player == null) return;

                OnPlayerClientDisconnected?.Invoke(player);
            }
        }
    }
}
