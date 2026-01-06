using BedWars.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal static class Build1
    {
        public static UIItemTextBoxUpdate<T> ItemTextBoxUpdate<T>(string t1, Asset<Texture2D> ico, GetSetString<T> gss)
        {
            UIItemTextBoxUpdate<T> ui = new UIItemTextBoxUpdate<T>(gss, t1, -1, ico.Value);
            ui.Height.Set(25, 0);
            ui.tb.Width.Set(-25 - 2, 1);
            ui.MouseText = t1;

            return ui;
        }

        public static UIState FoldCloseUI(GetSetStringString gss_name, Action onDel, Action onMouseHovering = null)
        {
            UIState ui_close = new UIState();
            ui_close.Height.Set(20, 0);

            Terraria.GameContent.UI.Elements.UIText ui_close_name = new Terraria.GameContent.UI.Elements.UIText(string.Empty);
            ui_close_name.Width.Set(-ui_close.Height.Pixels, 1);
            ui_close_name.Height.Pixels = ui_close.Height.Pixels;
            ui_close_name.VAlign = 0.5f;
            ui_close_name.TextOriginY = 0.5f;
            ui_close_name.TextOriginX = 0;
            ui_close_name.OnUpdate += _ =>
            {
                ui_close_name.SetText(gss_name.Get() ?? string.Empty);

                if (ui_close_name.IsMouseHovering) onMouseHovering?.Invoke();
            };
            ui_close.Append(ui_close_name);

            UIImageButton del = new UIImageButton(ui_close.Height.Pixels, "删除", "Images/UI/Cursor_6");
            del.HAlign = 1;
            del.VAlign = 0.5f;
            del.OnClick += () => onDel?.Invoke();
            ui_close.Append(del);

            return ui_close;
        }
    }
}
