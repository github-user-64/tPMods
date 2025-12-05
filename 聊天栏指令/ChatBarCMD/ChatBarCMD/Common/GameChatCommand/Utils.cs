using CommandHelp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 工具
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 获取指令
        /// </summary>
        /// <param name="clientId">客户端id</param>
        public delegate List<CommandObject> CMDGet(int clientId);

        /// <summary>
        /// 聊天转指令, 不是指令返回<see langword="null"/>
        /// </summary>
        public static string ChatToCMD(string chat, string head)
        {
            if (chat == null) return null;

            if (chat.Length < head.Length) return null;

            string chatHead = chat.Substring(0, head.Length);
            if (chatHead != head) return null;

            return chat.Substring(head.Length);
        }

        /// <summary>
        /// 聊天转指令, 不是指令返回<see langword="null"/>, 是发送到服务端的指令则<paramref name="isToServer"/>为<see langword="true"/>
        /// </summary>
        public static string ChatToCMD(string chat, out bool isToServer)
        {
            isToServer = false;//不是发送到服务端

            string cmd = ChatToCMD(chat, NetMode2.Head.val);
            if (cmd != null)
            {
                isToServer = true;//是发送到服务端
                return cmd;
            }

            return ChatToCMD(chat, NetMode01.Head.val);
        }

        /// <summary>
        /// 获取指令
        /// </summary>
        public static List<CommandObject> GetCMD(List<CMDGet> gameCmd, int clientId, Action<string> print)
        {
            List<CommandObject> cos = new List<CommandObject>();
            cos.Add(new CommandPrintList(cos, null, print));

            foreach (CMDGet i in gameCmd)
            {
                try
                {
                    List<CommandObject> list = i?.Invoke(clientId);
                    if (list == null) continue;
                    cos.AddRange(list);
                }
                catch
                {
                    Debug.WriteLine($"{nameof(Utils)}:获取指令异常, 跳过该指令");
                }
            }

            return cos;
        }

        /// <summary>
        /// 运行指令
        /// </summary>
        public static void InputCMD(string text, List<CommandObject> cos, Action<string> print)
        {
            if (text == null || text.Length < 1)
            {
                print?.Invoke("输入?获取指令信息");
                return;
            }
            if (cos == null)
            {
                print?.Invoke("没有指令");
                return;
            }

            string exText = tContentPatch.Command.Utils.CommandRun(text, cos);
            if (exText != null) print?.Invoke(exText);
        }

        /// <summary/>
        public static void MainNewTextTry(string newText, byte R = byte.MaxValue, byte G = byte.MaxValue, byte B = byte.MaxValue)
        {
            if (newText == null) return;
            if (Main.netMode != 0 && Main.netMode != 1) return;
            Main.NewText(newText, R, G, B);
        }
    }
}
