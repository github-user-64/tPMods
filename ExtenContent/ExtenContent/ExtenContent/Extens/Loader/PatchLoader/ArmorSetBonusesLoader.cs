using System.Collections.Generic;
using Terraria.DataStructures;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 套装效果加载器
    /// </summary>
    public static class ArmorSetBonusesLoader
    {
        private static readonly List<EPatchArmorSetBonus> PatchArmorSetBonuses = new List<EPatchArmorSetBonus>();

        internal static void Load()
        {
            Utils.Utils.ResetStaticMembers(typeof(ArmorSetBonuses));
            ArmorSetBonuses.Initialize();

            PatchArmorSetBonuses.ForEach(i => i.AddArmorSetBonus());

            ArmorSetBonuses.BuildLookup();
        }

        internal static void Unload()
        {
            PatchArmorSetBonuses.Clear();

            Utils.Utils.ResetStaticMembers(typeof(ArmorSetBonuses));
            ArmorSetBonuses.Initialize();
            ArmorSetBonuses.BuildLookup();
        }

        internal static void Register(EPatchArmorSetBonus e)
        {
            PatchArmorSetBonuses.Add(e);
        }
    }
}
