using tContentPatch.Content.UI;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIPanelSVEditControl : UIPanelEditControl, IEditControl
    {
        private readonly UIScrollViewer2 sv = new UIScrollViewer2();

        public UIPanelSVEditControl()
        {
            sv.Width.Precent = 1;
            sv.Height.Precent = 1;

            Append(sv);
        }

        public void AddItem(UIElement ui)
        {
            if (ui is IEditControl ec) AddEditControl(ec);

            sv.AddChild(ui);
        }
    }
}
