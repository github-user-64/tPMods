using ModTool.EntityTag;
using ModTool.ServerHelp;
using ModTool.Utils;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Account
{
    public static partial class AccountHelp
    {
        /// <summary>
        /// 添加封禁名称
        /// </summary>
        public static string BanAddName(string name)
        {
            return DataBanName.instance.BanAdd(name);
        }

        /// <summary>
        /// 删除封禁名称
        /// </summary>
        public static string BanDelName(string name)
        {
            return DataBanName.instance.BanDel(name);
        }

        /// <summary>
        /// 添加封禁uuid
        /// </summary>
        public static string BanAddUUID(string uuid)
        {
            return DataBanUUID.instance.BanAdd(uuid);
        }

        /// <summary>
        /// 删除封禁uuid
        /// </summary>
        public static string BanDelUUID(string uuid)
        {
            return DataBanUUID.instance.BanDel(uuid);
        }

        /// <summary>
        /// 添加封禁ip
        /// </summary>
        public static string BanAddIP(string ip, string port = null)
        {
            return DataBanIP.instance.BanAdd(ip, port);
        }

        /// <summary>
        /// 删除封禁ip
        /// </summary>
        public static string BanDelIP(string ip, string port = null)
        {
            return DataBanIP.instance.BanDel(ip, port);
        }

        /// <summary>
        /// 封禁在线玩家
        /// </summary>
        public static string BanAddPlayer(this Player player, string msg = null)
        {
            if (player == null) return "玩家为null";
            if (player.active == false) return "玩家不在线";

            BanAddName(player.name);
            BanAddUUID(player.GetUUID());

            if (ServerConfig.data.BanIP)
            {
                string port = player.GetPort().ToString();
                if (port == "-1") port = null;

                BanAddIP(player.GetIP(), port);
            }

            //player.GetAccount().SetVal(AccountTag.Ban, msg);//不用这个防止在封禁前踢出玩家导致获取不到在线账号
            GetNameAccount(player.name).SetVal(AccountTag.Ban, msg);
            DataAcc.instance.NeedSaveData();

            return null;
        }

        /// <summary>
        /// 添加名称为<paramref name="accName"/>账号的封禁
        /// </summary>
        public static string BanAddAccName(string accName, string msg = null)
        {
            if (accName == null) return "账号名称为null";

            Dictionary<string, string> acc = GetNameAccount(accName);
            if (acc == null) return "账号不存在";

            BanAddName(acc.GetVal(AccountTag.Name));
            BanAddUUID(acc.GetVal(AccountTag.UUID));

            if (ServerConfig.data.BanIP)
            {
                BanAddIP(acc.GetVal(AccountTag.IP), acc.GetVal(AccountTag.Port));
            }

            acc.SetVal(AccountTag.Ban, msg);
            DataAcc.instance.NeedSaveData();

            return null;
        }

        /// <summary>
        /// 删除名称为<paramref name="accName"/>账号的封禁
        /// </summary>
        public static string BanDelAccName(string accName)
        {
            if (accName == null) return "账号名称为null";

            Dictionary<string, string> acc = GetNameAccount(accName);
            if (acc == null) return "账号不存在";

            BanDelName(acc.GetVal(AccountTag.Name));
            BanDelUUID(acc.GetVal(AccountTag.UUID));

            BanDelIP(acc.GetVal(AccountTag.IP), acc.GetVal(AccountTag.Port));

            acc.DelKey(AccountTag.Ban);
            DataAcc.instance.NeedSaveData();

            return null;
        }
    }
}
