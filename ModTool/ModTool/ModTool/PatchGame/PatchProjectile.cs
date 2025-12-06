using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Projectile"/>
    /// </summary>
    public class PatchProjectile : tContentPatch.PatchProjectile
    {
        /// <summary/>
        public delegate void SetDefaultsEvent(Projectile This, int Type);
        /// <summary>在设置默认后</summary>
        public static event SetDefaultsEvent OnSetDefaultsPos = null;

        /// <inheritdoc/>
        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            OnSetDefaultsPos?.Invoke(This, Type);
        }
    }
}
