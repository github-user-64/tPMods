using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace PlayerAccount.Account
{
    /// <summary>
    /// 玩家账号
    /// </summary>
    public static class PlayAccount
    {
        internal static List<AccountData> datas { get; private set; } = null;

        internal static void Init()
        {
            AccountHelp.UpdateData();
            if (datas == null) datas = new List<AccountData>();
        }

        internal static bool UpdateData()
        {
            List<AccountData> data = AccountHelp.ReadData();
            if (data == null) return false;
            datas = data;
            return true;
        }

        /// <summary>
        /// 获取和该名称相同的账号, 不存在返回<see langword="null"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AccountData GetNameAccount(string name)
        {
            if (name == null) return null;

            return datas.FirstOrDefault(i => i.name == name);
        }

        /// <summary>
        /// 获取和玩家名相同的账号, 玩家不存在或账号不存在返回<see langword="null"/>
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static AccountData GetPlayAccount(this Player player)
        {
            if (player == null) return null;
            if (player.name == null) return null;
            if (Main.player?.IndexInRange(player.whoAmI) != true) return null;
            if (Main.player[player.whoAmI].name != player.name) return null;

            return datas.FirstOrDefault(i => i.name == player.name);
        }

        public static void Register(string name, string password)
        {

        }
    }
}
