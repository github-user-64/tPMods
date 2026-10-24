using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 套装效果加载器
    /// </summary>
    public static class PatchArmorSetBonusesLoader
    {
        private static readonly List<EPatchArmorSetBonus> PatchArmorSetBonuses = new List<EPatchArmorSetBonus>();

        internal static void Load()
        {
            LoaderUtils.ResetStaticMembers(typeof(ArmorSetBonuses));
            ArmorSetBonuses.Initialize();

            PatchArmorSetBonuses.ForEach(i => i.AddArmorSetBonus());

            ArmorSetBonuses.BuildLookup();
        }

        internal static void UnloadPrefix() { }
        internal static void Unload()
        {
            PatchArmorSetBonuses.Clear();

            LoaderUtils.ResetStaticMembers(typeof(ArmorSetBonuses));
            ArmorSetBonuses.Initialize();
            ArmorSetBonuses.BuildLookup();
        }

        internal static void Register(EPatchArmorSetBonus e)
        {
            PatchArmorSetBonuses.Add(e);
        }
    }
}
