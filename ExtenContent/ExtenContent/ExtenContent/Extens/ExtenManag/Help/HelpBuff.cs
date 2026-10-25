using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ExtenManag
    {
        #region Buff
        /// <summary>
        /// 获取<see cref="ExtenBuff.Type"/>, 不存在返回<see langword="-1"/>
        /// </summary>
        public static int BuffType<T>() where T : ExtenBuff
        {
            T instance = ExtenInstance<T>.Instance;

            return instance?.Type ?? -1;
        }

        /// <summary>
        /// <see cref="BuffID"/>是否是<see cref="ExtenBuff"/>
        /// </summary>
        public static bool IsExtenBuff(int type) => BuffLoader.IsExten(type);

        /// <summary>
        /// 获取对应的<see cref="ExtenBuff"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenBuff GetExtenBuff(int type) => BuffLoader.GetExtenBuff(type);
        #endregion
    }
}
