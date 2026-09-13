using ExtenContent.Extens;
using tContentPatch;
using Terraria;
using Terraria.GameContent.Items;

namespace ExtenContent.PatchGame
{
    internal class PItem : PatchItem
    {
        public override void SetDefaultsPostfix(Item This, int Type, ItemVariant variant)
        {
            ItemLoad.SetDefaults(This, Type, variant);
        }
    }
}
