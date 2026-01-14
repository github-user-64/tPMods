using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayVoidHeight : UIItemSwitch
    {
        public DisplayVoidHeight(Texture2D ico = null, string text = null) : base(ico, text)
        {
            Common.GameInterface.OnDraw.Add(DrawVoid);

            SetVal(true);
        }

        private void DrawVoid(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            if (data.Info.voidHeight < 1) return;

            Rectangle rect = data.Info.rect;

            rect.Y += rect.Height - data.Info.voidHeight;
            rect.Height = data.Info.voidHeight;

            Common.DrawUtils.Draw_rectangle(rect, Color.DarkRed * 0.9f, Color.DarkRed * 0.2f, 1);
        }
    }
}
