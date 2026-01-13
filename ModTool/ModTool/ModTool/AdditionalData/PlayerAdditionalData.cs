using ModTool.ServerHelp;
using Terraria;

namespace ModTool.AdditionalData
{
    /// <summary>
    /// 玩家附加数据
    /// </summary>
    public abstract class PlayerAdditionalData<T> : AdditionalData<T, Player>
    {
        /// <summary/>
        public PlayerAdditionalData() : base(Main.player)
        {
            PatchGame.PMain.OnEnterWorldPr += EnterWorldPr;
            PatchGame.PMessageBuffer.OnPlayerConnecting += ClientGotConnect;
            PatchGame.PMessageBuffer.OnPlayerDisconnecting += ClientGotDisconnect;
            PlayerJoinLeft.OnSyncConnectedPlayerPr += ServerConnectedPlayer;
            PlayerJoinLeft.OnPlayerClientDisconnected += p => ServerDisconnectedPlayer(p.whoAmI);
        }

        /// <inheritdoc/>
        protected override void OnNew()
        {
            int len = Main.player.Length - 1;//最后一个是服务器

            for (int i = 0; i < len; ++i)
            {
                Player p = CheckPlayer(i);
                if (p == null) continue;

                UpdateDataItem(i, false);
            }
        }

        /// <summary>
        /// 客户端收到玩家连接时
        /// </summary>
        protected virtual void ClientGotConnect(int playerIndex)
        {
            UpdateDataItem(Main.myPlayer, true);
        }

        /// <summary>
        /// 客户端收到玩家断开连接时
        /// </summary>
        protected virtual void ClientGotDisconnect(int playerIndex)
        {
            ClearDataItem(playerIndex);
        }

        /// <summary>
        /// 单人和客户端进入游戏前
        /// </summary>
        protected virtual void EnterWorldPr()
        {
            ClearData();
            UpdateDataItem(Main.myPlayer, true);
        }

        /// <summary>
        /// 服务端在同步已连接玩家前
        /// </summary>
        protected virtual void ServerConnectedPlayer(int ply)
        {
            UpdateDataItem(ply, true);
        }

        /// <summary>
        /// 服务端在同步断开连接玩家前
        /// </summary>
        protected virtual void ServerDisconnectedPlayer(int ply)
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

            if (Main.netMode == 0)//单人
            {
                if (player.whoAmI == Main.myPlayer) return player;
                return null;
            }

            if (Main.netMode == 1)//客户端
            {
                if (player.whoAmI == Main.myPlayer) return player;
                if (player.active == true) return player;
                return null;
            }

            if (Main.netMode == 2)//服务端
            {
                RemoteClient c = player.GetClient();
                if (c == null) return null;
                return player;
            }

            return null;
        }
    }
}
