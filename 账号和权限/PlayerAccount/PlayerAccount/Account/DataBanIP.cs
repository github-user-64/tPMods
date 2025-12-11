using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerAccount.Account
{
    internal class DataBanIP : DataABackup<List<(string IP, string Port)>>
    {
        public override string FilePath => "BanIP.txt";
        public override string BackupDir => "BackupBanIP";
        public override string BackupFileName => "BanIP.txt";
        internal static DataBanIP instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is List<(string IP, string Port)> data)
            {
                CheckData(data);
                datas = data;

                return;
            }

            ContentPatch.PrintTry("读取封禁ip失败,尝试读取备份");

            if (BackupRead() is List<(string IP, string Port)> backup)
            {
                CheckData(backup);
                datas = backup;

                return;
            }

            ContentPatch.PrintTry("读取封禁ip失败,所有数据将会清空,在备份中可能有以前数据");
            datas = new List<(string IP, string Port)>();
        }

        internal override void CheckData(List<(string IP, string Port)> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.RemoveAll(i => i.IP == null);
        }

        public string BanAdd(string ip, string port = null)
        {
            if (ip == null) return "ip为null";
            if (datas.Exists(i => i.IP == ip && i.Port == port)) return "ip已存在";

            datas.Add((ip, port));
            NeedSaveData();

            return null;
        }

        public string BanDel(string ip, string port = null)
        {
            if (ip == null) return "ip为null";

            int ok = datas.RemoveAll(i => i.IP == ip && i.Port == port);
            if (ok > 0) NeedSaveData();

            return null;
        }
    }
}
