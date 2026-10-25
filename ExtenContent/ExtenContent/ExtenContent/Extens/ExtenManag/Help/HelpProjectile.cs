using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class ExtenManag
    {
        #region 射弹
        /// <summary>
        /// 获取<see cref="ExtenProjectile.Type"/>, 不存在返回<see cref="ProjectileID.None"/>
        /// </summary>
        public static short ProjectileType<T>() where T : ExtenProjectile
        {
            T instance = ExtenInstance<T>.Instance;

            return (short?)instance?.Type ?? ProjectileID.None;
        }

        /// <summary>
        /// <see cref="Projectile.type"/>是否是<see cref="ExtenProjectile"/>
        /// </summary>
        public static bool IsExtenProjectile(int type) => ProjectileLoader.IsExten(type);

        /// <summary>
        /// 获取<see cref="Projectile.type"/>对应的<see cref="ExtenProjectile"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenProjectile GetExtenProjectile(int type) => ProjectileLoader.GetExtenProj(type);
        #endregion
    }
}
