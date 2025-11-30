using Microsoft.Xna.Framework;
using ChatBarCMD.Utils;
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
    }
}
