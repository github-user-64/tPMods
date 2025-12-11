using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class DataBanUUID : DataABackup<List<string>>
    {
        public override string FilePath => "BanUUID.txt";
        public override string BackupDir => "BackupBanUUID";
        public override string BackupFileName => "BanUUID.txt";
        internal static DataBanUUID instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is List<string> data)
            {
                CheckData(data);
                datas = data;

                return;
            }

            ContentPatch.PrintTry("读取封禁uuid失败,尝试读取备份");

            if (BackupRead() is List<string> backup)
            {
                CheckData(backup);
                datas = backup;

                return;
            }

            ContentPatch.PrintTry("读取封禁uuid失败,所有数据将会清空,在备份中可能有以前数据");
            datas = new List<string>();
        }

        internal override void CheckData(List<string> data)
        {
            if (datas == null) throw new ArgumentNullException(nameof(data));

            datas.RemoveAll(i => i == null);
        }

        public string BanAdd(string uuid)
        {
            if (uuid == null) return "uuid为null";
            if (datas.Contains(uuid)) return "uuid已存在";

            datas.Add(uuid);
            NeedSaveData();

            return null;
        }

        public string BanDel(string uuid)
        {
            if (uuid == null) return "uuid为null"; ;

            bool ok = datas.Remove(uuid);
            if (ok) NeedSaveData();

            return null;
        }
    }
}
