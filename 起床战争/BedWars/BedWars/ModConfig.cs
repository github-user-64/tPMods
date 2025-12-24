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
        }

        public override bool HasUI => false;
        public override Type DataType => typeof(Data);
        public override string FilePath => "模组配置.txt";
        private Data data = null;

        public override void Load(object v)
        {
            if (v is Data data == false)
            {
                data = new Data();
                NeedSave = true;
                Save();
            }

            this.data = data;
            ThisMod.LoadModConfig(this.data);
        }

        public override object GetSaveData() => data;
    }
}
