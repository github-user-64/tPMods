using ModTool.ServerHelp;
using ModTool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 账号帮助
    /// </summary>
    public static partial class AccountHelp
    {
        /// <summary>
        /// 在线玩家账号
        /// </summary>
        private static PlayerAccount playerAccount = null;
        /// <summary>
        /// 登录密码错误时
        /// </summary>
        public static event Action<Player> OnLoginPasswdError = null;
        /// <summary>
        /// 在注册成功时
        /// </summary>
        public static event Action<Player> OnRegistered = null;
        /// <summary>
        /// 在登录成功时
        /// </summary>
        public static event Action<Player> OnLogined = null;

        internal static void Init()
        {
            if (playerAccount != null) throw new Exception("不可重复初始化");

            playerAccount = new PlayerAccount();
        }

        /// <summary>
        /// 获取玩家数据, 数据正常ex为<see langword="null"/>
        /// </summary>
        public static (string name, string ip, int port, string uuid, string ex) GetPlayerData(this Player player)
        {
            (string name, string ip, int port, string uuid, string ex) data =
                (null, null, -1, null, null);

            Func<string, (string, string, int, string, string)> fun = ex => (null, null, -1, null, ex);

            if (player == null) return fun("玩家为[null]");

            data.name = player.name;
            if (data.name == null) return fun("名称为[null]");
            if (data.name.Length < 1) return fun("名称长度小于1");

            data.ip = player.GetIP();
            if (data.ip == null) return fun("无法获取地址");

            data.port = player.GetPort();
            if (data.port == -1) return fun("无法获取端口");

            data.uuid = player.GetUUID();
            if (data.uuid == null) return fun("无法获取uuid");

            return data;
        }

        /// <summary>
        /// 注册, 成功返回<see langword="null"/>
        /// </summary>
        public static string Register(this Player player, string password = null)
        {
            var (name, ip, port, uuid, ex) = GetPlayerData(player);
            if (ex != null) return ex;

            Dictionary<string, string> acc = GetNameAccount(player.name);
            if (acc != null) return $"{name}已注册";

            acc = new Dictionary<string, string>();

            if (acc.SetVal(AccountTag.Name, name) == false) return "设置名称失败";
            if (acc.SetVal(AccountTag.IP, ip) == false) return "设置地址失败";
            if (acc.SetVal(AccountTag.Port, port.ToString()) == false) return "设置端口失败";
            if (acc.SetVal(AccountTag.UUID, uuid) == false) return "设置uuid失败";
            if (acc.SetVal(AccountTag.Password, password) == false) return "设置密码失败";

            DataAcc.instance.datas.Insert(0, acc);
            DataAcc.instance.NeedSaveData();

            OnRegistered?.Invoke(player);

            return null;
        }

        /// <summary>
        /// 登录, 成功返回<see langword="null"/>
        /// </summary>
        public static string Login(this Player player, string password = null)
        {
            var (name, ip, port, uuid, ex) = GetPlayerData(player);
            if (ex != null) return ex;

            Dictionary<string, string> acc = GetNameAccount(player.name);
            if (acc == null) return $"{name}未注册";

            if (acc.EqualsVal(AccountTag.Name, name) == false) return "名称异常";

            if (acc.GetVal(AccountTag.Password) != password)
            {
                OnLoginPasswdError?.Invoke(player);
                return "密码错误";
            }

            bool ok = playerAccount.SetAccount(player, acc);
            if (ok == false) return "设置账号数据失败";

            acc.SetVal(AccountTag.IP, ip);
            acc.SetVal(AccountTag.Port, port.ToString());
            acc.SetVal(AccountTag.UUID, uuid);

            OnLogined?.Invoke(player);

            return null;
        }

        /// <summary>
        /// 获取该名称的账号数据, 不存在返回<see langword="null"/>
        /// </summary>
        public static Dictionary<string, string> GetNameAccount(string name)
        {
            if (name == null) return null;

            return DataAcc.instance.datas.FirstOrDefault(i => i.GetVal(AccountTag.Name, null) == name);
        }

        /// <summary>
        /// 获取已登录玩家的账号数据, 不存在返回<see langword="null"/>
        /// </summary>
        public static Dictionary<string, string> GetAccount(this Player player)
        {
            if (player == null) return null;

            return playerAccount.GetData(player.whoAmI, null);
        }
    }
}
