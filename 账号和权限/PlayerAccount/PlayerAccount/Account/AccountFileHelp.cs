using System;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号文件帮助
    /// </summary>
    public static class AccountFileHelp
    {
        /// <summary>
        /// 更新账号数据, 失败返回<see langword="false"/>
        /// </summary>
        public static bool UpdateData() => AccountData.instance.UpdateData();

        /// <summary>
        /// 备份账号数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string BackupData()
        {
            try
            {
                return AccountData.instance.BackupSave() ? null : "备份失败";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 备份并保存数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string SaveData()
        {
            try
            {
                if (AccountData.instance.BackupSave() == false) return "备份失败";
                AccountData.instance.Save();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
