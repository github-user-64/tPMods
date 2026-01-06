using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayShop : UIItemSwitch
    {
        public DisplayShop(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawTeam);

            SetVal(true);
        }

        private void DrawTeam(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            float dis = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 0.6f - 50;

            foreach (var i in data.Shops)
            {
                DrawUtils.Draw(data.Info.pos, i.pos, Color.Pink, $"{i.name}商店", dis);
            }
        }
    }
}
