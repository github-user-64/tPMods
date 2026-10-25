using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ExtenManag
    {
        #region SurfaceBackground
        /// <summary>
        /// 获取<see cref="ExtenSurfaceBackground.Type"/>, 不存在返回<see langword="-1"/>
        /// </summary>
        public static int BackType<T>() where T : ExtenSurfaceBackground
        {
            T instance = ExtenInstance<T>.Instance;

            return instance?.Type ?? -1;
        }

        /// <summary>
        /// <see cref="SurfaceBackgroundID"/>是否是<see cref="ExtenSurfaceBackground"/>
        /// </summary>
        public static bool IsExtenBack(int type) => SurfaceBackgroundLoader.IsExten(type);
        #endregion
    }
}
