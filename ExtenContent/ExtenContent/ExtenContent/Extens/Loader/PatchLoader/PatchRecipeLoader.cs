using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 合成表加载器
    /// </summary>
    public static class PatchRecipeLoader
    {
        private static readonly List<EPatchRecipe> PatchRecipes = new List<EPatchRecipe>();

        internal static void Load()
        {
            Reset();

            PatchRecipes.ForEach(i => i.AddRecipes());

            for (int i = ItemID.Count; i < ItemLoader.ItemCount; ++i)
            {
                ExtenManag.GetExtenItem(i)?.AddRecipes();
            }

            Recipe.SetupRecipes();
        }

        internal static void Unload()
        {
            PatchRecipes.Clear();

            Reset();

            Recipe.SetupRecipes();
        }

        internal static void Register(EPatchRecipe er)
        {
            PatchRecipes.Add(er);
        }

        private static void Reset()
        {
            for (int i = 0; i < Recipe.maxRecipes; ++i)
            {
                Main.recipe[i] = new Recipe();
            }
            Recipe.numRecipes = 0;
        }

        /// <summary>
        /// 获取一个配方对象
        /// </summary>
        public static Recipe Create(int type, int stack = 1)
        {
            Recipe recipe = new Recipe();
            recipe.createItem.SetDefaults(type);
            recipe.createItem.stack = stack;
            return recipe;
        }

        /// <summary>
        /// 将配方注册到游戏
        /// </summary>
        public static Recipe Register(this Recipe recipe)
        {
            if (recipe.createItem == null || recipe.createItem.type <= ItemID.None)
            {
                throw new Exception("创建合成表失败:合成物品不存在");
            }

            if (Recipe.numRecipes >= Recipe.maxRecipes)
            {
                Recipe.maxRecipes += 500;
                Array.Resize(ref Main.recipe, Recipe.maxRecipes);
                Array.Resize(ref Main.availableRecipe, Recipe.maxRecipes);
                Array.Resize(ref CraftingUI.availableRecipeY, Recipe.maxRecipes);

                for (int i = Recipe.numRecipes; i < Recipe.maxRecipes; ++i)
                {
                    Main.recipe[i] = new Recipe();
                    CraftingUI.availableRecipeY[i] = 65f * i;
                }
            }

            Main.recipe[Recipe.numRecipes] = recipe;
            Recipe.numRecipes++;

            return recipe;
        }

        /// <summary>
        /// 给配方添加所需材料
        /// </summary>
        public static Recipe AddIngredient(this Recipe recipe, int type, int stack = 1)
        {
            //Recipe.UpdateMaterialFieldForAllRecipes
            //在遍历requiredItem做Refresh时
            //没有做长度判断, 如果所有物品type>0就会一直便利下去, 直到超出索引
            int len = recipe.requiredItem.Length - 1;

            for (int i = 0; i < len; ++i)
            {
                if (recipe.requiredItem[i].IsAir != true) continue;

                recipe.requiredItem[i].SetDefaults(type);
                recipe.requiredItem[i].stack = stack;

                break;
            }
            return recipe;
        }

        /// <summary>
        /// 给配方所需材料设为同类型即可<br/>
        /// 木材->任意木材
        /// </summary>
        public static Recipe AddGroup(this Recipe recipe, RecipeGroup group)
        {
            recipe.RequireGroup(group);
            return recipe;
        }

        /// <summary>
        /// 设置制作站<see cref="TileID"/>
        /// </summary>
        public static Recipe SetTile(this Recipe recipe, int type)
        {
            recipe.requiredTile = type;
            return recipe;
        }
    }
}
