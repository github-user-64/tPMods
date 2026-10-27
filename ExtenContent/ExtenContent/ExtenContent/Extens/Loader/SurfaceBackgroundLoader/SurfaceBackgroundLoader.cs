using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class SurfaceBackgroundLoader
    {
        /// <summary>
        /// 背景数量, <see cref="SurfaceBackgroundID.Count"/>的数量加上<see cref="ExtenSurfaceBackground"/>的数量
        /// </summary>
        public static int BackCount { get; private set; } = SurfaceBackgroundID.Count;
        private static readonly List<ExtenSurfaceBackground> backs = new List<ExtenSurfaceBackground>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();

            backs.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(SurfaceBackgroundLoader)}:加载完成:新背景:{BackCount - SurfaceBackgroundID.Count}个", time);
        }

        internal static void UnloadPrefix()
        {
            BackCount = SurfaceBackgroundID.Count;
        }

        internal static void Unload()
        {
            Main.bgStyle = SurfaceBackgroundID.Forest1;

            backs.ForEach(i => i.Unload());
            backs.Clear();

            Main.bgStyle = SurfaceBackgroundID.Forest1;

            ContentInit();
        }

        internal static void Register(ExtenSurfaceBackground back)
        {
            backs.Add(back);
            back.Type = BackCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenSurfaceBackground"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenSurfaceBackground GetExtenBack(int type)
        {
            if (IsExten(type) != true) return null;

            return backs[type - SurfaceBackgroundID.Count];
        }

        /// <summary>
        /// <see cref="SurfaceBackgroundID"/>是否是<see cref="ExtenSurfaceBackground"/>
        /// </summary>
        public static bool IsExten(int type)
        {
            return SurfaceBackgroundID.Count <= type && type < BackCount;
        }
    }
}
