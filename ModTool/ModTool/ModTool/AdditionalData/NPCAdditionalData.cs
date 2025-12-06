using System.Linq;
using Terraria;

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
            PatchGame.PatchMain.OnEnterWorldPr += EnterWorldPr;
            PatchGame.PatchNPC.OnSetDefaultsPos += OnSetDefaultsPos;
        }

        /// <summary>
        /// 单人和客户端进入游戏前
        /// </summary>
        public virtual void EnterWorldPr()
        {
            ClearData();
        }

        private void OnSetDefaultsPos(NPC This, int Type, NPCSpawnParams spawnparams)
        {
            if (Main.npc?.Contains(This) != true) return;

            UpdateDataItem(This.whoAmI, true);
        }
    }
}
