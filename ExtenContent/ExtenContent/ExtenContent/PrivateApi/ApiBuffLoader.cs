using ExtenContent.Exten;
using Terraria;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiBuffLoader
    {
        internal ApiBuffLoader() { }

        /// <summary/>
        public void UpdateBuffsPostfix(Player player)
        {
            BuffLoader.UpdateBuffs(player);
        }

        /// <summary/>
        public void UpdateNPC_BuffSetFlagsPostfix(NPC npc, bool lowerBuffTime)
        {
            BuffLoader.UpdateBuffs(npc, lowerBuffTime);
        }
    }
}
