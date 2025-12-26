using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace BedWars.Edit.UI.EditWindowSpawItem
{
    internal class EditWindow : UIWindowEditControl
    {
        private UIScrollViewer2 sv = null;

        public EditWindow(string title, int width, int height) : base(title, width, height)
        {
            UIStackPanel sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.Height.Pixels = 20;
            sp.Horizontal = true;
            sp.ItemMargin = 6;
            Child.Append(sp);

            UIImageButton btn1 = new UIImageButton(sp.Height.Pixels, "清除掉落物", "Images/UI/Cursor_6");
            btn1.OnLeftClick += (e, s) =>
            {
                if (Main.item == null) return;
                for (int i = 0; i < Main.item.Length; ++i)
                {
                    Main.item[i]?.SetDefaults(ItemID.None);
                }
            };
            sp.Append(btn1);

            UIImageButtonSwitchPos btn2 = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "添加生成", "Images/UI/Cursor_7");
            btn2.OnSetPos = v =>
            {
                string ex = EditData.instance.AddSpawItem(v);
                if (ex != null) Main.NewText(ex);

                UpdateData();
            };
            sp.Append(btn2);

            sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Set(-sp.Height.Pixels - 2, 1);
            sv.VAlign = 1;
            Child.Append(sv);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering) Main.LocalPlayer.mouseInterface = true;

            base.Update(gameTime);
        }

        public override void Open(UIElement windowParent)
        {
            base.Open(windowParent);
            UpdateData();
        }

        public override void OnEditNoEnable()
        {
            base.OnEditNoEnable();
            Close();
        }

        private void UpdateData()
        {

        }
    }
}
