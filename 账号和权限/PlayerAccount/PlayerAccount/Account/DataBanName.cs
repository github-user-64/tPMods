using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class DataBanName : DataABackup<List<string>>
    {
        public override string FilePath => "BanName.txt";
        public override string BackupDir => "BackupBanName";
        public override string BackupFileName => "BanName.txt";
        internal static DataBanName instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is List<string> data)
            {
                CheckData(data);
                datas = data;

                return;
            }

            ContentPatch.PrintTry("读取封禁名称失败,尝试读取备份");

            if (BackupRead() is List<string> backup)
            {
                CheckData(backup);
                datas = backup;

                return;
            }

            ContentPatch.PrintTry("读取封禁名称失败,所有数据将会清空,在备份中可能有以前数据");
            datas = new List<string>();
        }

        internal override void CheckData(List<string> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.RemoveAll(i => i == null);
        }

        public string BanAdd(string name)
        {
            if (name == null) return "名称为null";
            if (datas.Contains(name)) return "名称已存在";

            datas.Add(name);
            NeedSaveData();

            return null;
        }

        public string BanDel(string name)
        {
            if (name == null) return "名称为null";

            bool ok = datas.Remove(name);
            if (ok) instance.NeedSaveData();

            return null;
        }
    }
}
