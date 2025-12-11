using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 踢出玩家
    /// </summary>
    public static class KickPlay
    {
        /// <summary>
        /// 踢出玩家, 成功返回<see langword="null"/>
        /// </summary>
        public static string Kick(int whoAmI, string msg)
        {
            if (Main.player?.IndexInRange(whoAmI) != true) return $"[{whoAmI}]不在范围内";
            if (Netplay.Clients?.IndexInRange(whoAmI) != true) return $"[{whoAmI}]不在范围内";

            NetMessage.SendData(MessageID.Kick, whoAmI, -1, NetworkText.FromLiteral(msg ?? string.Empty));

            ////踢出玩家
            Netplay.Clients[whoAmI].PendingTermination = true;
            Netplay.Clients[whoAmI].PendingTerminationApproved = true;

            try
            {
                Netplay.Clients[whoAmI].Reset();
                NetMessage.SyncDisconnectedPlayer(whoAmI);
            }
            catch { }

            return null;
        }
    }
}
