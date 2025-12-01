using HarmonyLib;
using System;
using Terraria;

namespace PlayerAccount.PatchGame
{
    /// <summary>
    /// 修补<see cref="NetMessage"/>
    /// </summary>
    [HarmonyPatch(typeof(NetMessage))]
    public class PatchNetMessage
    {
        /// <summary>
        /// 在同步已连接玩家时
        /// </summary>
        public static event Action<int> OnSyncConnectedPlayer = null;

        [HarmonyPatch("SyncConnectedPlayer")]
        [HarmonyPrefix]
        internal static void SyncConnectedPlayer(int plr)
        {
            OnSyncConnectedPlayer?.Invoke(plr);
        }
    }
}
