using ChatBarCMD.Utils;
using CommandHelp;
using CommandHelp.Exceptions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using tContentPatch;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace ChatBarCMD.Common.GameChatCommand
{
    /// <summary>
    /// 指令提示
    /// </summary>
    public class CommandTip : PatchRemadeChatMonitor
    {
        private static string oldChatText = null;

        internal static void DoUpdate_HandleChatPrefix()
        {
            oldChatText = Main.chatText;
        }

        internal static void DoUpdate_HandleChatPostfix()
        {
            UpdateTip(Main.chatText, oldChatText != Main.chatText);
        }

        /// <summary>
        /// 更新指令提示
        /// </summary>
        private static void UpdateTip(string chat, bool textUpdate)
        {
            if (NetMode01.CanUse() == false)
            {
                TipList = null;
                TipEx = null;
                return;
            }

            if (textUpdate == false) return;

            try
            {
                TipList = null;
                TipEx = null;

                string cmd = Utils.ChatToCMD(chat, out bool iss);
                if (cmd == null) return;

                Action<string> print = s => Utils.MainNewTextTry(s, B: 0);

                List<CommandObject> cos = null;
                if (iss) cos = NetMode01.GetServerCMD(print);
                else cos = NetMode01.GetCMD(print);

                (string textList, string textEx) = GetTip(cmd, cos);
                TipList = textList;
                TipEx = textEx;
            }
            catch { }
        }


        /// <summary>
        /// 启用
        /// </summary>
        public static GetSetReset<bool> Enable = new GetSetReset<bool>(true, true);
        /// <summary>
        /// 提示指令列表
        /// </summary>
        public static string TipList = string.Empty;
        /// <summary>
        /// 提示异常信息
        /// </summary>
        public static string TipEx = string.Empty;

        /// <inheritdoc/>
        public override void DrawChatPostfix(bool drawingPlayerChat)
        {
            if (drawingPlayerChat == false) return;
            if (Enable.val == false) return;

            DrawTipList(TipList);
            DrawTipEx(TipEx);
        }

        /// <summary>
        /// 绘制指令提示
        /// </summary>
        /// <param name="text"></param>
        public static void DrawTipList(string text)
        {
            if (text?.Length > 0 == false) return;

            Vector2 size = ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One);
            Vector2 pos = new Vector2(50, Main.screenHeight - 300);
            pos.Y -= size.Y;

            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, text,
                pos, Color.White, 0f, Vector2.Zero, Vector2.One);
        }

        /// <summary>
        /// 绘制异常提示
        /// </summary>
        /// <param name="text"></param>
        public static void DrawTipEx(string text)
        {
            if (text?.Length > 0 == false) return;

            Vector2 pos = new Vector2(50, Main.screenHeight - 300);

            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, text,
                pos, Color.Red, 0f, Vector2.Zero, Vector2.One);
        }

        /// <summary>
        /// 获取指令提示
        /// </summary>
        public static (string textList, string textEx) GetTip(string command, List<CommandObject> cos)
        {
            (string textList, string textEx) retV = (string.Empty, string.Empty);

            if (command == null || cos == null) return retV;

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

                string isVariable = (subco as CommandValue)?.IsVariable == true ? "(可不填)" : string.Empty;

                if (subco is CommandeEnum ce)
                {
                    for (int i2 = 0; i2 < ce.Enums.Length; i2++)
                    {
                        text += $"{(text == null ? "" : "\n")}> {ce.Enums[i2]}";
                        if (i2 < ce.TipTexts?.Length) text += $"//{ce.TipTexts[i2]}{isVariable}";
                    }
                }
                else
                {
                    text += $"{(text == null ? "" : "\n")}> {subco.Text}";
                    if (subco.TipText != null) text += $"//{subco.TipText}{isVariable}";
                }
            }

            retV.textList = text ?? "";

            return retV;
        }
    }
}
