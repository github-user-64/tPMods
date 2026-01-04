using ModTool.Utils.GetDataEventArgs;
using Terraria;

namespace BedWars.Run
{
    /// <summary>
    /// 状态的行为
    /// </summary>
    public abstract class IStateAction
    {
        public virtual void OnStart() { }

        public virtual void OnEnd() { }

        public virtual void Update() { }

        public virtual bool PlayCanActionTile(ClassTileEventArgs e) => false;

        /// <summary>
        /// 玩家能否做各种交互, 不包括移动
        /// </summary>
        public virtual bool PlayCanAction(GetDataEventArgs e) => false;

        public virtual void OnPlayJoinGame(Player player) { }

        public virtual void OnPlayLeftGame(int plr) { }

        public virtual void OnPlayLogin(Player player) { }

        /// <summary>
        /// 能否给玩家添加buff<see cref="Terraria.ID.BuffID.NoBuilding"/>
        /// </summary>
        public virtual bool CanAddBuffNoBuilding() => true;
    }
}
