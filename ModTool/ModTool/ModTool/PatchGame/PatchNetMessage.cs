using HarmonyLib;
using System;
using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NetMessage"/>
    /// </summary>
    [HarmonyPatch(typeof(NetMessage))]
    public class PatchNetMessage
    {
        /// <summary>在同步已连接玩家时</summary>
        public static event Action<int> OnSyncConnectedPlayer = null;
        /// <summary>在同步断开连接玩家时</summary>
        public static event Action<int> OnSyncDisconnectedPlayer = null;

        [HarmonyPatch("SyncConnectedPlayer")]
        [HarmonyPrefix]
        internal static void SyncConnectedPlayer(int plr)
        {
            OnSyncConnectedPlayer?.Invoke(plr);
        }

        [HarmonyPatch("SyncDisconnectedPlayer")]
        [HarmonyPrefix]
        internal static void SyncDisconnectedPlayer(int plr)
        {
            OnSyncDisconnectedPlayer?.Invoke(plr);
        }
    }
}
