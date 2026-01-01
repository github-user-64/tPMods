using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    internal class DisplayTeam : UIItemSwitch
    {
        public DisplayTeam(string text) : base(null, text)
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

            foreach (var i in data.Teams)
            {
                DrawUtils.Draw(data.Info.pos, i.spawTilePos, Color.BlueViolet, $"{i.name}队方块", 300 - 25);

                DrawUtils.Draw(data.Info.pos, i.spawPos, Color.LightBlue, $"{i.name}队重生", 300);
            }
        }
    }
}
