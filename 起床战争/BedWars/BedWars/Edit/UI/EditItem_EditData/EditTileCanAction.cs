using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditTileCanAction : UIItemSwitchSize
    {
        public EditTileCanAction(string text) : base(text)
        {
            BackColor = BorderColor = Color.DarkBlue;
            BackColor *= 0.2f;

            OnSetSize += r =>
            {
                EditData.instance.TileCanActionSet(r, true);
            };
        }
    }

    internal class EditTileNoCanAction : UIItemSwitchSize
    {
        public EditTileNoCanAction(string text) : base(text)
        {
            BackColor = BorderColor = Color.DarkRed;
            BackColor *= 0.2f;

            OnSetSize += r =>
            {
                EditData.instance.TileCanActionSet(r, false);
            };
        }
    }
}
