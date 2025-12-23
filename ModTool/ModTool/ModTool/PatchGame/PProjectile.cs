using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="Projectile"/>
    /// </summary>
    public class PProjectile : tContentPatch.PatchProjectile
    {
        /// <summary/>
        public delegate void SetDefaultsEvent(Projectile This, int Type);
        /// <summary>
        /// 在设置默认后
        /// </summary>
        public static event SetDefaultsEvent OnSetDefaultsPo = null;

        /// <inheritdoc/>
        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            OnSetDefaultsPo?.Invoke(This, Type);
        }
    }
}
