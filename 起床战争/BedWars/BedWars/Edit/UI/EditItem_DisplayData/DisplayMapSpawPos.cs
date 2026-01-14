using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayMapSpawPos : UIItemSwitch
    {
        public DisplayMapSpawPos(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawSpawPos);

            SetVal(true);
        }

        private void DrawSpawPos(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;

            float dis = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 0.5f - 50;

            DrawUtils.Draw(data.Info.Pos, data.Info.spawPos, Color.RosyBrown, "重生点", dis);
        }
    }
}
