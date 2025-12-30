using tContentPatch.Content.UI;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIEditPanel : UIState
    {
        /// <summary>
        /// 横的
        /// </summary>
        protected UIStackPanel sp = null;
        /// <summary>
        /// 竖的
        /// </summary>
        protected UIScrollViewer2 sv = null;

        public UIEditPanel()
        {
            sp = new UIStackPanel();
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
    }
}
