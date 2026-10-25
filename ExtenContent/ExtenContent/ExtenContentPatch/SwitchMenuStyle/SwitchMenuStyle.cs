using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch;
using tContentPatch.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace ExtenContentPatch
{
    internal class SwitchMenuStyle : PatchMain
    {
        protected UserInterface ui = null;
        protected UIState state = null;
        protected UIText text = null;

        public override void DrawMenuPrefix(GameTime gameTime)
        {
            if (ui == null) return;

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);

            ui.Draw(Main.spriteBatch, gameTime);

            Main.spriteBatch.End();
        }

        public override void UpdateUIStatesPostfix(GameTime gameTime)
        {
            if (ui == null)
            {
                UserInterface ui = new UserInterface();
                state = new UIState();

                text = new UIText("主菜单样式切换按钮");
                text.HAlign = 0.5f;
                text.VAlign = 1f;
                text.MarginBottom = 10;
                text.OnMouseOver += (e, s) => SoundEngine.PlaySound(12);
                text.OnLeftClick += (e, s) =>
                {
                    SoundEngine.PlaySound(12);

                    NextStyle();
                };
                state.Append(text);

                this.ui = ui;
            }

            int count = MenuStyleLoader.StyleCount - MenuStyleLoader.OriginCount;
            if (count < 1 || Main.menuMode != MenuID.Title)
            {
                ui.SetState(null);
                return;
            }
            ui.SetState(state);
            ui.Update(gameTime);

            text.TextColor = text.IsMouseHovering ? Colors.FancyUIFatButtonMouseOver : Color.White * 0.8f;
            text.ShadowColor = text.IsMouseHovering ? Color.Black : Color.Black * 0.8f;

            //

            ExtenMenuStyle style = MenuStyleLoader.CurrentStyle;

            if (style == null) text.SetText($"{count}个新样式");
            else text.SetText(GetDiaplayName(style));
        }

        public override void SaveSettingsPostfix()
        {
            Save();
        }

        public static void Save()
        {
            string key = MenuStyleLoader.CurrentStyle?.FullName;

            ModFile.SaveFileTry("MenuStyle.txt", path =>
            {
                MyJson1.Save(key, path);
                return true;
            }, ThisMod.mo);
        }

        public static void Load()
        {
            ModFile.ReadFileTry("MenuStyle.txt", path =>
            {
                string key = MyJson1.Get2<string>(path);

                MenuStyleLoader.SetStyle(key);
                return true;
            }, ThisMod.mo);
        }

        private static void NextStyle()
        {
            int style;
            ExtenMenuStyle now = MenuStyleLoader.CurrentStyle;

            if (now == null) style = MenuStyleLoader.OriginCount;
            else style = now.Type + 1;

            if (MenuStyleLoader.IsExten(style) != true) style = MenuStyleLoader.OriginCount - 1;

            MenuStyleLoader.SetStyle(MenuStyleLoader.GetExtenStyle(style)?.FullName);
        }

        private static string GetDiaplayName(ExtenMenuStyle style)
        {
            string name = style.DisplayName ?? style.Name;
            if (name == null) return "未知样式名";

            name = name.Trim();
            if (name.Length < 1) return "未知样式名";

            int max = 40;
            if (name.Length > max) name = name.Substring(0, max) + "...";

            return name;
        }
    }
}
