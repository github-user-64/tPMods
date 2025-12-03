using Terraria;
using Terraria.DataStructures;

namespace ModTool.AdditionalData
{
    /// <summary>
    /// NPC附加数据
    /// </summary>
    public abstract class NPCAdditionalData<T> : AdditionalData<T, NPC>
    {
        /// <summary/>
        public NPCAdditionalData() : base(Main.npc)
        {
            PatchGame.PatchMain.OnEnterWorlding += EnterWorlding;
            PatchGame.PatchNPC.OnNewNPCPos += PatchNPC_OnNewNPCPos;
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            ClearData();
        }

        private void PatchNPC_OnNewNPCPos(int result, IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
        {
            if (Main.npc?.IndexInRange(result) != true) return;

            Entity v = Main.npc[result];

            if (v == null) return;
            if (v.active == false) return;

            UpdateDataItem(result, true);
        }
    }
}
