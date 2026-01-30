using Terraria;

namespace BedWars.Run.StateActions.GameAction
{
    public abstract class IGameAction
    {
        public virtual void OnProjectileKill(Projectile proj) { }
        public virtual void UpdateProjectile(Projectile proj, Player player) { }
    }
}
