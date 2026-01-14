using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

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
    }
}
