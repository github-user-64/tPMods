using System;
using System.Collections.Generic;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class MenuStyleLoader
    {
        /// <summary>
        /// 表示原版的
        /// </summary>
        public static readonly int OriginCount = 1;
        /// <summary>
        /// 样式数量
        /// </summary>
        public static int StyleCount { get; private set; } = OriginCount;
        /// <summary>
        /// 当前菜单样式, 没有为<see langword="null"/>
        /// </summary>
        public static ExtenMenuStyle CurrentStyle { get; private set; } = null;
        private static readonly List<ExtenMenuStyle> styles = new List<ExtenMenuStyle>();
        private static readonly Dictionary<string, ExtenMenuStyle> keyToStyle = new Dictionary<string, ExtenMenuStyle>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            styles.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(MenuStyleLoader)}:加载完成:新菜单样式:{StyleCount - OriginCount}个", time);
        }

        internal static void UnloadPrefix()
        {
            StyleCount = OriginCount;
        }

        internal static void Unload()
        {
            SetStyle();

            styles.ForEach(i => i.Unload());
            styles.Clear();
            keyToStyle.Clear();
        }

        internal static void Register(ExtenMenuStyle style)
        {
            styles.Add(style);
            keyToStyle[style.FullName] = style;
            style.Type = StyleCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenMenuStyle"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenMenuStyle GetExtenStyle(int type)
        {
            if (IsExten(type) != true) return null;

            return styles[type - OriginCount];
        }

        /// <summary>
        /// 是否是<see cref="ExtenMenuStyle"/>
        /// </summary>
        public static bool IsExten(int type)
        {
            return OriginCount <= type && type < StyleCount;
        }

        /// <summary>
        /// 根据<see cref="ExtenType.FullName"/>设置样式, 不存在则设为<see langword="null"/>
        /// </summary>
        public static void SetStyle(string key = null)
        {
            CurrentStyle?.OnDeselected();

            if (key == null || keyToStyle.TryGetValue(key, out ExtenMenuStyle style) != true)
            {
                CurrentStyle = null;
                return;
            }

            if (ExtenManag.IsLoaded != true) return;

            CurrentStyle = style;
            CurrentStyle.OnSelected();
        }
    }
}
