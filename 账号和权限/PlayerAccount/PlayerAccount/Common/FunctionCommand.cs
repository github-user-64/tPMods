using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 指令功能
    /// </summary>
    public static class FunctionCommand
    {
        /// <summary>
        /// 输出玩家列表
        /// </summary>
        /// <param name="print"></param>
        public static void playing(Action<string> print)
        {
            if (print == null) return;

            string s = null;
            int len = 0;

            for (int i = 0; i < Main.player?.Length; ++i)
            {
                Player player = Main.player[i];
                if (player == null) continue;
                if (player.active == false) continue;

                string text = null;

                if (s == null) text = $"{player.name}";
                else text = $",{player.name}";

                if (len > 32)
                {
                    s = $"{s}\n{text}";
                    len = text.Length;
                }
                else
                {
                    s = $"{s}{text}";
                    len += text.Length;
                }
            }

            PlayerGroup.Utils.Utils.PrintTry(s ?? "没有玩家", print);
        }

        /// <summary>
        /// 踢出玩家
        /// </summary>
        /// <param name="whoAmI"></param>
        /// <param name="msg"></param>
        /// <param name="print"></param>
        public static void kick(int whoAmI, string msg, Action<string> print)
        {
            if (Main.player?.IndexInRange(whoAmI) == false)
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
