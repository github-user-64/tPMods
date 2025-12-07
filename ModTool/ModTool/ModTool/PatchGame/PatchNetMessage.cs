using System;
using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NetMessage"/>
    /// </summary>
    public class PatchNetMessage : tContentPatch.PatchNetMessage
    {
        /// <summary>服务端在同步已连接玩家前</summary>
        public static event Action<int> OnSyncConnectedPlayerPr = null;
        /// <summary>服务端在同步断开连接玩家前</summary>
        public static event Action<int> OnSyncDisconnectedPlayerPr = null;

        /// <inheritdoc/>
        public override void SyncConnectedPlayerPrefix(int plr)
        {
            OnSyncConnectedPlayerPr?.Invoke(plr);
        }

        /// <inheritdoc/>
        public override void SyncDisconnectedPlayerPrefix(int plr)
        {
            OnSyncDisconnectedPlayerPr?.Invoke(plr);
        }
    }
}
