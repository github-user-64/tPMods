using ChatBarCMD.Utils;
using CommandHelp;
using System;
using System.Collections.Generic;
using Terraria;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 单人和客户端
    /// </summary>
    public class NetMode01
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
        /// 游戏指令, 在单人或客户端时可用
        /// </summary>
        public static List<Utils.CMDGet> CMD { get; internal set; } = new List<Utils.CMDGet>();
        /// <summary>
        /// 来自服务器的指令, 仅用于指令提示
        /// </summary>
        public static List<Utils.CMDGet> ServerCMD { get; internal set; } = new List<Utils.CMDGet>();

        /// <summary>
        /// 功能启用且在单人或客户端时
        /// </summary>
        public static bool CanUse()
        {
            if (Enable.val == false) return false;
            return Main.netMode == 0 || Main.netMode == 1;
        }

        /// <summary>
        /// 获取单人和客户端指令
        /// </summary>
        public static List<CommandObject> GetCMD(Action<string> print)
        {
            return Utils.GetCMD(CMD, Main.myPlayer, print);
        }

        /// <summary>
        /// 获取服务器指令
        /// </summary>
        public static List<CommandObject> GetServerCMD(Action<string> print)
        {
            return Utils.GetCMD(ServerCMD, Main.myPlayer, print);
        }
    }
}
