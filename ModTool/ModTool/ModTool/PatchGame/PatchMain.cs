using System;

namespace ModTool.PatchGame
{
    /// <summary/>
    public class PatchMain : tContentPatch.PatchMain
    {
        /// <summary>在单人或客户端进入世界前</summary>
        public static event Action OnEnterWorldPr = null;

        /// <inheritdoc/>
        public override void OnEnterWorldPrefix()
        {
            OnEnterWorldPr?.Invoke();
        }
    }
}
