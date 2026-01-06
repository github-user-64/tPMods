using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit.UI.EditShop
{
    internal class EditPanel : UIEditPanel
    {
        public EditPanel()
        {
            UIImageButtonSwitchPos btn1 = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "添加商店", "Images/UI/Cursor_7");
            btn1.OnSetPos = v =>
            {
                string ex = EditData.instance.ShopAdd(v);
                if (ex != null) Main.NewText(ex);

                UpdateData();
            };
            sp.Append(btn1);
        }

        public void UpdateData()
        {
            sv.Deactivate();
            sv.ClearChild();

            List<ShopData> datas = EditData.instance.DataShops;
            if (datas == null) return;

            foreach (var i in datas)
            {
                EditItem ui = new EditItem(EditData.instance.Data, i, UpdateData, OnItemOpen);

                sv.AddChild(new UIFoldPanel(ui));
                ui.Activate();
            }
        }

        public override void OnActivate()
        {
            base.OnActivate();

            UpdateData();
        }

        private UIFold _openitem = null;
        private void OnItemOpen(UIFold ui)
        {
            _openitem?.Close();
            _openitem = ui;
        }
    }
}
