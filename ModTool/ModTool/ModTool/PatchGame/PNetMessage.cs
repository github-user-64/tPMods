using System;
using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NetMessage"/>
    /// </summary>
    public class PNetMessage : tContentPatch.PatchNetMessage
    {
        /// <summary>服务端在同步已连接玩家前</summary>
        public static event Action<int> OnSyncConnectedPlayerPr = null;
        /// <summary>服务端在同步已连接玩家后</summary>
        public static event Action<int> OnSyncConnectedPlayerPo = null;
        /// <summary>服务端在同步玩家断开连接前</summary>
        public static event Action<int> OnSyncOnePlayerDisconnectedPr = null;
        /// <summary>服务端在同步玩家断开连接后</summary>
        public static event Action<int> OnSyncOnePlayerDisconnectedPo = null;

        /// <inheritdoc/>
        public override void SyncConnectedPlayerPrefix(int plr)
        {
            OnSyncConnectedPlayerPr?.Invoke(plr);
        }

        /// <inheritdoc/>
        public override void SyncConnectedPlayerPostfix(int plr)
        {
            OnSyncConnectedPlayerPo?.Invoke(plr);
        }

        /// <inheritdoc/>
        public override void SyncOnePlayerPrefix(int plr, int toWho, int fromWho)
        {
            if (Main.player?.IndexInRange(plr) != true) return;
            if (Main.player[plr].active == true) return;

            OnSyncOnePlayerDisconnectedPr?.Invoke(plr);
        }

        /// <inheritdoc/>
        public override void SyncOnePlayerPostfix(int plr, int toWho, int fromWho)
        {
            if (Main.player?.IndexInRange(plr) != true) return;
            if (Main.player[plr].active == true) return;

            OnSyncOnePlayerDisconnectedPo?.Invoke(plr);
        }
    }
}
