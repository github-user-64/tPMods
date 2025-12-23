using System;
using System.Diagnostics;

namespace ModTool.PatchGame
{
    /// <summary/>
    public class PMain : tContentPatch.PatchMain
    {
        /// <summary>
        /// 在单人或客户端进入世界前
        /// </summary>
        public static event Action OnEnterWorldPr = null;
        /// <summary>
        /// 在世界更新前
        /// </summary>
        public static event Action OnDoUpdateInWorldPr = null;
        /// <summary>
        /// 在世界更新后
        /// </summary>
        public static event Action OnDoUpdateInWorldPo = null;

        /// <inheritdoc/>
        public override void DoUpdateInWorldPrefix(Stopwatch sw)
        {
            OnDoUpdateInWorldPr?.Invoke();
        }

        /// <inheritdoc/>
        public override void DoUpdateInWorldPostfix(Stopwatch sw)
        {
            OnDoUpdateInWorldPo?.Invoke();
        }

        /// <inheritdoc/>
        public override void OnEnterWorldPrefix()
        {
            OnEnterWorldPr?.Invoke();
        }
    }
}
