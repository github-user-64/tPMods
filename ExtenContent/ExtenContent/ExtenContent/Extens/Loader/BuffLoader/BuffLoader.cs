using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class BuffLoader
    {
        private static readonly FieldInfo __buffNameCache = typeof(Lang).GetField("_buffNameCache", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo __buffDescriptionCache = typeof(Lang).GetField("_buffDescriptionCache", BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        /// buff数量, <see cref="BuffID.Count"/>的数量加上<see cref="ExtenBuff"/>的数量
        /// </summary>
        public static int BuffCount { get; private set; } = BuffID.Count;
        private static readonly List<ExtenBuff> buffs = new List<ExtenBuff>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();
            ContentLoad();

            buffs.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(BuffLoader)}:加载完成:新buff:{BuffCount - BuffID.Count}个", time);
        }

        internal static void UnloadPrefix()
        {
            BuffCount = BuffID.Count;
        }

        internal static void Unload()
        {
            buffs.ForEach(i => i.Unload());
            buffs.Clear();

            ContentInit();
        }

        internal static void Register(ExtenBuff eb)
        {
            buffs.Add(eb);
            eb.Type = BuffCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenBuff"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenBuff GetExtenBuff(int type)
        {
            if (TypeInRange(type) != true) return null;

            return buffs[type - BuffID.Count];
        }

        /// <summary>
        /// <see cref="BuffID"/>是否是<see cref="ExtenBuff"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return BuffID.Count <= type && type < BuffCount;
        }
    }
}
