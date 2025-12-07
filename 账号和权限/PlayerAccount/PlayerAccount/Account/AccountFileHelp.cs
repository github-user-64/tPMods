using System;
using System.Collections.Generic;
using tContentPatch.Utils;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号文件帮助
    /// </summary>
    public static class AccountFileHelp
    {
        /// <summary>
        /// 保存数据到指定位置
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void SaveData(List<Dictionary<string, string>> datas, string FilePath, bool indented = false)
        {
            MyJson1.Save(datas, FilePath, indented);
        }

        /// <summary>
        /// 从指定位置读取账号数据, 数据会检查
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static List<Dictionary<string, string>> ReadData(string FilePath)
        {
            List<Dictionary<string, string>> datas = MyJson1.Get2<List<Dictionary<string, string>>>(FilePath);

            CheckData(datas);

            return datas;
        }

        /// <summary>
        /// 清空异常数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckData(List<Dictionary<string, string>> datas) => AccountData.CheckData(datas);

        /// <summary>
        /// 更新账号数据, 失败返回<see langword="false"/>
        /// </summary>
        public static bool UpdateData() => AccountData.UpdateData();

        /// <summary>
        /// 备份账号数据, 成功返回<see langword="null"/>
        /// </summary>
        public static string BackupData()
        {
            try
            {
                AccountData.BackupData();
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
        public static string SaveData()
        {
            try
            {
                AccountData.SaveData();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
