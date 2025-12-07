using Terraria;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号帮助
    /// </summary>
    public static class AccountHelp
    {
        /// <summary>
        /// 注册, 成功返回<see langword="null"/>
        /// </summary>
        public static string Register(this Player player, string password = null)
        {
            if (player == null) return "玩家为[null]";
            if (player.name == null) return "玩家名为[null]";
            if (player.name.Length < 1) return "玩家名长度小于1";

            return null;
        }

        /// <summary>
        /// 登录, 成功返回<see langword="null"/>
        /// </summary>
        public static string Login(this Player player, string password = null)
        {
            if (player == null) return "玩家为[null]";
            if (player.name == null) return "玩家名为[null]";

            return null;
        }
    }
}
