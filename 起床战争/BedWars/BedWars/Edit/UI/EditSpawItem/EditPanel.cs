using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Edit.UI.EditSpawItem
{
    internal class EditPanel : UIEditPanel
    {
        public EditPanel()
        {
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
        }

        public void UpdateData()
        {
            sv.Deactivate();
            sv.ClearChild();

            List<SpawItemData> datas = EditData.instance.DataSpawItems;
            if (datas == null) return;

            foreach (var i in datas)
            {
                EditItem ui = new EditItem(i, UpdateData, OnItemOpen);

                sv.AddChild(new UIFoldPanel(ui));
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
