using System.Collections.Generic;
using tContentPatch.Content.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIWindowEditControl : UIWindow, IEditControl
    {
        protected readonly List<IEditControl> ecs = new List<IEditControl>();

        public UIWindowEditControl(string title, int width, int height) : base(title, width, height)
        {
        }

        protected void AddEditControl(IEditControl ec)
        {
            ecs.Add(ec);
        }

        public void OnEditEnable()
        {
            ecs.ForEach(i => i.OnEditEnable());
        }

        public virtual void OnEditEnableNo()
        {
            ecs.ForEach(i => i.OnEditEnableNo());
        }
    }
}
