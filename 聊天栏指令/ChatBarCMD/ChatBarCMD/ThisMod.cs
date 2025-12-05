using System;
using System.Linq;
using System.Reflection;
using tContentPatch;
using tContentPatch.ModLoad;

namespace ChatBarCMD
{
    /// <summary>
    /// 单人和客户端发送指令时, 是发送到服务端的指令则不处理, 否则拦截消息并在本地运行指令
    /// 服务端收到指令时, 拦截消息并运行指令
    /// </summary>
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; private set; } = null;

        public override void Load()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.assembly == Assembly.GetExecutingAssembly());

            ThisMod.mo = mo;

            if (mo == null) throw new Exception($"{nameof(ChatBarCMD)}:找不到模组对象");

            //

            Common.GameChatCommand.OnSendChat.Init();
        }
    }
}
