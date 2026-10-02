using ExtenContent.Extens;
using Terraria;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiPatchPlayerLoader
    {
        internal ApiPatchPlayerLoader() { }

        /// <summary/>
        public void GetAdjustedItemScalePostfix(ref float result, Player player, Item item)
        {
            PatchPlayerLoader.GetAdjustedItemScalePostfix(ref result, player, item);
        }
    }
}
