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

            PatchGame.PatchMain.OnEnterWorlding += EnterWorlding;
            PatchGame.PatchNetMessage.OnSyncConnectedPlayer += ConnectedPlayer;
            PatchGame.PatchNetMessage.OnSyncDisconnectedPlayer += DisconnectedPlayer;
        }

        private void EnterWorlding()
        {
            UpdateDataItem(Main.myPlayer);
        }

        /// <summary>
        /// 玩家断开连接时
        /// </summary>
        public virtual void DisconnectedPlayer(int ply)
        {
            ClearDataItem(ply);
        }

        /// <summary>
        /// 玩家连接时
        /// </summary>
        public virtual void ConnectedPlayer(int ply)
        {
            ClearDataItem(ply);

            UpdateDataItem(ply);
        }

        /// <summary>
        /// 将<paramref name="ply"/>转化为<typeparamref name="T"/>
        /// </summary>
        public virtual T ConverterThrow(int ply) => default;

        /// <summary>
        /// 获取玩家的数据, 不存在返回<paramref name="def"/>
        /// </summary>
        public virtual T GetData(Player player, T def = default)
        {
            if (CheckPlayer(player, out int index) == false) return def;

            if (name[index] == null) return def;

            return data[index];
        }

        /// <summary>
        /// 设置数据, 成功返回<see langword="true"/>
        /// </summary>
        public virtual bool SetData(Player player, T val)
        {
            if (CheckPlayer(player, out int index) == false) return false;

            if (name[index] == null) return false;

            data[index] = val;

            return true;
        }
        
        /// <summary>
        /// 更新数据
        /// </summary>
        public virtual void UpdateData()
        {
            if (data == null) return;

            for (int i = 0; i < data.Length; ++i)
            {
                ClearDataItem(i);

                UpdateDataItem(i);
            }
        }

        /// <summary>
        /// 更新数据项, 成功返回<see langword="true"/>
        /// </summary>
        public virtual bool UpdateDataItem(int index)
        {
            try
            {
                if (data?.IndexInRange(index) != true) return false;

                Player p = ServerUtils.GetPlay(index);
                if (p?.name == null) return false;

                T d = ConverterThrow(index);
                string n = p.name;

                data[index] = d;
                name[index] = n;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 清除数据项
        /// </summary>
        protected virtual void ClearDataItem(int index)
        {
            if (data?.IndexInRange(index) != true) return;

            data[index] = default;
            name[index] = null;
        }

        /// <summary>
        /// 检查玩家, 正常返回<see langword="true"/>, 否则<paramref name="whoAmI"/>为-1
        /// </summary>
        protected virtual bool CheckPlayer(Player player, out int whoAmI)
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
        protected virtual Player CheckPlayer(int whoAmI)
        {
            if (data?.IndexInRange(whoAmI) != true) return null;

            Player p = ServerUtils.GetPlay(whoAmI);

            return p;
        }
    }
}
