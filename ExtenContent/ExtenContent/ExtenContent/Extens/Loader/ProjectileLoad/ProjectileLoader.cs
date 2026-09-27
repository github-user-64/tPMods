using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class ProjectileLoad
    {
        private static readonly FieldInfo __projectileNameCache = typeof(Lang).GetField("_projectileNameCache", BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        /// 物品数量, <see cref="ProjectileID.Count"/>的数量加上<see cref="ExtenProjectile"/>的数量
        /// </summary>
        public static int ProjectileCount { get; private set; } = ProjectileID.Count;
        private static readonly List<ExtenProjectile> projs = new List<ExtenProjectile>();

        internal static void Load()
        {
            ResizeArrays();
            Setup();

            projs.ForEach(i => i.Load());
        }

        internal static void Unload()
        {
            projs.ForEach(i => i.Unload());

            ProjectileCount = ProjectileID.Count;
            projs.Clear();
        }

        internal static void Register(ExtenProjectile proj)
        {
            projs.Add(proj);
            ++ProjectileCount;
        }

        private static void Setup()
        {
            LocalizedText[] _projectileNameCache = (LocalizedText[])__projectileNameCache.GetValue(null);

            for (int i = 0; i < projs.Count; ++i)
            {
                ExtenProjectile ep = projs[i];
                ep.Projectile.SetDefaults(ProjectileID.Count + i);

                TextureAssets.Projectile[ep.Type] = ep.Asset.Request<Texture2D>(ep.Texture);
                _projectileNameCache[ep.Type] = ep.DisplayName;
            }

            __projectileNameCache.SetValue(null, _projectileNameCache);
        }

        /// <summary>
        /// 获取<see cref="Projectile.type"/>对应的<see cref="ExtenProjectile"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenProjectile GetProj(int type)
        {
            if (TypeInRange(type) != true) return null;

            return projs[type - ProjectileID.Count];
        }

        /// <summary>
        /// <see cref="Projectile.type"/>是否是<see cref="ExtenProjectile"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return ProjectileID.Count <= type && type < ProjectileCount;
        }
    }
}
