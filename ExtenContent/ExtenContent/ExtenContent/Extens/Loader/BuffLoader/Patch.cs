using Terraria;

namespace ExtenContent.Extens
{
    public static partial class BuffLoader
    {
        internal static void UpdateBuffs(Player player)
        {
            for (int i = 0; i < Player.maxBuffs; ++i)
            {
                if (player.buffType[i] <= 0 || player.buffTime[i] <= 0) continue;

                GetBuff(player.buffType[i])?.UpdateBuffs(player);
            }
        }

        internal static void UpdateBuffs(NPC npc, bool lowerBuffTime)
        {
            for (int i = 0; i < NPC.maxBuffs; ++i)
            {
                if (npc.buffType[i] <= 0 || npc.buffTime[i] <= 0) continue;

                GetBuff(npc.buffType[i])?.UpdateBuffs(npc, lowerBuffTime);
            }
        }
    }
}
