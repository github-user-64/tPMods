using HarmonyLib;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BedWars.Run
{
    [HarmonyPatch(typeof(NPC))]
    internal class PatchNPC
    {
        public static bool CanNewNPC = true;

        [HarmonyPatch("NewNPC")]
        [HarmonyPrefix]
        internal static bool NewNPC(IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
        {
            return CanNewNPC;
        }

        public static NPC NewNPC(Vector2 pos, int Type, int Start = 0, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f, float ai3 = 0f, int Target = 255)
        {
            bool temp = CanNewNPC;
            CanNewNPC = true;
            int index = NPC.NewNPC(null, (int)pos.X, (int)pos.Y, Type, Start, ai0, ai1, ai2, ai3, Target);
            CanNewNPC = temp;

            return Main.npc[index];
        }
    }
}
