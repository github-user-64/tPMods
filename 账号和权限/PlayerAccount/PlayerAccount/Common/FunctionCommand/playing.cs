using System;
using Terraria;

namespace PlayerAccount.Common.FunctionCommand
{
    /// <summary>
    /// 输出玩家列表
    /// </summary>
    public static class playing
    {
        /// <summary>
        /// 输出玩家列表
        /// </summary>
        /// <param name="print"></param>
        public static void print(Action<string> print)
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
    }
}
