using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    public static partial class ProjectileLoad
    {
        private static readonly int InitData_MaxNPCs = 200;
        private static void ResizeArrays()
        {
            LocalizedText[] _projectileNameCache = (LocalizedText[])__projectileNameCache.GetValue(null);

            Utils.Utils.ResetStaticMembers(typeof(ProjectileID.Sets));

            Array.Resize(ref TextureAssets.Projectile, ProjectileCount);
            Array.Resize(ref Main.projHostile, ProjectileCount);
            Array.Resize(ref Main.projHook, ProjectileCount);
            Array.Resize(ref Main.projFrames, ProjectileCount);
            Array.Resize(ref Main.projPet, ProjectileCount);

            Array.Resize(ref _projectileNameCache, ProjectileCount);
            for (int i = ProjectileID.Count; i < ProjectileCount; ++i)
            {
                Main.projFrames[i] = 1;
                _projectileNameCache[i] = LocalizedText.Empty;
            }

            Resize(ref Projectile.perIDStaticNPCImmunity, ProjectileCount, InitData_MaxNPCs);

            __projectileNameCache.SetValue(null, _projectileNameCache);
        }

        private static void Resize<T>(ref T[,] array, int len1, int len2)
        {
            T[,] arr = new T[len1, len2];

            len1 = Math.Min(len1, array.GetLength(0));
            len2 = Math.Min(len2, array.GetLength(1));

            for (int i = 0; i < len1; ++i)
            {
                for (int j = 0; j < len2; ++j)
                {
                    arr[i, j] = array[i, j];
                }
            }

            array = arr;
        }
    }
}
