using Newtonsoft.Json;
using System;
using tContentPatch;

namespace BedWars
{
    internal class ModConfig : ModSetting
    {
        public class Data
        {
            [JsonProperty("地图数据目录")]
            public string DirMapData = "MapData";
            [JsonProperty("加入游戏需登录")]
            public bool HasLogin = true;
            [JsonProperty("调试")]
            public int IsDebug = 0;
        }

        public override bool HasUI => false;
        public override Type DataType => typeof(Data);
        public override string FilePath => "模组配置.txt";
        private static ModConfig instance = null;
        private Data data = null;

        public override void Load(object v)
        {
            instance = this;

            if (v is Data data)
            {
                this.data = data;
            }
            else
            {
                this.data = new Data();
                NeedSave = true;
                Save();
            }

            ThisMod.LoadModConfig(this.data);
        }

        public override object GetSaveData() => data;

        public static bool Update()
        {
            try
            {
                if (instance == null) return false;

                instance.Load(instance.Read());

                return true;
            }
            catch { }

            return false;
        }
    }
}
