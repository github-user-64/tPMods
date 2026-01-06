using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.UI;

namespace BedWars.Edit.UI.EditTeam
{
    internal class EditItem : UIFold
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Buff_135", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/UI/Workshop/Tags", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico3 = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/ColorSkin", AssetRequestMode.ImmediateLoad);

        private Action OnDataUpdate = null;
        private MapData mapData = null;
        private TeamData data = null;
        private GetSetStringBool gss_canSpaw = null;
        private GetSetStringInt gss_team = null;
        private GetSetStringString gss_name = null;
        private GetSetStringInt gss_maxPlay = null;
        //
        private UIStackPanel ui_open = null;
        private UIState ui_close = null;

        public EditItem(MapData mapData, TeamData data, Action OnDataUpdate, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.mapData = mapData;
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;

            gss_canSpaw = new GetSetStringBool(() => data.canSpaw, v => data.canSpaw = v);
            gss_team = new GetSetStringInt(() => data.team, v => data.team = v);
            gss_name = new GetSetStringString(() => data.name, v => data.name = v);
            gss_maxPlay = new GetSetStringInt(() => data.maxPlay, v => data.maxPlay = v);

            Width.Precent = 1;
        }

        public override UIElement GetUIOpen()
        {
            if (ui_open != null) return ui_open;

            ui_open = new UIStackPanel();
            ui_open.Width.Precent = 1;
            ui_open.ItemMargin = 2;
            ui_open.IsAutoUpdateSize = true;

            UIItemSwitchUpdate canSpaw = new UIItemSwitchUpdate(gss_canSpaw, ico1.Value, "能否重生");
            canSpaw.MouseText = "该队伍的玩家能否重生";
            ui_open.Append(canSpaw);

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

            ui_open.Append(Build1.ItemTextBoxUpdate("名称", ico2, gss_name));

            ui_open.Append(Build1.ItemTextBoxUpdate("最大玩家数量", ico3, gss_maxPlay));

            UIStackPanel sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.Height.Pixels = 25;
            sp.ItemMargin = 4;
            sp.Horizontal = true;
            ui_open.Append(sp);

            UIImageButton del = new UIImageButton(sp.Height.Pixels, "删除", "Images/UI/Cursor_6");
            del.VAlign = 0.5f;
            del.OnClick += () =>
            {
                EditData.instance.TeamDel(data);
                OnDataUpdate?.Invoke();
            };
            sp.Append(del);

            UIImageButtonSwitchPos setSpawTilePos = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "重生方块位置,右键传送", "Images/UI/SpawnBed");
            setSpawTilePos.OnSetPos = v =>
            {
                string ex = EditData.instance.TeamSpawTileSetPos(data, v);
                if (ex != null) Main.NewText(ex);
            };
            setSpawTilePos.OnRightClick += (e, s) =>
            {
                EditData.instance.Tp(data.spawTilePos);
            };
            sp.Append(setSpawTilePos);

            UIImageButtonSwitchPos setSpawPos = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "玩家重生位置,右键传送", "Images/UI/SpawnPoint");
            setSpawPos.OnSetPos = v =>
            {
                string ex = EditData.instance.TeamSpawSetPos(data, v);
                if (ex != null) Main.NewText(ex);
            };
            setSpawPos.OnRightClick += (e, s) =>
            {
                EditData.instance.Tp(data.spawPos);
            };
            sp.Append(setSpawPos);

            return ui_open;
        }

        public override UIElement GetUIClose()
        {
            if (ui_close == null) ui_close = Build1.FoldCloseUI(gss_name, () =>
            {
                EditData.instance.TeamDel(data);
                OnDataUpdate?.Invoke();
            });

            return ui_close;
        }
    }
}
