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

namespace BedWars.Edit.UI.EditShop
{
    internal class EditItem : UIFold
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/UI/Workshop/Tags", AssetRequestMode.ImmediateLoad);

        private Action OnDataUpdate = null;
        private MapData mapData = null;
        private ShopData data = null;
        private GetSetStringString gss_name = null;
        //
        private UIStackPanel ui_open = null;
        private UIState ui_close = null;

        public EditItem(MapData mapData, ShopData data, Action OnDataUpdate, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.mapData = mapData;
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;

            gss_name = new GetSetStringString(() => data.name, v => data.name = v);

            Width.Precent = 1;
        }

        public override UIElement GetUIOpen()
        {
            if (ui_open != null) return ui_open;

            ui_open = new UIStackPanel();
            ui_open.Width.Precent = 1;
            ui_open.ItemMargin = 2;
            ui_open.IsAutoUpdateSize = true;

            ui_open.Append(Build1.ItemTextBoxUpdate("名称", ico1, gss_name));

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
                EditData.instance.ShopDel(data);
                OnDataUpdate?.Invoke();
            };
            sp.Append(del);

            UIImageButtonSwitchPos setPos = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "商店npc左上角位置,右键传送,npc大小为2*3", "Images/UI/SpawnPoint");
            setPos.OnSetPos = v =>
            {
                string ex = EditData.instance.ShopSetPos(data, v);
                if (ex != null) Main.NewText(ex);
            };
            setPos.OnRightClick += (e, s) =>
            {
                EditData.instance.Tp(data.pos);
            };
            sp.Append(setPos);

            return ui_open;
        }

        public override UIElement GetUIClose()
        {
            if (ui_close == null) ui_close = Build1.FoldCloseUI(gss_name, () =>
            {
                EditData.instance.ShopDel(data);
                OnDataUpdate?.Invoke();
            });

            return ui_close;
        }
    }
}
