using System;
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
        public static event CanGetDataEvent OnCanGetData = null;
        /// <summary>
        /// 在收到数据前
        /// </summary>
        public static event GetDataEvent OnGetDataPr = null;
        /// <summary>
        /// 在收到数据后
        /// </summary>
        public static event GetDataEvent OnGetDataPo = null;
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
            if (OnCanGetData == null) return true;

            bool ok = true;
            foreach (CanGetDataEvent i in OnCanGetData.GetInvocationList().Cast<CanGetDataEvent>())
            {
                if (i == null) continue;

                This.reader.BaseStream.Position = start + 1;
                ok &= i.Invoke(This, start, length, messageType);
            }

            return ok;
        }

        /// <inheritdoc/>
        public override void GetDataPrefix(MessageBuffer This, int start, int length, int messageType)
        {
            if (OnGetDataPr == null) return;

            foreach (GetDataEvent i in OnGetDataPr.GetInvocationList().Cast<GetDataEvent>())
            {
                This.reader.BaseStream.Position = start + 1;
                i?.Invoke(This, start, length, messageType);
            }
        }

        /// <inheritdoc/>
        public override void GetDataPostfix(MessageBuffer This, int start, int length, int messageType)
        {
            if (OnGetDataPo == null) return;

            foreach (GetDataEvent i in OnGetDataPo.GetInvocationList().Cast<GetDataEvent>())
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
