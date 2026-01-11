using Microsoft.Xna.Framework;
using ModTool.PatchGame;
using ModTool.ServerHelp;
using System.Diagnostics;
using Terraria;

namespace PlayerAccount.Common.ActionPermission
{
    internal class SendMsgToPlay : PMain
    {
        private static int[] cd = new int[Main.player.Length];

        public override void DoUpdateInWorldPrefix(Stopwatch sw)
        {
            for (int i = 0; i < cd.Length; i++)
            {
                if (cd[i] > 0) cd[i]--;
            }
        }

        public static void Send(Player player, string msg, Color color)
        {
            if (player == null) return;
            if (cd.IndexInRange(player.whoAmI) != true) return;

            if (cd[player.whoAmI] > 0) return;
            cd[player.whoAmI] = 60;

            ToPlayerPrint.PrintToPlay(player.whoAmI, msg, color);
        }
    }
}
