using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ExtenManag
    {
        #region 音乐
        /// <summary>
        /// 获取<see cref="ExtenMusic.Type"/>, 不存在返回<see langword="-1"/>
        /// </summary>
        public static int MusicType<T>() where T : ExtenMusic
        {
            T instance = ExtenInstance<T>.Instance;

            return instance?.Type ?? -1;
        }

        /// <summary>
        /// <see cref="MusicID"/>是否是<see cref="ExtenMusic"/>
        /// </summary>
        public static bool IsExtenMusic(int type) => MusicLoader.TypeInRange(type);
        #endregion
    }
}
