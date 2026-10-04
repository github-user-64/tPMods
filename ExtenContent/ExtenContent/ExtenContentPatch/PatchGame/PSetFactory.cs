using ExtenContent.Extens;
using HarmonyLib;
using System;
using System.Diagnostics;
using System.Reflection;
using Terraria.GameContent.Prefixes;
using Terraria.ID;

namespace ExtenContentPatch.PatchGame
{
    [HarmonyPatch(typeof(SetFactory))]
    internal static class PSetFactory
    {
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(int) })]
        [HarmonyPrefix]
        private static void SetFactoryPrefix(ref int size)
        {
            StackTrace st = new StackTrace();

            StackFrame sf = st.GetFrame(2);
            if (sf == null) return;

            MethodBase mb = sf.GetMethod();
            if (mb == null) return;

            Type type = mb.DeclaringType;
            if (type == null) return;

            if (type == typeof(ItemID.Sets)) size = ItemLoader.ItemCount;
            if (type == typeof(AmmoID.Sets)) size = ItemLoader.ItemCount;
            if (type == typeof(PrefixLegacy.ItemSets)) size = ItemLoader.ItemCount;
            else if (type == typeof(ArmorIDs.Wing.Sets)) size = EquipLoader.WingCount;
            else if (type == typeof(ProjectileID.Sets)) size = ProjectileLoader.ProjectileCount;
        }
    }
}
