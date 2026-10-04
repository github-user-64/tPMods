using System.Collections.Generic;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static class PatchPlayerLoader
    {
        private static readonly List<EPatchPlayer> patchs = new List<EPatchPlayer>();

        internal static void Load()
        {
            patchs.ForEach(i => i.Load());
        }

        internal static void Unload()
        {
            patchs.ForEach(i => i.Unload());

            patchs.Clear();
        }

        internal static void Register(EPatchPlayer patch)
        {
            patchs.Add(patch);
        }

        internal static void GetAdjustedItemScalePostfix(ref float result, Player player, Item item)
        {
            foreach (EPatchPlayer patch in patchs)
            {
                patch.GetAdjustedItemScalePostfix(ref result, player, item);
            }
        }
    }
}
