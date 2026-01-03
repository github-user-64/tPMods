using ModTool.AdditionalData;
using System;
using Terraria;
using Terraria.ID;

namespace ModTool.ServerHelp
{
    internal class ClientUUID : PlayerAdditionalData<string>
    {
        /// <summary>
        /// 在收到uuid时
        /// </summary>
        public Action<int> OnGotUUID = null;

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
        public override bool UpdateDataItem(int index, bool clearOld = false) => false;

        private void SetUUIDData(int index, string val)
        {
            if (IndexInRange(index) == false) return;

            data[index] = val;
            hasData[index] = true;

            OnGotUUID?.Invoke(index);
        }
    }
}
