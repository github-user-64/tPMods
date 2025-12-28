using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using System.Collections.Generic;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.ID;

namespace BedWars.Edit.UI.EditSpawItem
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
            AddEditControl(btn2);

            sv = new UIScrollViewer2();
            sv.Width.Precent = 1;
            sv.Height.Set(-sp.Height.Pixels - 2, 1);
            sv.VAlign = 1;
            sv.ItemMargin = 4;
            Append(sv);
        }

        public void UpdateData()
        {
            sv.ClearChild();

            Common.SpawItem.SpawDatas.Clear();

            List<SpawItemData> sis = EditData.instance.DataSpawItems;
            if (sis == null) return;

            foreach (SpawItemData si in sis)
            {
                EditItemSP ui = new EditItemSP(si, UpdateData, OnItemOpen);

                sv.AddChild(ui);
            }
        }

        private UIFold _openitem = null;
        private void OnItemOpen(UIFold ui)
        {
            _openitem?.Close();
            _openitem = ui;
        }
    }
}
