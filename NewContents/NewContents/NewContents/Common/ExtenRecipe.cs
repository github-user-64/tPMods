using ExtenContent.Extens;
using Terraria.ID;

namespace NewContents.Common
{
    internal class ExtenRecipe : EPatchRecipe
    {
        public override void AddRecipes()
        {
            //钴护盾
            PatchRecipeLoader.Create(ItemID.CobaltShield, 1)
                .AddIngredient(ItemID.CobaltBar, 6)
                .AddIngredient(ItemID.GoldBar, 1)
                .AddGroup(RecipeGroups.CobaltBar)
                .AddGroup(RecipeGroups.GoldBar)
                .SetTile(TileID.TinkerersWorkbench)
                .Register();

            //幸运马掌
            PatchRecipeLoader.Create(ItemID.LuckyHorseshoe, 1)
                .AddIngredient(ItemID.Wood, 1)
                .AddIngredient(ItemID.GoldBar, 7)
                .AddGroup(RecipeGroups.Wood)
                .AddGroup(RecipeGroups.GoldBar)
                .SetTile(TileID.TinkerersWorkbench)
                .Register();

            //闪亮红气球
            PatchRecipeLoader.Create(ItemID.ShinyRedBalloon, 1)
                .AddIngredient(ItemID.Silk, 8)
                .AddIngredient(ItemID.WhiteString, 1)
                .SetTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
