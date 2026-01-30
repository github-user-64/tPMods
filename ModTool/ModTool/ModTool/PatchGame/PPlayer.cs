using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.Localization;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Player"/>
    /// </summary>
    public class PPlayer : PatchPlayer
    {
        /// <summary>
        /// 能否掉落墓碑
        /// </summary>
        public static event DropTombstoneEvent OnCanDropTombstone
        {
            add
            {
                if (value == null) return;
                onCanDropTombstone.Add(value);
            }
            remove => onCanDropTombstone.Remove(value);
        }
        /// <summary/>
        public delegate bool DropTombstoneEvent(Player This, long coinsOwned, NetworkText deathText, int hitDirection);
        private readonly static List<DropTombstoneEvent> onCanDropTombstone = new List<DropTombstoneEvent>();

        /// <inheritdoc/>
        public override bool CanDropTombstone(Player This, long coinsOwned, NetworkText deathText, int hitDirection)
        {
            bool ok = true;
            onCanDropTombstone.ForEach(i => ok &= i(This, coinsOwned, deathText, hitDirection));

            return ok;
        }
    }
}
