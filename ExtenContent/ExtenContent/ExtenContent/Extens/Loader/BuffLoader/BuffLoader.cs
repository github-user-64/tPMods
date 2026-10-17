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
            ResizeArrays();
            Setup();

            buffs.ForEach(i => i.Load());

            ThisMod.LogPrint($"{typeof(BuffLoader)}:加载完成:新buff:{BuffCount - BuffID.Count}");
        }

        internal static void Unload()
        {
            buffs.ForEach(i => i.Unload());

            BuffCount = BuffID.Count;
            buffs.Clear();

            ResizeArrays();
        }

        internal static void Register(ExtenBuff proj)
        {
            buffs.Add(proj);
            ++BuffCount;
        }

        /// <summary>
        /// 获取buffType对应的<see cref="ExtenBuff"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenBuff GetBuff(int type)
        {
            if (TypeInRange(type) != true) return null;

            return buffs[type - BuffID.Count];
        }

        /// <summary>
        /// buffType是否是<see cref="ExtenBuff"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return BuffID.Count <= type && type < BuffCount;
        }
    }
}
