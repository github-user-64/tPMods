using CommandHelp;
using CommandHelp.Exceptions;
using ReLogic.OS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Chat;

namespace PlayerAccount.Common.GameChatCommand
{
    internal partial class GameChatCommand
    {
        private static string oldChatText = null;

        public static void DoUpdate_HandleChatPrefix()
        {
            oldChatText = Main.chatText;
        }

        public static void DoUpdate_HandleChatPostfix()
        {
            if (Main.netMode != 0)
            {
                CommandTip.TipList = null;
                CommandTip.TipEx = null;
                return;
            }
            if (oldChatText == Main.chatText) return;

            try
            {
                CommandTip.TipList = null;
                CommandTip.TipEx = null;
                string cmd = ChatToCMD(Main.chatText);
                if (cmd == null) return;

                (string textList, string textEx) = GetTip(cmd, GetGameCO(s => Main.NewText(s)));
                CommandTip.TipList = textList;
                CommandTip.TipEx = textEx;
            }
            catch { }
        }

        public static bool OnProcessIncomingMessage(ChatMessage message, int clientId)
        {
            if (Main.netMode != 0) return true;
            if (clientId != Main.myPlayer) return true;

            string cmd = ChatToCMD(message?.Text);
            if (cmd == null) return true;

            try
            {
                InputCMD(message.Text.Substring(1), s => Main.NewText(s));
            }
            catch { }

            return false;//不调用该方法
        }

        public static string ChatToCMD(string chat)
        {
            if (chat == null) return null;
            if (chat.Length < 1) return null;
            if (chat[0] != '/') return null;

            return chat.Substring(1);
        }

        public static void InputCMD(string text, Action<string> print)
        {
            if (text == null) return;

            string exText = tContentPatch.Command.Utils.CommandRun(text, GetGameCO(print));
            if (exText != null) PlayerGroup.Utils.Utils.PrintTry(exText, print);
        }

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
                }
            }

            retV.textList = text ?? "";

            return retV;
        }
    }
}
