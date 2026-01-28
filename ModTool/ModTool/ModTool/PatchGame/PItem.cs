using System.Diagnostics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Items;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Item"/>
    /// </summary>
    public class PItem : tContentPatch.PatchItem
    {
        /// <summary/>
        public delegate void NewItemEvent(int result, IEntitySource source,
            int X, int Y, int Width, int Height, int Type, int Stack,
            bool noBroadcast, int pfix, bool noGrabDelay);
        /// <summary>
        /// 在创建物品后
        /// </summary>
        public static event NewItemEvent OnNewItemPo = null;

        /// <inheritdoc/>
        public override void NewItemPostfix(int __result, IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay)
        {
            OnNewItemPo?.Invoke(__result, source, X, Y, Width, Height, Type, Stack,
                noBroadcast, pfix, noGrabDelay);
        }

        /// <summary/>
        public delegate void SetDefaultsEvent(Item This, int Type, ItemVariant variant);
        /// <summary>在设置默认前</summary>
        public static event SetDefaultsEvent OnSetDefaults = null;

        /// <inheritdoc/>
        public override void SetDefaultsPrefix(Item This, int Type, ItemVariant variant)
        {
            OnSetDefaults?.Invoke(This, Type, variant);
        }
    }
}
