using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 玩家指令
    /// </summary>
    public class PlayCMD
    {
        /// <summary>
        /// 运行指令
        /// </summary>
        /// <param name="text"></param>
        /// <param name="player"></param>
        public static void InputCMD(string text, Player player)
        {
            if (text == null) return;

            string exText = tContentPatch.Command.Utils.CommandRun(text, GetGameCMD());
            if (exText != null) Main.NewText(exText);
        }
    }
}
