using HarmonyLib;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;
using tContentPatch.Patch;

namespace ExtenContentPatch
{
    /// <summary>
    /// 该模组参考了tModLoader部分代码
    /// </summary>
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; protected set; } = null;
        public static Assembly assembly { get; protected set; } = null;
        public static ExtenContent.PrivateApi.Api Api { get; protected set; }

        private const string patchId = "tPlainModLoader.Mod.ExtenContentPatch.gamePatch";//修补的id, 应该输入啥都行
        private Harmony harmony = null;

        public ThisMod()
        {
            string targetVersion = "1-beta15-t1.4.5.8";
            if (ContentPatch.VersionTPlainModLoader != targetVersion) throw new Exception($"扩展内容的目标tPlainModLoader版本为:{targetVersion}");

            LoadAssembly(out ModObject mo);
            InitAssembly(mo);
        }

        protected static void LoadAssembly(out ModObject mo)
        {
            string key = "StaticTile.ExtenContent";

            mo = ContentPatch.GetModObjects().FirstOrDefault(i => i.config.key == key);
            if (mo == null) throw new Exception($"找不到模组对象:{key}");

            assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(i => i.GetName().Name == "ExtenContent");
            if (assembly != null) return;

            assembly = Assembly.Load(File.ReadAllBytes(Path.Combine(mo.modPath, "ExtenContent.dll")));
        }

        protected static void InitAssembly(ModObject mo)
        {
            Api = (ExtenContent.PrivateApi.Api)typeof(ExtenContent.PrivateApi.Api).CreateInstance();

            Api.ExtenManag.Initialize(mo);
        }

        public override void Load(ModObject mo)
        {
            ThisMod.mo = mo;
        }

        public override void AddPatch(IAddPatch addPatch)//添加修补
        {
            harmony = new Harmony(patchId);
            harmony.PatchAll();//修补全部
        }

        public override void Loaded()
        {
            Api.ExtenManag.Load();
        }

        public override void Unload()
        {
            Api?.ExtenManag.Unload();

            harmony?.UnpatchAll(patchId);//卸载修补
            harmony = null;
        }
    }
}
