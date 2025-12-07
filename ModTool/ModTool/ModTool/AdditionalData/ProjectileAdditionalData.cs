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
            PatchGame.PatchMain.OnEnterWorldPr += EnterWorldPr;
            PatchGame.PatchProjectile.OnSetDefaultsPo += OnSetDefaultsPos;
        }

        /// <summary>
        /// 单人和客户端进入游戏前
        /// </summary>
        public virtual void EnterWorldPr()
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
