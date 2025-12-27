using System.Collections.Generic;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIPanelEditControl : UIState, IEditControl
    {
        private readonly List<IEditControl> ecs = new List<IEditControl>();

        protected void AddEditControl(IEditControl ec)
        {
            ecs.Add(ec);
        }

        protected void ClearEditControl()
        {
            ecs.Clear();
        }

        public virtual void OnEditEnable()
        {
            ecs.ForEach(i => i.OnEditEnable());
        }

        public virtual void OnEditNoEnable()
        {
            ecs.ForEach(i => i.OnEditNoEnable());
        }
    }
}
