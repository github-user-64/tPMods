using System.Collections.Generic;


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
    }
}
