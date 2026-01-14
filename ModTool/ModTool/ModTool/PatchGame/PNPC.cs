using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria;
using static ModTool.PatchGame.PPlayer;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="NPC"/>
    /// </summary>
    public class PNPC : tContentPatch.PatchNPC
    {
        /// <summary>
        /// 能否自然生成npc
        /// </summary>
        public static event Func<bool> OnCanSpawnNPC
        {
            add
            {
                if (value == null) return;
                onCanSpawnNPC.Add(value);
            }
            remove => onCanSpawnNPC.Remove(value);
        }
        private static readonly List<Func<bool>> onCanSpawnNPC = new List<Func<bool>>();

        /// <summary>
        /// 在设置默认后
        /// </summary>
        public static event SetDefaultsEvent OnSetDefaultsPo = null;
        /// <summary/>
        public delegate void SetDefaultsEvent(NPC This, int Type, NPCSpawnParams spawnparams);

        /// <inheritdoc/>
        public override void SetDefaultsPostfix(NPC This, int Type, NPCSpawnParams spawnparams)
        {
            OnSetDefaultsPo?.Invoke(This, Type, spawnparams);
        }

        [HarmonyPatch(typeof(NPC), "SpawnNPC")]
        private static class PatchSpawnNPC
        {
            internal static bool Prefix()
            {
                bool ok = true;
                onCanSpawnNPC.ForEach(i => ok &= i());

                return ok;
            }
        }
    }
}
