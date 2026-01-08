using ModTool.Utils.GetDataEventArgs;
using System;
using Terraria;

namespace BedWars.Run
{
    /// <summary>
    /// 状态的行为
    /// </summary>
    public abstract class IStateAction
    {
        protected GameRun game = null;
        public IStateAction(GameRun game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public virtual void OnStart() { }

        public virtual void OnEnd() { }

        public virtual void Update() { }

        /// <summary>
        /// 不能和图格交互
        /// </summary>
        public virtual bool PlayCanActionTile(ClassTileEventArgs e) => false;

        /// <summary>
        /// 除控制移动外不能做任何操作
        /// </summary>
        public virtual bool PlayCanAction(GetDataEventArgs e)
        {
            if (e is ControlsEventArgs ce == false) return false;//不是控制
            if (ce.ghost != ce.player.ghost) return false;//修改了幽灵状态

            return true;
        }

        /// <summary>
        /// 玩家加入时设为幽灵状态
        /// </summary>
        public virtual void OnPlayJoinGame(Player player)
        {
            game.SetPlayGhost(player);
        }

        public virtual void OnPlayLeftGame(Player player) { }

        public virtual void OnPlayLogin(Player player) { }

        /// <summary>
        /// 可以给玩家添加buff<see cref="Terraria.ID.BuffID.NoBuilding"/>
        /// </summary>
        public virtual bool CanAddBuffNoBuilding() => true;
    }
}
