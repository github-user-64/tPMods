using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NPC"/>
    /// </summary>
    public class PatchNPC : tContentPatch.PatchNPC
    {
        /// <summary/>
        public delegate void SetDefaultsEvent(NPC This, int Type, NPCSpawnParams spawnparams);
        /// <summary>在设置默认后</summary>
        public static event SetDefaultsEvent OnSetDefaultsPos = null;

        /// <inheritdoc/>
        public override void SetDefaultsPostfix(NPC This, int Type, NPCSpawnParams spawnparams)
        {
            OnSetDefaultsPos?.Invoke(This, Type, spawnparams);
        }
    }
}
