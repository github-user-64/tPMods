using Terraria;

namespace ExtenContent.Extens
{
    public static partial class PatchPlayerLoader
    {
        internal static void GetAdjustedItemScalePostfix(ref float result, Player player, Item item)
        {
            foreach (EPatchPlayer patch in patchs)
            {
                patch.GetAdjustedItemScalePostfix(ref result, player, item);
            }
        }
    }
}
