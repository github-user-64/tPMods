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

        public static bool InWorld(Vector2 pos, float fluff = 0)
        {
            if (pos.X < fluff || pos.X >= Main.maxTilesX * 16 - fluff ||
                pos.Y < fluff || pos.Y >= Main.maxTilesY * 16 - fluff)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取范围内的项
        /// </summary>
        public static List<(T, int)> GetInRange<T>(Point pos, Point size, T[] arr, Func<T, Point> getp)
        {
            List<(T, int)> list = new List<(T, int)>();

            if (size.X < 1) return list;
            if (size.Y < 1) return list;
            int endx = pos.X + size.X - 1;
            int endy = pos.Y + size.Y - 1;

            for (int i = 0; i < arr.Length; ++i)
            {
                T item = arr[i];

                if (item == null) continue;
                Point p = getp(item);

                if (p.X < pos.X) continue;
                if (p.Y < pos.Y) continue;
                if (p.X > endx) continue;
                if (p.Y > endy) continue;

                list.Add((item, i));
            }

            return list;
        }

        /// <summary>
        /// 获取范围内的箱子, 位置是世界位置
        /// </summary>
        public static List<(Chest, int)> GetInRangeChest(Point pos, Point size)
        {
            return GetInRange(pos, size, Main.chest, i => new Point(i.x, i.y));
        }

        /// <summary>
        /// 获取范围内的告示牌, 位置是世界位置
        /// </summary>
        public static List<(Sign, int)> GetInRangeSign(Point pos, Point size)
        {
            return GetInRange(pos, size, Main.sign, i => new Point(i.x, i.y));
        }

        /// <summary>
        /// 清除范围内的箱子, 位置是世界位置
        /// </summary>
        public static void ClearInRangeChest(Point pos, Point size)
        {
            List<(Chest, int)> list = GetInRangeChest(pos, size);

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
        public static void ClearInRangeSign(Point pos, Point size)
        {
            List<(Sign, int)> list = GetInRangeSign(pos, size);

            list.ForEach(i =>
            {
                Sign.KillSign(i.Item1.x, i.Item1.y);
            });
        }
    }
}
