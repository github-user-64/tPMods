using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class ProjectileLoader
    {
        private static readonly FieldInfo __projectileNameCache = typeof(Lang).GetField("_projectileNameCache", BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        /// 物品数量, <see cref="ProjectileID.Count"/>的数量加上<see cref="ExtenProjectile"/>的数量
        /// </summary>
        public static int ProjectileCount { get; private set; } = ProjectileID.Count;
        private static readonly List<ExtenProjectile> projs = new List<ExtenProjectile>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();
            ContentLoad();

            projs.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(ProjectileLoader)}:加载完成:新射弹:{ProjectileCount - ProjectileID.Count}个", time);
        }

        internal static void Unload()
        {
            ProjectileCount = ProjectileID.Count;

            projs.ForEach(i => i.Unload());
            projs.Clear();

            ContentInit();
        }

        internal static void Register(ExtenProjectile proj)
        {
            projs.Add(proj);
            proj.Projectile.type = ProjectileCount++;
        }

        /// <summary>
        /// 获取<see cref="Projectile.type"/>对应的<see cref="ExtenProjectile"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenProjectile GetExtenProj(int type)
        {
            if (IsExten(type) != true) return null;

            return projs[type - ProjectileID.Count];
        }

        /// <summary>
        /// <see cref="Projectile.type"/>是否是<see cref="ExtenProjectile"/>
        /// </summary>
        public static bool IsExten(int type)
        {
            return ProjectileID.Count <= type && type < ProjectileCount;
        }
    }
}
