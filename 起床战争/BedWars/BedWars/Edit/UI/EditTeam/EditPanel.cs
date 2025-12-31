using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit.UI.EditTeam
{
    internal class EditPanel : UIEditPanel
    {
        public EditPanel()
        {
            UIImageButton btn1 = new UIImageButton(sp.Height.Pixels, "添加队伍", "Images/UI/Cursor_7");
            btn1.OnLeftClick += (e, s) =>
            {
                string ex = EditData.instance.AddTeam();
                if (ex != null) Main.NewText(ex);

                UpdateData();
            };
            sp.Append(btn1);
        }

        public void UpdateData()
        {
            sv.Deactivate();
            sv.ClearChild();

            List<TeamData> datas = EditData.instance.DataTeams;
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
