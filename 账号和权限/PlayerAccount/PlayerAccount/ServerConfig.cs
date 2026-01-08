using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using ModTool.Utils;
using Newtonsoft.Json;
using System;

namespace PlayerAccount
{
    internal class ServerConfig : ModSettingBackup
    {
        public class Data
        {
            [JsonProperty("自动登录")]
            public bool AutoLogin = true;
            [JsonProperty("自动登录匹配地址")]
            public bool AutoLoginMatchIP = false;
            [JsonProperty("自动登录匹配端口")]
            public bool AutoLoginMatchPort = false;
            [JsonProperty("自动登录匹配UUID")]
            public bool AutoLoginMatchUUID = true;
            [JsonProperty("进服消息")]
            public string EnterServerMsg = null;
            [JsonProperty("封禁玩家或账号时封禁IP")]
            public bool BanIP = false;
            [JsonProperty("启用注册")]
            public bool EnableRegister = true;
            [JsonProperty("启用服务端角色")]
            public bool EnableServerSideCharacter = false;
            [JsonProperty("没登录不能操作")]
            public bool NoLoginNoAction = false;
            [JsonProperty("非管理不能修改方块")]
            public bool NoAdminNoTile = false;
        }

        public override bool HasUI => false;
        public override string FilePath => "服务器配置.txt";
        public override Type DataType => typeof(Data);
        public static Data data = null;
        private static ServerConfig instance = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is Data data)
            {
                ServerConfig.data = data;
            }
            else
            {
                ServerConfig.data = new Data();
                NeedSave = true;
                Save();
            }

            //启用服务端角色
            if (ServerConfig.data.EnableServerSideCharacter) ModTool.ServerHelp.Utils.ServerSideCharacter(true);

            if (ServerConfig.data.NoLoginNoAction) PrintTo.PrintToPlayAll("服务器已启用没登录不能操作", Color.Red);
        }

        public override object GetSaveData() => data;

        public static bool Update()
        {
            if (instance == null) return false;

            if (instance.Read() is Data v == false) return false;

            instance.Load(v);

            return true;
        }
    }
}
