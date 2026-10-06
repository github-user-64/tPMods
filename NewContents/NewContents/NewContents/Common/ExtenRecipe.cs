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
                .AddIngredient(ItemID.CobaltBar, 6)//钴锭
                .AddIngredient(ItemID.GoldBar, 1)//金锭
                .AddGroup(RecipeGroups.CobaltBar)
                .AddGroup(RecipeGroups.GoldBar)
                .SetTile(TileID.TinkerersWorkbench)//工匠作坊
                .Register();

            //幸运马掌
            PatchRecipeLoader.Create(ItemID.LuckyHorseshoe, 1)
                .AddIngredient(ItemID.Wood, 1)//木材
                .AddIngredient(ItemID.GoldBar, 7)//金锭
                .AddGroup(RecipeGroups.Wood)
                .AddGroup(RecipeGroups.GoldBar)
                .SetTile(TileID.TinkerersWorkbench)//工匠作坊
                .Register();

            //闪亮红气球
            PatchRecipeLoader.Create(ItemID.ShinyRedBalloon, 1)
                .AddIngredient(ItemID.Silk, 8)//丝绸
                .AddIngredient(ItemID.WhiteString, 1)//白绳
                .SetTile(TileID.TinkerersWorkbench)//工匠作坊
                .Register();

            //云朵瓶
            PatchRecipeLoader.Create(ItemID.CloudinaBottle, 1)
                .AddIngredient(ItemID.Cloud, 1)//云
                .AddIngredient(ItemID.Bottle, 1)//玻璃瓶
                .SetTile(TileID.SkyMill)//天磨
                .Register();

            //赫尔墨斯靴
            PatchRecipeLoader.Create(ItemID.HermesBoots, 1)
                .AddIngredient(ItemID.SwiftnessPotion, 1)//敏捷药水
                .AddIngredient(ItemID.OldShoe, 1)//旧鞋
                .AddIngredient(ItemID.Silk, 2)//丝绸
                .SetTile(TileID.Loom)//织布机
                .Register();
        }
    }
}
