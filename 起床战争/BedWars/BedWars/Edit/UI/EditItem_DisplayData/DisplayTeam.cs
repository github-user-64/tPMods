using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayTeam : UIItemSwitch
    {
        public DisplayTeam(Texture2D ico = null, string text = null) : base(ico, text)
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

            float dis = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 0.9f - 50;
            float dis2 = Math.Min(Main.screenWidth, Main.screenHeight) / 2f * 0.8f - 50;

            Point pos = data.Info.Pos;

            foreach (var i in data.Teams)
            {
                Rectangle rect = i.spawTile;
                rect.X += pos.X;
                rect.Y += pos.Y;

                DrawUtils.Draw(rect, Color.BlueViolet, $"{i.name}队方块", dis);

                //

                DrawUtils.Draw(pos, i.spawPos, Color.Wheat, $"{i.name}队重生", dis2);
            }
        }
    }
}
