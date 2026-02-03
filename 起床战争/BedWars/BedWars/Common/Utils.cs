using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace BedWars.Common
{
    public static class Utils
    {
        /// <summary>
        /// 获取随机物品id, 排除<paramref name="exclude"/>, 如果不存在返回0
        /// </summary>
        public static int GetRandItemID(List<int> exclude = null)
        {
            int index = ModTool.Utils.Utils.GetRand(1, ItemID.Count);

            for (int i = index; ;)
            {
                if (
                    ItemID.Sets.Deprecated[i] ||//已弃用
                    exclude?.Contains(i) == true//排除
                    )
                {
                    ++i;
                    if (i < ItemID.Count == false) i = 1;//到结尾就从头开始
                    if (i == index) return 0;//如果绕一圈回来了
                    continue;
                }

                return i;
            }
        }

        /// <summary>
        /// 获取范围内的项
        /// </summary>
        public static List<(T, int)> GetInRange<T>(Rectangle rect, T[] arr, Func<T, Point> getp)
        {
            List<(T, int)> list = new List<(T, int)>();

            if (rect.Width < 1) return list;
            if (rect.Height < 1) return list;

            for (int i = 0; i < arr.Length; ++i)
            {
                T item = arr[i];
                if (item == null) continue;

                if (rect.Contains(getp(item)) == false) continue;

                list.Add((item, i));
            }

            return list;
        }

        /// <summary>
        /// 获取范围内的箱子, 位置是世界位置
        /// </summary>
        public static List<(Chest, int)> GetInRangeChest(Rectangle rect)
        {
            return GetInRange(rect, Main.chest, i => new Point(i.x, i.y));
        }

        /// <summary>
        /// 获取范围内的告示牌, 位置是世界位置
        /// </summary>
        public static List<(Sign, int)> GetInRangeSign(Rectangle rect)
        {
            return GetInRange(rect, Main.sign, i => new Point(i.x, i.y));
        }

        /// <summary>
        /// 清除范围内的箱子, 位置是世界位置
        /// </summary>
        public static void ClearInRangeChest(Rectangle rect)
        {
            List<(Chest, int)> list = GetInRangeChest(rect);

            list.ForEach(i =>
            {
                int x = i.Item1.x;
                int y = i.Item1.y;
                Chest.DestroyChestDirect(x, y, i.Item2);
            });
        }

        /// <summary>
        /// 清除范围内的告示牌, 位置是世界位置
        /// </summary>
        public static void ClearInRangeSign(Rectangle rect)
        {
            List<(Sign, int)> list = GetInRangeSign(rect);

            list.ForEach(i =>
            {
                Sign.KillSign(i.Item1.x, i.Item1.y);
            });
        }

        //WeaponsMelee               武器 近战
        //WeaponsRanged              武器 远程
        //WeaponsMagic               武器 魔法
        //WeaponsMinions             武器 仆从
        //WeaponsAssorted            武器 混合
        //WeaponsAmmo                武器 弹药
        //ToolsPicksaws              工具 钻锯
        //ToolsHamaxes               工具 锤斧
        //ToolsPickaxes              工具 镐子
        //ToolsAxes                  工具 斧头
        //ToolsHammers               工具 锤子
        //ToolsTerraforming          工具 地形改造
        //ToolsFishing               工具 钓鱼
        //ToolsGolf                  工具 高尔夫
        //ToolsInstruments           工具 乐器
        //ToolsKeys                  工具 钥匙
        //ToolsKites                 工具 风筝
        //ToolsAmmoLeftovers         工具 剩余弹药
        //ToolsMisc                  工具 杂项
        //ArmorCombat                护甲 战斗
        //ArmorVanity                护甲 虚荣
        //ArmorAccessories           护甲 配饰
        //EquipGrapple               装备 抓钩
        //EquipMount                 装备 坐骑
        //EquipCart                  装备 小车
        //EquipLightPet              装备 光宠物
        //EquipVanityPet             装备 虚荣宠物
        //PotionsDyes                药水 染料
        //PotionsHairDyes            药水 染发剂
        //PotionsLife                药水 生命
        //PotionsJustTheMushroom     药水 仅蘑菇
        //PotionsMana                药水 魔力
        //PotionsElixirs             药水 灵药
        //PotionsBuffs               药水 增益
        //PotionsFood                药水 食物
        //MiscValuables              杂项 贵重物品
        //MiscPainting               杂项 画作
        //MiscWiring                 杂项 电线
        //MiscMaterials              杂项 材料
        //MiscJustTheGlowingMushroom 杂项 仅发光蘑菇
        //MiscRopes                  杂项 绳索
        //MiscHerbsAndSeeds          杂项 草药和种子
        //MiscAcorns                 杂项 橡子
        //MiscGems                   杂项 宝石
        //MiscBossBags               杂项 首领宝袋
        //MiscCritters               杂项 小动物
        //MiscExtractinator          杂项 提取器
        //LastMaterials              最后 材料
        //LastTilesImportant         最后 重要瓷砖
        //LastTilesCommon            最后 常见瓷砖
        //LastNotTrash               最后 非垃圾
        //LastTrash                  最后 垃圾
        public static int GetMaxStack(Item item = null)
        {
            if (item == null) return 0;
            if (item.maxStack < 1) return 0;
            if (ItemSorting.GetSortingLayer(item.type).InFirstString("Weapons")) return 1;
            if (ItemSorting.GetSortingLayer(item.type).InFirstString("Tools")) return 1;
            if (ItemSorting.GetSortingLayer(item.type).InFirstString("Armor")) return 1;
            if (ItemSorting.GetSortingLayer(item.type).InFirstString("Equip")) return 1;

            return item.maxStack;
        }

        public static bool InFirstString(this string s1, string s2)
        {
            if (s1 == null || s2 == null) return false;
            if (s1.Length < 1 || s2.Length < 1) return false;
            if (s1.Length < s2.Length) return false;
            for (int i = 0; i < s2.Length; ++i)
            {
                if (s1[i] != s2[i]) return false;
            }
            return true;
        }
    }
}
