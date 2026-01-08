using CommandHelp;
using Microsoft.Xna.Framework;
using ModTool.Utils;
using System;
using System.Collections.Generic;
using Terraria;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 服务端
    /// </summary>
    public static class NetMode2
    {
        /// <summary>
        /// 启用
        /// </summary>
        public static GetSetReset<bool> Enable = new GetSetReset<bool>(true, true);
        /// <summary>
        /// 指令头
        /// </summary>
        public static GetSetReset<string> Head = new GetSetReset<string>("/", "/", s => s ?? "/");
        /// <summary>
        /// 游戏指令, 在服务端时可用
        /// </summary>
        public static List<Utils.CMDGet> CMD { get; internal set; } = new List<Utils.CMDGet>();

        /// <summary>
        /// 获取服务端指令
        /// </summary>
        public static List<CommandObject> GetCMD(int clientId, Action<string> print)
        {
            return Utils.GetCMD(CMD, clientId, print);
        }

        /// <summary>
        /// 功能启用且在服务端时
        /// </summary>
        public static bool CanUse()
        {
            if (Enable.val == false) return false;
            return Main.dedServ;
        }

        /// <summary>
        /// 聊天转文本, 不是指令返回<see langword="null"/>
        /// </summary>
        public static string ChatToCMD(string chat) => Utils.ChatToCMD(chat, Head.val);

        internal static bool OnGot(string chat, int clientId)//服务端收到聊天时
        {
            if (CanUse() == false) return true;

            string cmd = Utils.ChatToCMD(chat, Head.val);
            if (cmd == null) return true;

            try
            {
                Action<string> print = s => ModTool.ServerHelp.PrintTo.PrintToPlay(clientId, s, Color.Yellow);
                Utils.InputCMD(cmd, GetCMD(clientId, print), print);
            }
            catch { }

            return false;//不处理
        }
    }
}
