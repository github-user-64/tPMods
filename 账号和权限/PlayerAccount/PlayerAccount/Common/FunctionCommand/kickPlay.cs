using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 踢出玩家
    /// </summary>
    public static class kickPlay
    {
        /// <summary>
        /// 踢出玩家
        /// </summary>
        /// <param name="whoAmI"></param>
        /// <param name="msg"></param>
        /// <param name="print"></param>
        public static void kick(int whoAmI, string msg, Action<string> print)
        {
            if (Main.player?.IndexInRange(whoAmI) != true)
            {
                PlayerGroup.Utils.Utils.PrintTry($"[{whoAmI}]不在范围内", print);
                return;
            }

            string name = Main.player[whoAmI]?.name ?? string.Empty;

            NetMessage.SendData(MessageID.Kick, whoAmI, -1, NetworkText.FromLiteral(msg ?? string.Empty));

            PlayerGroup.Utils.Utils.PrintTry($"已踢出[{name}]", print);

            ////踢出玩家
            //Netplay.Clients[__instance.whoAmI].PendingTermination = true;
            //Netplay.Clients[__instance.whoAmI].PendingTerminationApproved = true;
        }
    }
}
