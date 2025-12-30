using System.Collections.Generic;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIPanelEditControl : UIState, IEditControl
    {
        protected readonly List<IEditControl> ecs = new List<IEditControl>();

        protected void AddEditControl(IEditControl ec)
        {
            ecs.Add(ec);
        }

        public virtual void OnEditEnable()
        {
            ecs.ForEach(i => i.OnEditEnable());
        }

        public virtual void OnEditEnableNo()
        {
            ecs.ForEach(i => i.OnEditEnableNo());
        }
    }
}
