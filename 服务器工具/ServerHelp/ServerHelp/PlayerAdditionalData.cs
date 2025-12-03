using Terraria;

namespace ServerHelp
{
    /// <summary>
    /// 玩家附加数据
    /// </summary>
    public abstract class PlayerAdditionalData<T> : AdditionalData<T, Player>
    {
        /// <summary/>
        public PlayerAdditionalData() : base(Main.player)
        {
            PatchGame.PatchMain.OnEnterWorlding += EnterWorlding;
            PatchGame.PatchNetMessage.OnSyncConnectedPlayer += ConnectedPlayer;
            PatchGame.PatchNetMessage.OnSyncDisconnectedPlayer += DisconnectedPlayer;
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            UpdateDataItem(Main.myPlayer, true);
        }

        /// <summary>
        /// 玩家连接时
        /// </summary>
        public virtual void ConnectedPlayer(int ply)
        {
            UpdateDataItem(ply, true);
        }

        /// <summary>
        /// 玩家断开连接时
        /// </summary>
        public virtual void DisconnectedPlayer(int ply)
        {
            ClearDataItem(ply);
        }

        /// <summary>
        /// 检查玩家, 正常返回<see langword="true"/>, 否则<paramref name="whoAmI"/>为-1
        /// </summary>
        protected virtual bool CheckPlayer(Player player, out int whoAmI)
        {
            whoAmI = -1;

            if (player == null) return false;

            int index = player.whoAmI;

            Player p = CheckPlayer(index);
            if (p == null) return false;

            whoAmI = index;
            return true;
        }

        /// <summary>
        /// 检查玩家, 正常返回<see cref="Player"/>
        /// </summary>
        protected virtual Player CheckPlayer(int whoAmI)
        {
            if (IndexInRange(whoAmI) == false) return null;

            Player player = Main.player[whoAmI];
            if (player == null) return null;

            if (Main.netMode == 0)
            {
                if (player.whoAmI == Main.myPlayer) return player;
                return null;
            }

            if (Main.netMode == 1)
            {
                if (player.whoAmI == Main.myPlayer) return player;
                if (player.active == true) return player;
                return null;
            }

            RemoteClient c = player.GetClient();
            if (c == null) return null;

            return player;
        }
    }
}
