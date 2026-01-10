using HarmonyLib;
using System;
using System.Collections.Generic;
using Terraria;

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
        public static List<Func<bool>> OnCanSpawnNPC { get; private set; } = new List<Func<bool>>();
        /// <summary/>
        public delegate void SetDefaultsEvent(NPC This, int Type, NPCSpawnParams spawnparams);
        /// <summary>
        /// 在设置默认后
        /// </summary>
        public static event SetDefaultsEvent OnSetDefaultsPo = null;

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
                OnCanSpawnNPC.RemoveAll(i => i == null);

                bool ok = true;
                foreach (Func<bool> i in OnCanSpawnNPC)
                {
                    ok &= i();
                }

                return ok;
            }
        }
    }
}
