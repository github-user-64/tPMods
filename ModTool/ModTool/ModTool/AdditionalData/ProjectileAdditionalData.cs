using Terraria;
using Terraria.DataStructures;

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
            PatchGame.PatchProjectile.OnNewProjectilePos += OnNewProjectilePos;
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            ClearData();
        }

        private void OnNewProjectilePos(int result, IEntitySource spawnSource,
            float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1, float ai2)
        {
            if (Main.projectile?.IndexInRange(result) != true) return;

            Entity v = Main.projectile[result];

            if (v == null) return;
            if (v.active == false) return;

            UpdateDataItem(result, true);
        }
    }
}
