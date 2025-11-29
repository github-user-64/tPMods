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
        /// <param name="uuid"></param>
        /// <param name="ip"></param>
        public AccountData(string name, string password, string uuid, string ip)
        {
            this.name = name;
            this.password = password;
            this.uuid = uuid;
            this.ip = ip;
        }

        /// <summary>
        /// 玩家名
        /// </summary>
        public string name = null;
        /// <summary>
        /// 密码
        /// </summary>
        public string password = null;
        /// <summary>
        /// uuid
        /// </summary>
        public string uuid = null;
        /// <summary>
        /// ui
        /// </summary>
        public string ip = null;
    }
}
