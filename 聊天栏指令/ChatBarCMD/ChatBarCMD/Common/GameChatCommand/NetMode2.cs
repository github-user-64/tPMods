using ChatBarCMD.Utils;
using CommandHelp;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 服务端
    /// </summary>
    internal class NetMode2
    {
        /// <summary>
        /// 启用
        /// </summary>
        public static GetSetReset<bool> Enable = new GetSetReset<bool>(true, true);
        /// <summary>
        /// 指令头
        /// </summary>
        public static GetSetReset<string> Head = new GetSetReset<string>("//", "//", s => s ?? "//");
        /// <summary>
        /// 游戏指令, 在服务端时可用
        /// </summary>
        public static List<Utils.CMDGet> CMD { get; internal set; } = new List<Utils.CMDGet>();

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
            return Main.netMode == 2;
        }
    }
}
