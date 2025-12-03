using Terraria;
using Terraria.DataStructures;

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
        }

        /// <summary>
        /// 单人和客户端进入游戏时
        /// </summary>
        public virtual void EnterWorlding()
        {
            ClearData();
        }

        private void OnNewItemPos(int result, IEntitySource source, int X, int Y, int Width, int Height, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
        {
            if (Main.item?.IndexInRange(result) != true) return;

            Entity v = Main.item[result];

            if (v == null) return;
            if (v.active == false) return;

            UpdateDataItem(result, true);
        }
    }
}
