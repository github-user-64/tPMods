using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="MessageBuffer"/>
    /// </summary>
    public class PatchMessageBuffer : tContentPatch.PatchMessageBuffer
    {
        /// <summary/>
        public delegate void GetDataEvent(MessageBuffer This, int start, int length, int messageType);
        /// <summary/>
        public delegate bool CanGetDataEvent(MessageBuffer This, int start, int length, int messageType);
        /// <summary>
        /// 如果返回<see langword="false"/>那么原版方法不会被调用, 不影响<see cref="OnGetDataPr"/>和<see cref="OnGetDataPo"/>
        /// </summary>
        public static List<CanGetDataEvent> OnCanGetData { get; private set; } = new List<CanGetDataEvent>();
        /// <summary>
        /// 在收到数据前
        /// </summary>
        public static List<GetDataEvent> OnGetDataPr { get; private set; } = new List<GetDataEvent>();
        /// <summary>
        /// 在收到数据后
        /// </summary>
        public static List<GetDataEvent> OnGetDataPo { get; private set; } = new List<GetDataEvent>();
        /// <summary>
        /// 客户端收到玩家连接时//只是有玩家处于活动状态时, 其它数据可能还未同步
        /// </summary>
        public static event Action<int> OnPlayerConnecting = null;
        /// <summary>
        /// 客户端收到玩家断开连接时
        /// </summary>
        public static event Action<int> OnPlayerDisconnecting = null;

        /// <inheritdoc/>
        public override bool CanGetData(MessageBuffer This, int start, int length, int messageType)
        {
            OnCanGetData.RemoveAll(i => i == null);

            bool ok = true;
            foreach (CanGetDataEvent i in OnCanGetData)
            {
                This.reader.BaseStream.Position = start + 1;
                ok &= i.Invoke(This, start, length, messageType);
            }

            return ok;
        }

        /// <inheritdoc/>
        public override void GetDataPrefix(MessageBuffer This, int start, int length, int messageType)
        {
            OnGetDataPr.RemoveAll(i => i == null);

            foreach (GetDataEvent i in OnGetDataPr)
            {
                This.reader.BaseStream.Position = start + 1;
                i?.Invoke(This, start, length, messageType);
            }
        }

        /// <inheritdoc/>
        public override void GetDataPostfix(MessageBuffer This, int start, int length, int messageType)
        {
            OnGetDataPo.RemoveAll(i => i == null);

            foreach (GetDataEvent i in OnGetDataPo)
            {
                This.reader.BaseStream.Position = start + 1;
                i?.Invoke(This, start, length, messageType);
            }
        }

        /// <inheritdoc/>
        public override void OnPlayerConnect(int playerIndex)
        {
            OnPlayerConnecting?.Invoke(playerIndex);
        }

        /// <inheritdoc/>
        public override void OnPlayerDisconnect(int playerIndex)
        {
            OnPlayerDisconnecting?.Invoke(playerIndex);
        }
    }
}
