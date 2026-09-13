using ExtenContent.Extens;
using HarmonyLib;
using System;
using Terraria.ID;

namespace ExtenContent.PatchGame
{
    [HarmonyPatch(typeof(SetFactory))]
    internal static class PSetFactory
    {
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(int) })]
        [HarmonyPrefix]
        public static void SetFactoryPrefix(ref int size)
        {
            if (size == ItemID.Count) size = ItemLoad.ItemCount;
        }
    }
}
