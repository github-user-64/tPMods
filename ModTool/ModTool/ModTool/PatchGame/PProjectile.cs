using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using static ModTool.PatchGame.PPlayer;

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

        /// <summary>
        /// 设置闪电颜色
        /// </summary>
        public static event SetLightningColorEvent SetLightningColor
        {
            add
            {
                if (value == null) return;
                setLightningColor.Add(value);
            }
            remove => setLightningColor.Remove(value);
        }
        /// <summary/>
        public delegate Color SetLightningColorEvent(Projectile This, Color color);
        private static readonly List<SetLightningColorEvent> setLightningColor = new List<SetLightningColorEvent>();


        /// <inheritdoc/>
        public override void SetDefaultsPostfix(Projectile This, int Type)
        {
            OnSetDefaultsPo?.Invoke(This, Type);
        }

        /// <inheritdoc/>
        public override Color AI_203_GetLightningColor(Projectile This, Color color)
        {
            setLightningColor.ForEach(i => color = i(This, color));

            return color;
        }
    }
}
