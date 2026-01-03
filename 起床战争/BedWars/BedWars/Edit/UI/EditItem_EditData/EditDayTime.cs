using BedWars.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditDayTime : UIItemSwitchUpdate
    {
        private static GetSetStringBool gss = new GetSetStringBool(GetV, SetV);

        public EditDayTime(string text) : base(gss, null, text)
        {
            MouseText = "是白天, 4:30到7:30";
        }

        private static bool GetV()
        {
            return EditData.instance.DataInfo?.dayTime ?? default;
        }

        private static void SetV(bool v)
        {
            if (EditData.instance.DataInfo == null)
            {
                Main.NewText("数据为null");
                return;
            }

            EditData.instance.DataInfo.dayTime = v;
        }
    }
}
