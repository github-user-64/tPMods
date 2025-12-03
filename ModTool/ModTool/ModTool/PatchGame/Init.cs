using HarmonyLib;
using tContentPatch;
using tContentPatch.Patch;

namespace ModTool.PatchGame
{
    internal class Init : Mod
    {
        internal const string patchId = "tPlainModLoader.Mod.ModTool.gamePatch";
        internal static Harmony harmony { get; private set; } = null;

        public override void AddPatch(IAddPatch addPatch)
        {
            harmony = new Harmony(patchId);
            harmony.PatchAll(ThisMod.mo.assembly);
        }

        public override void Unload()
        {
            harmony.UnpatchAll(patchId);
            harmony = null;
        }
    }
}
