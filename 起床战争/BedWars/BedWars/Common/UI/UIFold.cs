using System;
using tContentPatch.Content.UI;
using Terraria.UI;

namespace BedWars.Common.UI
{
    internal class UIFold : UIStackPanel
    {
        public bool IsOpen { get; protected set; } = false;
        private readonly Action<UIFold> OnOpen = null;

        public UIFold (Action<UIFold> OnOpen)
        {
            this.OnOpen = OnOpen;

            MinHeight.Pixels = 20;
            MinWidth.Pixels = 20;
            IsAutoUpdateSize = true;

            OnLeftClick += (e, s) => Open();
        }

        public override void OnInitialize()
        {
            SetUI(GetUIClose());
        }

        public virtual UIElement GetUIOpen()
        {
            return null;
        }

        public virtual UIElement GetUIClose()
        {
            return null;
        }

        public virtual void SetUI(UIElement ui)
        {
            Elements.ForEach(i => i.Deactivate());
            RemoveAllChildren();

            if (ui == null) return;
            Append(ui);
            ui.Activate();
        }

        public virtual void Open()
        {
            if (IsOpen) return;
            IsOpen = true;

            OnOpen?.Invoke(this);

            SetUI(GetUIOpen());
        }

        public virtual void Close()
        {
            if (IsOpen == false) return;
            IsOpen = false;

            SetUI(GetUIClose());
        }
    }
}
