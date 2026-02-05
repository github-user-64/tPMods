using System;
using System.IO;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace BedWars
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; private set; } = null;
        public static ModConfig.Data Config { get; private set; } = null;
        public static string Dir { get; private set; } = null;
        public static string DirMapData { get; private set; } = null;

        public override void Load()
        {
            mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());
            if (mo == null) throw new Exception($"{nameof(BedWars)}:找不到模组对象");

            //

            Dir = mo.modPath;
            LoadModConfig(new ModConfig.Data());
        }

        public static void LoadModConfig(ModConfig.Data data)
        {
            Config = data ?? throw new ArgumentNullException(nameof(data), "配置为空");

            SetDirMapData(data.DirMapData);
        }

        public static void SetDirMapData(string path)
        {
            DirMapData = Path.Combine(Dir, path);
        }
    }
}
