using Microsoft.Xna.Framework;
using PlayerAccount.Utils;
using tContentPatch;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace PlayerAccount.Common.GameChatCommand
{
    public class CommandTip : PatchRemadeChatMonitor
    {
        public static GetSetReset<bool> Enable = new GetSetReset<bool>(true, true);
        public static string TipList = string.Empty;
        public static string TipEx = string.Empty;

        public override void DrawChatPostfix(bool drawingPlayerChat)
        {
            if (drawingPlayerChat == false) return;
            if (Enable.val == false) return;

            DrawTipList(TipList);
            DrawTipEx(TipEx);
        }

        private static void DrawTipList(string text)
        {
            if (text?.Length > 0 == false) return;

            Vector2 size = ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One);
            Vector2 pos = new Vector2(50, Main.screenHeight - 300);
            pos.Y -= size.Y;

            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, text,
                pos, Color.White, 0f, Vector2.Zero, Vector2.One);
        }

        private static void DrawTipEx(string text)
        {
            if (text?.Length > 0 == false) return;

            Vector2 pos = new Vector2(50, Main.screenHeight - 300);

            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, text,
                pos, Color.Red, 0f, Vector2.Zero, Vector2.One);
        }
    }
}
