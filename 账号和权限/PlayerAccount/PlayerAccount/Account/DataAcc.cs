using ModTool.Utils;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class DataAcc : DataABackup<List<Dictionary<string, string>>>
    {
        public override string FilePath => "AccountData.txt";
        public override string BackupDir => "BackupAcc";
        public override string BackupFileName => "AccountData.txt";
        internal static DataAcc instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is List<Dictionary<string, string>> data)
            {
                CheckData(data);
                datas = data;

                return;
            }

            ContentPatch.PrintTry($"{nameof(DataAcc)}:读取账号数据失败,尝试读取备份");

            if (BackupRead() is List<Dictionary<string, string>> backup)
            {
                CheckData(backup);
                datas = backup;

                return;
            }

            ContentPatch.PrintTry($"{nameof(DataAcc)}:读取账号数据失败,所有数据将会清空,在备份中可能有以前数据");
            datas = new List<Dictionary<string, string>>();
        }

        public override void CheckData(List<Dictionary<string, string>> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.RemoveAll(i =>
            i == null ||
            i.GetVal(AccountTag.Name, null) == null);
        }
    }
}
