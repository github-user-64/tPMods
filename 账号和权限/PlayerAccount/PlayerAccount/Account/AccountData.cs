namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号数据
    /// </summary>
    public class AccountData
    {
        /// <summary>
        /// 账号数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public AccountData(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        /// <summary>
        /// 玩家名
        /// </summary>
        public string name = null;
        /// <summary>
        /// 密码
        /// </summary>
        public string password = null;
    }
}
