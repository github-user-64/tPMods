using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayVoidHeight : UIItemSwitch
    {
        public DisplayVoidHeight(string text) : base(null, text)
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

            Rectangle rect = new Rectangle();
            rect.X = data.Info.pos.X;
            rect.Y = data.Info.pos.Y + data.Info.size.Y - 1;
            rect.Width = data.Info.size.X - 1;
            rect.Height = 0;

            rect.Y -= data.Info.voidHeight - 1;
            rect.Height = data.Info.voidHeight - 1;

            Common.DrawUtils.Draw_rectangle(rect, Color.DarkRed * 0.9f, Color.DarkRed * 0.2f, 1);
        }
    }
}
