using ChatBarCMD.Utils;
using CommandHelp;
using CommandHelp.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Terraria;

namespace ChatBarCMD.Common.GameChatCommand
{
    public partial class GameChatCommand
    {
        public static GetSetReset<bool> Enable = new GetSetReset<bool>(true, true);
        public static GetSetReset<string> CMDHead = new GetSetReset<string>("/", "/", s => s ?? "/");
        public static List<Func<List<CommandObject>>> GameCMD { get; internal set; } = new List<Func<List<CommandObject>>>();

        /// <summary>
        /// 更新指令提示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textUpdate"></param>
        public static void UpdateTip(string text, bool textUpdate)
        {
            if (CanUse() == false)
            {
                CommandTip.TipList = null;
                CommandTip.TipEx = null;
                return;
            }

            if (textUpdate == false) return;

            try
            {
                CommandTip.TipList = null;
                CommandTip.TipEx = null;
                string cmd = ChatToCMD(text);
                if (cmd == null) return;

                (string textList, string textEx) = GetTip(cmd, GetGameCMD());
                CommandTip.TipList = textList;
                CommandTip.TipEx = textEx;
            }
            catch { }
        }

        /// <summary>
        /// 判断聊天是否是指令, 是指令就返回指令内容
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public static string ChatToCMD(string chat)
        {
            if (chat == null) return null;

            string head = CMDHead.val;
            if (chat.Length < head.Length) return null;

            string chatHead = chat.Substring(0, head.Length);
            if (chatHead != head) return null;

            return chat.Substring(head.Length);
        }

        /// <summary>
        /// 获取游戏内指令
        /// </summary>
        /// <returns></returns>
        public static List<CommandObject> GetGameCMD()
        {
            List<CommandObject> cos = new List<CommandObject>();
            cos.Add(new CommandPrintList(cos, null, s => Main.NewText(s)));

            foreach (Func<List<CommandObject>> i in GameCMD)
            {
                try
                {
                    if (i == null) continue;
                    cos.AddRange(i());
                }
                catch
                {
                    Debug.WriteLine($"{nameof(GameChatCommand)}:获取指令异常, 跳过该指令");
                }
            }

            return cos;
        }

        /// <summary>
        /// 运行指令
        /// </summary>
        /// <param name="text"></param>
        public static void InputCMD(string text)
        {
            if (text == null) return;

            string exText = tContentPatch.Command.Utils.CommandRun(text, GetGameCMD());
            if (exText != null) Main.NewText(exText);
        }

        /// <summary>
        /// 功能启用且在单人或客户端时
        /// </summary>
        /// <returns></returns>
        internal static bool CanUse()
        {
            if (Enable.val == false) return false;
            return Main.netMode == 0 || Main.netMode == 1;
        }

        /// <summary>
        /// 获取指令提示
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cos"></param>
        /// <returns></returns>
        public static (string textList, string textEx) GetTip(string command, List<CommandObject> cos)
        {
            (string textList, string textEx) retV = (string.Empty, string.Empty);

            if (command == null) return retV;

            (List<CommandObject> cos, CommandException cex) v = ParseCommand.Parse(command, cos);
            List<CommandObject> cmdList = v.cos;
            CommandException ex = v.cex;
            bool isCmdLack = false;

            if (ex != null)
            {
                if (ex as CommandLackException != null || ex.InnerException as CommandLackException != null)
                {
                    isCmdLack = true;

                    retV.textEx = $"指令缺失";
                    if (ex.Line > -1) retV.textEx += $", 位于:{command.Substring(0, ex.Line)}<";
                }
                else
                if (ex as CommandParseException != null || ex.InnerException as CommandParseException != null)
                {
                    retV.textEx = $"指令错误";
                    if (ex.Line > -1) retV.textEx += $", 位于: {command.Substring(0, ex.Line)}>{command.Substring(ex.Line)?.TrimStart()}<\n错误的指令:>{ex.ExceptionCommand?.TrimStart()}<";
                }
                else
                {
                    retV.textEx = $"指令错误";
                    if (ex.Line > -1) retV.textEx += $", 位于: {command.Substring(0, ex.Line)}>{command.Substring(ex.Line)?.TrimStart()}<\n{ex.ExceptionMessage}";
                }
            }

            //显示c的子指令
            //c为指令对象列表的最后一个
            //如果指令缺失, 如果最后为空格且不为空文本则不变, 否者不显示
            //如果指令异常, 如果有错误部分的指令则不变, 否者不显示
            //如果没异常, 如果最后为空格, 则c为最后一个非可变参数
            CommandObject c = cmdList.LastOrDefault();
            bool isEndSpace = command?.Length > 0 == true && command[command.Length - 1] == ' ';

            if (isCmdLack)
            {
                if (isEndSpace == false && command != "") return retV;
            }
            else
            if (ex != null)
            {
                int exCmdLen = ex?.ExceptionCommand?.TrimStart().Length ?? 0;
                if (exCmdLen < 1) return retV;
            }
            else
            if (isEndSpace)
            {
                for (int i = cmdList.Count - 1; i > -1; --i)
                {
                    CommandValue cv = cmdList[i] as CommandValue;
                    c = cmdList[i];

                    if (cv == null) break;
                    if (cv.IsDefault == false) break;
                }
            }

            List<CommandObject> coList = (c?.SubCommand) ?? cos;
            string text = null;

            for (int i = 0; i < coList.Count; i++)
            {
                CommandObject subco = coList[i];
                if (subco == null) continue;

                if (subco is CommandeEnum ce)
                {
                    for (int i2 = 0; i2 < ce.Enums.Length; i2++) text += $"{(text == null ? "" : "\n")}> {ce.Enums[i2]}";
                }
                else
                {
                    text += $"{(text == null ? "" : "\n")}> {subco.Text}";
                    if (subco.TipText != null) text += $"//{subco.TipText}";
                }
            }

            retV.textList = text ?? "";

            return retV;
        }
    }
}
