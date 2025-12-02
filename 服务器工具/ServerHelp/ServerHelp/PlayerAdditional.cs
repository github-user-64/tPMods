using Terraria;

namespace ServerHelp
{
    /// <summary>
    /// 玩家附加数据
    /// </summary>
    public class PlayerAdditional<T>
    {
        /// <summary>数据</summary>
        protected T[] data = null;
        /// <summary>玩家名</summary>
        protected string[] name = null;

        /// <summary/>
        public PlayerAdditional()
        {
            data = new T[Main.player.Length];
            name = new string[data.Length];

            PatchGame.PatchNetMessage.OnSyncConnectedPlayer += ConnectedPlayer;
            PatchGame.PatchNetMessage.OnSyncDisconnectedPlayer += DisconnectedPlayer;
        }

        /// <summary>
        /// 玩家断开连接时
        /// </summary>
        public virtual void DisconnectedPlayer(int ply)
        {
            if (data?.IndexInRange(ply) != true) return;

            data[ply] = default;
            name[ply] = null;
        }

        /// <summary>
        /// 玩家连接时
        /// </summary>
        public virtual void ConnectedPlayer(int ply)
        {
            if (data?.IndexInRange(ply) != true) return;

            try
            {
                Player p = Utils.GetPlay(ply);
                if (p?.name == null) return;

                data[ply] = default;
                name[ply] = null;
                data[ply] = Converter(ply);
                name[ply] = p.name;
            }
            catch { }
        }

        /// <summary>
        /// 将<paramref name="ply"/>转化为<typeparamref name="T"/>
        /// </summary>
        public virtual T Converter(int ply) => default;

        /// <summary>
        /// 获取玩家的数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public T GetData(Player player, T def = default)
        {
            if (CheckPlayer(player, out int index) == false) return def;

            return data[index];
        }

        /// <summary>
        /// 设置数据, 成功返回<see langword="true"/>
        /// </summary>
        public bool SetData(Player player, T val)
        {
            if (CheckPlayer(player, out int index) == false) return false;

            data[index] = val;

            return true;
        }

        /// <summary>
        /// 检查玩家, 正常返回<see langword="true"/>, 否则<paramref name="whoAmI"/>为-1
        /// </summary>
        protected bool CheckPlayer(Player player, out int whoAmI)
        {
            whoAmI = -1;

            RemoteClient c = player.GetClient();
            if (c == null) return false;

            int index = player.whoAmI;

            if (data?.IndexInRange(index) != true) return false;

            if (player.name != name[index]) return false;

            whoAmI = index;
            return true;
        }

        /// <summary>
        /// 检查玩家, 正常返回<see cref="Player"/>
        /// </summary>
        protected Player CheckPlayer(int whoAmI)
        {
            if (data?.IndexInRange(whoAmI) != true) return null;

            Player p = Utils.GetPlay(whoAmI);

            return p;
        }
    }
}
