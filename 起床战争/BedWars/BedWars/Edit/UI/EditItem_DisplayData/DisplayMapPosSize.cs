using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayMapPosSize : UIItemSwitch
    {
        public DisplayMapPosSize(string text) : base(null, text)
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

            float dis = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 1f - 50;

            DrawUtils.Draw(data.Info.rect, Color.Green, "地图位置", dis);
        }
    }
}
