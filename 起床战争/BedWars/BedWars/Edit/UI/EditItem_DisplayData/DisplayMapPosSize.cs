using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayMapPosSize : UIItemSwitch
    {
        public DisplayMapPosSize(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawMapPosSize);
        }

        private void DrawMapPosSize(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            Point sizePos = data.Info.pos;
            sizePos.X += data.Info.size.X - 1;
            sizePos.Y += data.Info.size.Y - 1;
            Common.DrawUtils.Draw_rectangle(data.Info.pos, sizePos, Color.LawnGreen * 0.9f, Color.LawnGreen * 0.2f, 1);

            string text = "地图位置";

            Vector2 textP = new Point(data.Info.pos.X - 1, data.Info.pos.Y - 1).ToWorldCoordinates();
            textP += data.Info.size.ToWorldCoordinates() / 2;
            Vector2 v = textP - Main.LocalPlayer.Center;
            if (v.Length() > 200)
            {
                v = Vector2.Normalize(v) * 200;
                textP = Main.LocalPlayer.Center + v;
            }

            Utils.DrawBorderString(spriteBatch, text, textP - Main.screenPosition, Color.Green, anchorx: 0.5f, anchory: 0.25f);
        }
    }
}
