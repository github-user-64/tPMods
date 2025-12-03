using Terraria;
using Terraria.DataStructures;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NPC"/>
    /// </summary>
    public class PatchNPC : tContentPatch.PatchNPC
    {
        /// <summary/>
        public delegate void NewNPCEvent(int result, IEntitySource source,
            int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target);
        /// <summary>在创建NPC后</summary>
        public static event NewNPCEvent OnNewNPCPos = null;

        /// <inheritdoc/>
        public override void NewNPCPostfix(int __result, IEntitySource source,
            int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
        {
            OnNewNPCPos?.Invoke(__result, source, X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);
        }
    }
}
