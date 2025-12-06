using CommandHelp;
using ModTool.Utils;
using System;
using System.Collections.Generic;
using Terraria;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 单人和客户端
    /// </summary>
    public static class NetMode01
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
        /// 指令头, 发送到服务端的
        /// </summary>
        public static GetSetReset<string> HeadToServer = new GetSetReset<string>("/", "/", s => s ?? "/");
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
        /// 获取服务器指令, 仅用于指令提示
        /// </summary>
        public static List<CommandObject> GetServerCMD(Action<string> print)
        {
            return Utils.GetCMD(ServerCMD, Main.myPlayer, print, false);
        }

        /// <summary>
        /// 聊天转文本, 不是指令返回<see langword="null"/>, 是发送到服务端的指令则<paramref name="isToServer"/>为<see langword="true"/>
        /// </summary>
        public static string ChatToCMD(string chat, out bool isToServer)
        {
            isToServer = false;

            string cmd = Utils.ChatToCMD(chat, Head.val);
            string cmdts = Utils.ChatToCMD(chat, HeadToServer.val);

            if (cmd == null)
            {
                if (cmdts != null) isToServer = true;
                return cmdts;
            }
            if (cmdts == null) return cmd;

            //两个指令头都匹配时, 返回头最长的那个
            if (Head.val.Length < HeadToServer.val.Length)
            {
                isToServer = true;
                return cmdts;
            }

            return cmd;
        }

        internal static bool OnSend(string chat)//单人和客户端发送聊天时
        {
            if (CanUse() == false) return true;

            string cmd = ChatToCMD(chat, out bool iss);
            if (iss) return true;//是发送到服务端则不处理
            if (cmd == null) return true;//不是指令则不处理

            try
            {
                Action<string> print = s => Utils.MainNewTextTry(s, B: 0);
                Utils.InputCMD(cmd, GetCMD(print), print);
            }
            catch { }

            return false;//不发送
        }
    }
}
