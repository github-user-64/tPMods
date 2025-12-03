using Terraria;
using Terraria.DataStructures;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Item"/>
    /// </summary>
    public class PatchItem : tContentPatch.PatchItem
    {
        /// <summary/>
        public delegate void NewItemEvent(int result, IEntitySource source,
            int X, int Y, int Width, int Height, int Type, int Stack,
            bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup);
        /// <summary>在创建物品后</summary>
        public static event NewItemEvent OnNewItemPos = null;

        /// <inheritdoc/>
        public override void NewItemPostfix(int __result, IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
        {
            OnNewItemPos?.Invoke(__result, source, X, Y, Width, Height, Type, Stack,
                noBroadcast, pfix, noGrabDelay, reverseLookup);
        }
    }
}
