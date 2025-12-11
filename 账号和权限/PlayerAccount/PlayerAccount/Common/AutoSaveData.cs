using tContentPatch;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 保存世界时保存数据
    /// </summary>
    internal class AutoSaveData : PatchWorldFile
    {
        public override void SaveWorldPostfix(bool useCloudSaving, bool resetTime)
        {
            if (Main.netMode != 2) return;

            if (Account.DataAcc.instance.NeedSave)
            {
                ContentPatch.PrintTry("保存并备份账号数据");

                string msg = Account.AccountFileHelp.BackupSaveData();

                if (msg == null) msg = "保存账号数据完成";
                else msg = $"保存账号数据失败:{msg}";

                ContentPatch.PrintTry(msg);
            }
        }
    }
}
