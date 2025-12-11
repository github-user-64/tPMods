using System;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号文件帮助
    /// </summary>
    internal static class AccountFileHelp
    {
        /// <summary>
        /// 更新数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string UpdateData()
        {
            try
            {
                if (DataAcc.instance.UpdateData() == false) return "更新账号失败";
                if (DataBanIP.instance.UpdateData() == false) return "更新封禁ip失败";
                if (DataBanName.instance.UpdateData() == false) return "更新封禁名称失败";
                if (DataBanUUID.instance.UpdateData() == false) return "更新封禁uuid失败";

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 备份数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string BackupData()
        {
            try
            {
                if (DataAcc.instance.BackupSave() == false) return "备份账号失败";
                if (DataBanIP.instance.BackupSave() == false) return "备份封禁ip失败";
                if (DataBanName.instance.BackupSave() == false) return "备份封禁名称失败";
                if (DataBanUUID.instance.BackupSave() == false) return "备份封禁uuid失败";

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 备份并保存数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string BackupSaveData()
        {
            try
            {
                if (BackupData() is string exmsg) return exmsg;

                DataAcc.instance.Save();
                DataBanIP.instance.Save();
                DataBanName.instance.Save();
                DataBanUUID.instance.Save();

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
