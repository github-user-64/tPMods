using Newtonsoft.Json;
using System;
using tContentPatch;

namespace PlayerAccount
{
    internal class ServerConfig : ModSetting
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
        }

        public override bool HasUI => false;
        public override string FilePath => "服务器配置.txt";
        public override Type DataType => typeof(Data);
        public static Data data = null;

        public override void Load(object v)
        {
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
        }

        public override object GetSaveData() => data;
    }
}
