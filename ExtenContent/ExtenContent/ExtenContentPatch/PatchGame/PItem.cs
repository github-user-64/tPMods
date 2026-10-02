using tContentPatch;
using Terraria;
using Terraria.GameContent.Items;

namespace ExtenContentPatch.PatchGame
{
    internal class PItem : PatchItem
    {
        public override void SetDefaultsPostfix(Item This, int Type, ItemVariant variant)
        {
            ThisMod.Api.Item.SetDefaults(This, Type, variant);
        }
    }
}
