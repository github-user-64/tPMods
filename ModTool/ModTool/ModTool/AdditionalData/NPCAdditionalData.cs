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
            PatchGame.PMain.OnEnterWorldPr += EnterWorldPr;
            PatchGame.PNPC.OnSetDefaultsPo += OnSetDefaultsPos;
        }

        /// <inheritdoc/>
        protected override void OnNew()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i]?.active != true) continue;

                UpdateDataItem(i, false);
            }
        }

        /// <summary>
        /// 单人和客户端进入游戏前
        /// </summary>
        protected virtual void EnterWorldPr()
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
