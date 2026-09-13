using HarmonyLib;
using tContentPatch;
using tContentPatch.ModLoad;
using tContentPatch.Patch;

namespace ExtenContent
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; protected set; } = null;
        private const string patchId = "tPlainModLoader.Mod.ExtenContent.gamePatch";//修补的id, 应该输入啥都行
        private Harmony harmony = null;

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
            Extens.ExtenManag.Load();
        }

        public override void Unload()
        {
            Extens.ExtenManag.Unload();

            harmony?.UnpatchAll(patchId);//卸载修补
            harmony = null;
        }
    }
}
