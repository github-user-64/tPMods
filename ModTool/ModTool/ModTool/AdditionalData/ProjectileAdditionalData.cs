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
            PatchGame.PatchMain.OnEnterWorlding += EnterWorlding;
            PatchGame.PatchProjectile.OnSetDefaults += OnSetDefaults;
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            ClearData();
        }

        private void OnSetDefaults(Projectile This, int Type)
        {
            if (This.active == false || This.type != Type)
            {
                UpdateDataItem(This.whoAmI, true);
            }
        }
    }
}
