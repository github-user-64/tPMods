using BedWars.BedWarsData;
using BedWars.Common.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BedWars.Edit.UI.EditTeam
{
    internal class EditItem : UIFold
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Buff_18", AssetRequestMode.ImmediateLoad);

        private Action OnDataUpdate = null;
        private TeamData data = null;
        private GetSetStringInt gss_team = null;
        //
        private UIStackPanel ui_open = null;
        private UIState ui_close = null;

        public EditItem(TeamData data, Action OnDataUpdate, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;

            gss_team = new GetSetStringInt(() => data.team, v => data.team = v);

            Width.Precent = 1;
        }

        public override UIElement GetUIOpen()
        {
            if (ui_open != null) return ui_open;

            ui_open = new UIStackPanel();
            ui_open.Width.Precent = 1;
            ui_open.ItemMargin = 2;
            ui_open.IsAutoUpdateSize = true;

            UIItemValueSliderUpdate team = new UIItemValueSliderUpdate(gss_team, 0, 5, text: "队伍");
            team.MouseText = "该队伍玩家的队伍";
            team.FloatToString = v =>
            {
                if (v == 0) return "无队";
                if (v == 1) return "[c/cc3333:红队]";
                if (v == 2) return "[c/3bda55:绿队]";
                if (v == 3) return "[c/3b95da:蓝队]";
                if (v == 4) return "[c/f2dd64:黄队]";
                if (v == 5) return "[c/e064f2:粉队]";
                return "-";
            };
            ui_open.Append(team);

            return ui_open;
        }

        public override UIElement GetUIClose()
        {
            if (ui_close != null) return ui_close;

            ui_close = new UIState();
            ui_close.Height.Set(20, 0);

            //ui_close_name = new Terraria.GameContent.UI.Elements.UIText(string.Empty);
            //ui_close_name.Width.Set(-ui_close.Height.Pixels, 1);
            //ui_close_name.Height.Pixels = ui_close.Height.Pixels;
            //ui_close_name.VAlign = 0.5f;
            //ui_close_name.TextOriginY = 0.5f;
            //ui_close_name.TextOriginX = 0;
            //ui_close_name.OnUpdate += _ => ui_close_name.SetText(gss_name.Get() ?? string.Empty);
            //ui_close.Append(ui_close_name);

            //UIImageButton del = new UIImageButton(ui_close.Height.Pixels, "删除", "Images/UI/Cursor_6");
            //del.HAlign = 1;
            //del.VAlign = 0.5f;
            //del.OnClick += () =>
            //{
            //    EditData.instance.DelSpawItem(data);
            //    OnDataUpdate?.Invoke();
            //};
            //ui_close.Append(del);

            return ui_close;
        }
    }
}
