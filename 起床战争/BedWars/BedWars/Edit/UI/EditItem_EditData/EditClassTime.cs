using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class EditTime : UIItemTextBoxUpdate<double>
    {
        public EditTime(string text) : base(null, "时间", -1, null, text)
        {
            Height.Set(25, 0);
            tb.Width.Set(0, 0.5f);
            MouseText = "不小于0则维持时间. 3600=1时";

            gss = new GetSetStringDouble(GetV, SetV);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (EditData.instance.DataInfo == null) return;

            Common.GameAction.Time = EditData.instance.DataInfo.time;
            Common.GameAction.DayTime = EditData.instance.DataInfo.dayTime;
        }

        public override void OnDeactivate()
        {
            Common.GameAction.Time = -1;

            base.OnDeactivate();
        }

        private static double GetV()
        {
            return EditData.instance.DataInfo?.time ?? default;
        }

        private static void SetV(double v)
        {
            if (EditData.instance.DataInfo == null)
            {
                Main.NewText("数据为null");
                return;
            }

            EditData.instance.DataInfo.time = v;
        }
    }

    internal class EditDayTime : UIItemSwitchUpdate
    {
        public EditDayTime(string text) : base(null, null, text)
        {
            MouseText = "是白天, 4:30到7:30";

            gss = new GetSetStringBool(GetV, SetV);
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
