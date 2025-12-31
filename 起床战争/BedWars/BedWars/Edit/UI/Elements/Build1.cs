using BedWars.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal static class Build1
    {
        public static UIElement ItemTextBoxUpdate<T>(string t1, Asset<Texture2D> ico, GetSetString<T> gss)
        {
            UIItemTextBoxUpdate<T> ui = new UIItemTextBoxUpdate<T>(gss, t1, -1, ico.Value);
            ui.Height.Set(25, 0);
            ui.tb.Width.Set(-25 - 2, 1);
            ui.MouseText = t1;

            return ui;
        }
    }
}
