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

            string name = Main.player[whoAmI]?.name ?? string.Empty;

            NetMessage.SendData(MessageID.Kick, whoAmI, -1, NetworkText.FromLiteral(msg ?? string.Empty));

            return null;

            ////踢出玩家
            //Netplay.Clients[__instance.whoAmI].PendingTermination = true;
            //Netplay.Clients[__instance.whoAmI].PendingTerminationApproved = true;
        }
    }
}
