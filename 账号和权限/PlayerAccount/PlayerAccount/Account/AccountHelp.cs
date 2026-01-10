using Microsoft.Xna.Framework;
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
        /// 登录密码错误时
        /// </summary>
        public static event Action<Player> OnLoginPasswdError = null;
        /// <summary>
        /// 在玩家注册成功时
        /// </summary>
        public static event Action<Player> OnRegisterPlayered = null;
        /// <summary>
        /// 在登录成功时
        /// </summary>
        public static event Action<Player> OnLogined = null;
        /// <summary>
        /// 在加入游戏后, 不管是否自动登录都会触发, 自动登录前
        /// </summary>
        public static event Action<int> OnJoinGamePr = null;
        /// <summary>
        /// 在加入游戏后, 不管是否自动登录都会触发, 自动登录后
        /// </summary>
        public static event Action<int> OnJoinGamePo = null;
        /// <summary>
        /// 在账号更新时
        /// </summary>
        public static event Action OnUpdateAccount = null;
        /// <summary>
        /// 在线玩家账号
        /// </summary>
        private static PlayerAccount playerAccount = null;

        internal static void Init()
        {
            if (playerAccount != null) throw new Exception("不可重复初始化");

            playerAccount = new PlayerAccount();
        }

        internal static void HandJoinGamePr(int ply) => OnJoinGamePr?.Invoke(ply);
        internal static void HandJoinGamePo(int ply) => OnJoinGamePo?.Invoke(ply);

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
        /// 玩家注册账号, 成功返回<see langword="null"/>
        /// </summary>
        public static string RegisterPlayer(this Player player, string password = null)
        {
            var (name, ip, port, uuid, ex) = GetPlayerData(player);
            if (ex != null) return ex;

            string exMsg = Register(name, password, out Dictionary<string, string> regOkAcc);
            if (exMsg != null) return exMsg;

            regOkAcc.SetVal(AccountTag.IP, ip);
            regOkAcc.SetVal(AccountTag.Port, port.ToString());
            regOkAcc.SetVal(AccountTag.UUID, uuid);

            OnRegisterPlayered?.Invoke(player);

            return null;
        }

        /// <summary>
        /// 添加账号, 成功返回<see langword="null"/>, 成功<paramref name="regOkAcc"/>为添加的账号
        /// </summary>
        public static string Register(string name, string password, out Dictionary<string, string> regOkAcc)
        {
            regOkAcc = null;

            if (name == null) return "名称为null";
            if (password == null) return "密码为null";

            Dictionary<string, string> acc = GetNameAccount(name);
            if (acc != null) return $"{name}已注册";

            acc = new Dictionary<string, string>();

            if (acc.SetVal(AccountTag.Name, name) == false) return "设置名称失败";
            if (acc.SetVal(AccountTag.Password, password) == false) return "设置密码失败";

            DataAcc.instance.datas.Insert(0, acc);
            regOkAcc = acc;

            DataAcc.instance.NeedSaveData();

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

            //移动到最前
            DataAcc.instance.datas.Remove(acc);
            DataAcc.instance.datas.Insert(0, acc);

            OnLogined?.Invoke(player);

            return null;
        }

        /// <summary>
        /// 删除账号, 有账号被删除返回<see langword="true"/>
        /// </summary>
        public static bool DelAccount(Dictionary<string, string> acc)
        {
            bool ok = DataAcc.instance.datas.Remove(acc);
            if (ok) DataAcc.instance.NeedSaveData();

            return ok;
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
        public static Dictionary<string, string> GetAccount(int whoAmI)
        {
            return playerAccount.GetData(whoAmI, null);
        }

        /// <summary>
        /// 获取已登录玩家的账号数据, 不存在返回<see langword="null"/>
        /// </summary>
        public static Dictionary<string, string> GetAccount(this Player player)
        {
            if (player == null) return null;

            return GetAccount(player.whoAmI);
        }

        /// <summary>
        /// 更新在线玩家账号数据, 只更新已登录玩家账号
        /// </summary>
        public static void UpdateAccount()
        {
            for (int i = 0; i < Main.player.Length; ++i)
            {
                Player player = Main.player[i];
                if (player?.active != true) return;

                Dictionary<string, string> oldAcc = GetAccount(i);

                if (oldAcc == null) continue;

                Dictionary<string, string> newAcc = GetNameAccount(oldAcc.GetVal(AccountTag.Name));

                playerAccount.SetAccount(player, newAcc);
                PrintTo.PrintToPlay(i, "你的账号已变更", new Color(0f, 1f, 1f));
            }

            OnUpdateAccount?.Invoke();
        }
    }
}
