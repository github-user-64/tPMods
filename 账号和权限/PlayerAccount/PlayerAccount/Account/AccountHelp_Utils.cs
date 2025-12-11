using ModTool.Utils;
using System.Collections.Generic;

namespace PlayerAccount.Account
{
    public static partial class AccountHelp
    {
        /// <summary>
        /// 获取管理等级, 账号为空或没有管理等级或无法转化为数值则返回<see langword="null"/>
        /// </summary>
        public static int? GetAdminLevel(Dictionary<string, string> acc)
        {
            if (acc == null) return null;

            string val = acc.GetVal(AccountTag.AdminLevel, null);

            if (int.TryParse(val, out int lv) == false) return null;

            return lv;
        }

        /// <summary>
        /// 判断<paramref name="acc1"/>的管理等级是否可以操作<paramref name="acc2"/>
        /// <para/><paramref name="acc1"/>&lt;=<paramref name="acc2"/>
        /// <para/>"等级越小能力越大"
        /// </summary>
        public static bool MeasureAdminLevel(Dictionary<string, string> acc1, Dictionary<string, string> acc2)
        {
            int? lv1 = GetAdminLevel(acc1);
            int? lv2 = GetAdminLevel(acc2);

            if (lv1 == null) return false;//没有管理员等级
            if (lv2 == null) return true;//被操作方没管理员等级

            return lv1 <= lv2;
        }
    }
}
