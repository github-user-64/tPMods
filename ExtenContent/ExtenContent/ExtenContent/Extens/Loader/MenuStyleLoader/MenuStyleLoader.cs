using System;
using System.Collections.Generic;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class MenuStyleLoader
    {
        /// <summary/>
        public static readonly int OriginCount = 1;
        /// <summary>
        /// 样式数量
        /// </summary>
        public static int StyleCount { get; private set; } = OriginCount;
        /// <summary>
        /// 当前菜单样式, 没有为<see langword="null"/>
        /// </summary>
        public static ExtenMenuStyle CurrentStyle { get; set; } = null;
        private static readonly List<ExtenMenuStyle> styles = new List<ExtenMenuStyle>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            styles.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(MenuStyleLoader)}:加载完成:新菜单样式:{StyleCount - OriginCount}个", time);
        }

        internal static void Unload()
        {
            CurrentStyle = null;

            styles.ForEach(i => i.Unload());

            CurrentStyle = null;

            StyleCount = OriginCount;
            styles.Clear();
        }

        internal static void Register(ExtenMenuStyle style)
        {
            styles.Add(style);
            style.Type = StyleCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenMenuStyle"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenMenuStyle GetStyle(int type)
        {
            if (TypeInRange(type) != true) return null;

            return styles[type - OriginCount];
        }

        /// <summary>
        /// 是否是<see cref="ExtenMenuStyle"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return OriginCount <= type && type < StyleCount;
        }
    }
}
