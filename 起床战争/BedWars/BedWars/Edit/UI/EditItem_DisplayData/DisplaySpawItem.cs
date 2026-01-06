using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplaySpawItem : UIItemSwitch
    {
        public DisplaySpawItem(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawMapPosSize);

            SetVal(true);
        }

        private void DrawMapPosSize(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            float dis = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 0.7f - 50;

            foreach (var i in data.SpawItems)
            {
                DrawUtils.Draw(data.Info.pos, i.pos, Color.LawnGreen, i.name, dis);
            }
        }
    }
}
