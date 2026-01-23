using ModTool.Utils;
using System;
using System.Reflection;

namespace PlayerAccount
{
    internal class CommandText : ModSettingBackup
    {
        public class Data
        {
            public string account = "账号";
            public string accountSave = "保存";
            public string accountUpdate = "更新";
            public string accountInfo = "信息";
            public string accountDel = "删除";
            public string accountTags = "设置标签";
            public string accountTagd = "删除标签";
            public string accountSetMan = "设为服主";
            public string accountSetAdmin = "设为管理员";
            public string updateConfig = "账号模组更新配置";
            public string kick = "踢出";
            public string ban = "封禁";
            public string banPly = "玩家";
            public string banAdd = "添加";
            public string banDel = "删除";
            public string banIp = "ip";
            public string banName = "名称";
            public string banAcc = "账号";
            public string noChat = "禁言";
            public string nonoChat = "取消禁言";
            public string sendToGame = "发消息";
            public string sendToGameAll = "全部";
            public string enableRegister = "启用注册";
            public string enableRegisterTrue = "开";
            public string enableRegisterFalse = "关";
            public string playing = "playing";
            public string login = "登录";
            public string register = "注册";
        }

        private static readonly Data data = new Data();
        private static CommandText instance = null;
        public static string Acc => data.account;
        public static string AccSave => data.accountSave;
        public static string AccUpdate => data.accountUpdate;
        public static string AccInfo => data.accountInfo;
        public static string AccDel => data.accountDel;
        public static string AccTags => data.accountTags;
        public static string AccTagd => data.accountTagd;
        public static string AccSetMan => data.accountSetMan;
        public static string AccSetAdmin => data.accountSetAdmin;
        public static string UpdateConfig => data.updateConfig;
        public static string Kick => data.kick;
        public static string Ban => data.ban;
        public static string BanPly => data.banPly;
        public static string BanAdd => data.banAdd;
        public static string BanDel => data.banDel;
        public static string BanIp => data.banIp;
        public static string BanName => data.banName;
        public static string BanAcc => data.banAcc;
        public static string NoChat => data.noChat;
        public static string NoNoChat => data.nonoChat;
        public static string SendToGame => data.sendToGame;
        public static string SendToGameAll => data.sendToGameAll;
        public static string EnableRegister => data.enableRegister;
        public static string EnableRegisterTrue => data.enableRegisterTrue;
        public static string EnableRegisterFalse => data.enableRegisterFalse;
        public static string Playing => data.playing;
        public static string Login => data.login;
        public static string Register => data.register;

        public override bool HasUI => false;
        public override string FilePath => "指令文本.txt";
        public override Type DataType => typeof(Data);

        public override void Load(object v)
        {
            if (instance == null) instance = this;

            if (v is Data data)
            {
                SetData(data);
            }
            else
            {
                NeedSave = true;
                Save();
            }
        }

        public override object GetSaveData() => data;

        public static void SetData(Data data)
        {
            if (data == null) return;

            FieldInfo[] fs = typeof(Data).GetFields();

            foreach (FieldInfo f in fs)
            {
                if (f.GetValue(data) is string s != true) continue;
                if (s == string.Empty) continue;

                f.SetValue(CommandText.data, s);
            }
        }

        public static bool Update()
        {
            if (instance == null) return false;

            if (instance.Read() is Data v == false) return false;

            instance.Load(v);

            return true;
        }
    }
}
