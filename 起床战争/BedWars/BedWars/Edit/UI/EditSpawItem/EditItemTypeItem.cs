using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using System;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace BedWars.Edit.UI.EditSpawItem
{
    internal class EditItemTypeItem : UIPanel
    {
        private Action OnDataUpdate = null;
        private SpawItemData data = null;
        private int index = -1;
        private UITextBox tb = null;

        public EditItemTypeItem(SpawItemData data, Action OnDataUpdate, int index)
        {
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;
            this.index = index;

            Width.Pixels = 70;
            Height.Pixels = 28;
            PaddingLeft = PaddingRight = 8;
            PaddingTop = PaddingBottom = 4;
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(43, 60, 120);

            tb = new UITextBox();
            tb.Width.Precent = 1;
            tb.Height.Precent = 1;
            tb.Text_MaxLength = 6;
            tb.OnLostFocus += () =>
            {
                if (int.TryParse(tb.Text, out int type) == false) return;
                if (GetType(out _) == false) return;

                data.types[index] = type;
            };
            Append(tb);

            OnRightClick += (e, s) => DelType();
        }

        public bool GetType(out int type)
        {
            if (index < 0 || index >= data.types.Count)
            {
                type = 0;
                OnDataUpdate?.Invoke();
                return false;
            }

            type = data.types[index];
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GetType(out int type) == false) return;

            if (IsMouseHovering)
            {
                Item item = new Item();
                item.SetDefaults(type);
                Main.instance.MouseText($"[i:{type}]{item.Name}");
            }

            if (tb.Focus == false) tb.SetText(type.ToString());
        }

        public void DelType()
        {
            if (GetType(out _) == false) return;

            data.types.RemoveAt(index);

            OnDataUpdate?.Invoke();
        }
    }
}
