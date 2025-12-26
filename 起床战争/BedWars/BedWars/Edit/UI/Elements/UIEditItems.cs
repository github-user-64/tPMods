using System.Collections.Generic;
using tContentPatch.Content.UI;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIEditItems : UIScrollViewer2, IEditControl
    {
        private readonly List<IEditControl> ecs = new List<IEditControl>();

        public UIEditItems()
        {
            Width.Precent = 1;
            Height.Precent = 1;
        }

        public void AddItem(UIElement ui)
        {
            if (ui is IEditControl ec) ecs.Add(ec);

            AddChild(ui);
        }

        public void OnEditEnable()
        {
            ecs.ForEach(i => i.OnEditEnable());
        }

        public void OnEditNoEnable()
        {
            ecs.ForEach(i => i.OnEditNoEnable());
        }
    }
}
