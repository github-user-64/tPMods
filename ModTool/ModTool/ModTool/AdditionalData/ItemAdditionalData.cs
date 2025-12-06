using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ModTool.AdditionalData
{
    /// <summary>
    /// 物品附加数据
    /// </summary>
    public abstract class ItemAdditionalData<T> : AdditionalData<T, Item>
    {
        /// <summary/>
        public ItemAdditionalData() : base(Main.item)
        {
            PatchGame.PatchMain.OnEnterWorlding += EnterWorlding;
            PatchGame.PatchItem.OnNewItemPos += OnNewItemPos;
            PatchGame.PatchMessageBuffer.OnGetDataPo.Add(OnGetData);
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            ClearData();
        }

        private void OnGetData(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.SyncItem) return;
            if (Main.netMode != 1) return;
            //客户端收到物品同步后

            int whoAmI = This.reader.ReadInt16();
            UpdateDataItem(whoAmI, true);
        }

        private void OnNewItemPos(int result, IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
        {
            //如果是客户端则不处理
            //客户端返回的物品索引都是400
            //等服务端同步物品后才会有正常的索引
            //而且索引400的物品好像是无效的
            if (Main.netMode == 1) return;

            UpdateDataItem(result, true);
        }
    }
}
