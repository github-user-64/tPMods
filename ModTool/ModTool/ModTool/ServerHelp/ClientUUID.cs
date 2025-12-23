using ModTool.AdditionalData;
using Terraria;
using Terraria.ID;

namespace ModTool.ServerHelp
{
    internal class ClientUUID : PlayerAdditionalData<string>
    {
        #region
        private static ClientUUID instance = null;

        internal static void Init()
        {
            instance = new ClientUUID();
        }

        internal static string GetUUID(Player player)
        {
            if (instance == null) return null;
            return instance.GetData(player.whoAmI);
        }
        #endregion

        public ClientUUID()
        {
            PatchGame.PMessageBuffer.OnGetDataPo.Add(GetDataPo);
        }

        private void GetDataPo(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.Unknown68) return;
            if (Main.netMode != 2) return;
            //当服务端收到客户端发送的uuid

            int index = This.whoAmI;

            Player p = CheckPlayer(index);
            if (p == null) return;

            SetUUIDData(index, This.reader.ReadString());
        }

        //不允许设置值
        public override bool SetData(int index, string val) => false;
        //不允许设置值
        protected override void ClearDataItem(int index) { }

        public override void ServerDisconnectedPlayer(int ply)
        {
            if (IndexInRange(ply) == false) return;

            data[ply] = null;
            hasData[ply] = false;
        }

        private void SetUUIDData(int index, string val)
        {
            if (IndexInRange(index) == false) return;

            data[index] = val;
            hasData[index] = true;
        }
    }
}
