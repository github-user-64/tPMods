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

        public virtual void OnStart(object arg) { }

        public virtual void OnEnd() { }

        public virtual void Update(uint gametime) { }

        /// <summary>
        /// 不能和图格交互
        /// </summary>
        public virtual bool PlayCanActionTile(ClassTileEventArgs e) => false;

        /// <summary>
        /// 不能做任何操作, 除了:
        /// <para/>控制移动
        /// </summary>
        public virtual bool PlayCanAction(GetDataEventArgs e)
        {
            if (e is ControlsEventArgs ce)
            {
                return ce.ghost == ce.player.ghost;//是否修改了幽灵状态
            }

            return false;
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

        public virtual void OnPlayJoinGameTryAutoLoginPo(Player player) { }

        public virtual void OnGetDataPr(Player player, int messageType) { }

        public virtual void OnGetDataPo(Player player, int messageType) { }

        public virtual bool ModifyShop(ModTool.Common.ModifyShop.ItemData[] items, NPC npc, Player player) => false;
    }
}
