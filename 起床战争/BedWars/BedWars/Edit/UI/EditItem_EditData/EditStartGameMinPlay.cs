using BedWars.Common.UI;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditStartGameMinPlay : UIItemTextBoxUpdate<int>
    {
        public EditStartGameMinPlay(string text) : base(null, "玩家数量", -1, null, text)
        {
            Height.Set(25, 0);
            tb.Width.Set(0, 0.5f);
            MouseText = "开始游戏所需最小玩家数";

            gss = new GetSetStringInt(GetV, SetV);
        }

        private static int GetV()
        {
            return EditData.instance.DataInfo?.startGameMinPlay ?? default;
        }

        private static void SetV(int v)
        {
            if (EditData.instance.DataInfo == null)
            {
                Main.NewText("数据为null");
                return;
            }

            EditData.instance.DataInfo.startGameMinPlay = v;
        }
    }
}
