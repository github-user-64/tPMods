using ModTool.Utils;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class AccountData : ModSettingBackup
    {
        public override bool HasUI => false;
        public override string FilePath => "AccountData.txt";
        public override string BackupDir => "BackupAcc";
        public override string BackupFileName => "AccountData.txt";
        public override Type DataType => typeof(List<Dictionary<string, string>>);
        internal static List<Dictionary<string, string>> datas { get; private set; } = null;
        internal static AccountData instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is List<Dictionary<string, string>> data)
            {
                CheckData(data);
                datas = data;

                return;
            }

            ContentPatch.PrintTry($"{nameof(AccountData)}:读取账号数据失败,尝试读取备份");

            if (BackupRead() is List<Dictionary<string, string>> backup)
            {
                CheckData(backup);
                datas = backup;

                return;
            }

            ContentPatch.PrintTry($"{nameof(AccountData)}:读取账号数据失败,所有数据将会清空,在备份中可能有以前数据");
            datas = new List<Dictionary<string, string>>();
        }

        public override object GetSaveData() => datas;

        /// <summary>
        /// 更新账号数据, 失败返回<see langword="false"/>
        /// </summary>
        internal bool UpdateData()
        {
            if (Read() is List<Dictionary<string, string>> data == false) return false;
            CheckData(data);

            datas = data;

            return true;
        }

        /// <summary>
        /// 清空异常数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void CheckData(List<Dictionary<string, string>> datas)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));

            datas.RemoveAll(i =>
            i == null ||
            i.GetVal(AccountTag.Name, null) == null);
        }
    }
}
