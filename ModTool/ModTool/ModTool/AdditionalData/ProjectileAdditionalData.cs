using System.Linq;
using Terraria;

namespace ModTool.AdditionalData
{
    /// <summary>
    /// 射弹附加数据
    /// </summary>
    public abstract class ProjectileAdditionalData<T> : AdditionalData<T, Projectile>
    {
        /// <summary/>
        public ProjectileAdditionalData() : base(Main.projectile)
        {
            PatchGame.PMain.OnEnterWorldPr += OnEnterWorldPr;
            PatchGame.PProjectile.OnSetDefaultsPo += OnSetDefaultsPos;

            OnNew();
        }

        /// <inheritdoc/>
        protected override void OnNew()
        {
            for (int i = 0; i < Main.projectile.Length; ++i)
            {
                if (Main.projectile[i]?.active != true) continue;

                UpdateDataItem(i, false);
            }
        }

        /// <summary>
        /// 单人和客户端进入游戏前
        /// </summary>
        protected virtual void OnEnterWorldPr()
        {
            ClearData();
        }

        private void OnSetDefaultsPos(Projectile This, int Type)
        {
            if (Main.projectile?.Contains(This) != true) return;

            UpdateDataItem(This.whoAmI, true);
        }
    }
}
