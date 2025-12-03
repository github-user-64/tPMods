using System;
using Terraria;

namespace ServerHelp.PatchGame
{
    /// <summary/>
    public class PatchMain : tContentPatch.PatchMain
    {
        /// <summary>在单人或客户端进入世界后</summary>
        public static event Action OnEnterWorlding = null;

        /// <inheritdoc/>
        public override void OnEnterWorld()
        {
            if (Main.netMode != 0 && Main.netMode != 1) return;

            OnEnterWorlding?.Invoke();
        }
    }
}
