using PlayerAccount.Account;
using tContentPatch;
using Terraria;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 保存世界时保存数据
    /// </summary>
    internal class AutoSaveData : PatchWorldFile
    {
        public override void SaveWorldPostfix(bool useCloudSaving, bool resetTime, bool useTemps, bool canBeSkipped)
        {
            if (Main.dedServ == false) return;

            string msg = AccountFileHelp.BackupSaveData();

            if (msg == null) msg = "保存账号数据完成";
            else msg = $"保存账号数据失败:{msg}";

            ContentPatch.PrintTry(msg);
        }
    }
}
