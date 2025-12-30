using BedWars.Edit.UI.Elements;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.EditTeam
{
    internal class EditPanel : UIPanelEditControl
    {
        private UIScrollViewer2 sv = null;

        public EditPanel()
        {
            UIStackPanel sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.Height.Pixels = 20;
            sp.Horizontal = true;
            sp.ItemMargin = 6;
            Append(sp);

            sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Set(-sp.Height.Pixels - 2, 1);
            sv.VAlign = 1;
            sv.ItemMargin = 4;
            Append(sv);
        }

        public void UpdateData()
        {

        }

        public override void OnEditEnable()
        {
            base.OnEditEnable();
            UpdateData();
        }
    }
}
