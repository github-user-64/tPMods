using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplaySpawItem : UIItemSwitch
    {
        public DisplaySpawItem(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawMapPosSize);
        }

        private void DrawMapPosSize(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            foreach (SpawItemData i in data.SpawItems)
            {
                Common.DrawUtils.Draw_rectangle(i.pos, i.pos, Color.LawnGreen * 0.9f, Color.LawnGreen * 0.2f, 1);

                string text = i.name;
                if (text == null) continue; 
                if (text == string.Empty) continue; 

                Vector2 textP = i.pos.ToWorldCoordinates();
                textP -= new Vector2(16) / 2;
                Vector2 v = textP - Main.LocalPlayer.Center;
                if (v.Length() > 300)
                {
                    v = Vector2.Normalize(v) * 300;
                    textP = Main.LocalPlayer.Center + v;
                }

                Utils.DrawBorderString(spriteBatch, text, textP - Main.screenPosition, Color.Green, anchorx: 0.5f, anchory: 0.25f);
            }
        }
    }
}
