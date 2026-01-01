using System.Collections.Generic;
using tContentPatch.Content.UI;
using Terraria.UI;

namespace BedWars.Edit.UI.Elements
{
    internal class UIWindowEditControl : UIWindow, IEditControl
    {
        protected readonly List<IEditControl> ecs = new List<IEditControl>();
        private readonly UIElement P = null;

        public UIWindowEditControl(UIElement P, string title, int width, int height) : base(title, width, height)
        {
            this.P = P;
        }

        protected void AddEditControl(IEditControl ec)
        {
            ecs.Add(ec);
        }

        public virtual void OnEditEnable()
        {
            ecs.ForEach(i => i.OnEditEnable());
            Open(P);
        }

        public virtual void OnEditEnableNo()
        {
            ecs.ForEach(i => i.OnEditEnableNo());
            Close();
        }

        public override void Close()
        {
            Deactivate();
            base.Close();
        }

        public override void Open(UIElement windowParent)
        {
            base.Open(windowParent);
            Activate();
        }
    }
}
