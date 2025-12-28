using BedWars.BedWarsData;
using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using tContentPatch.Content.UI;
using tContentPatch.Content.UI.ModSet;
using Terraria;
using Terraria.UI;

namespace BedWars.Edit.UI.EditSpawItem
{
    internal class EditItemSP : Terraria.GameContent.UI.Elements.UIPanel
    {
        private EditItem ei = null;

        public EditItemSP(SpawItemData data, Action OnDataUpdate, Action<UIFold> OnOpen)
        {
            Width.Precent = 1;
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(43, 60, 120);
            SetPadding(6);

            ei = new EditItem(data, OnDataUpdate, OnOpen);
            ei.Width.Precent = 1;
            Append(ei);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Height.Pixels = ei.Height.Pixels + PaddingTop + PaddingBottom;
        }
    }

    // 实在想不到有啥好的方法同步ui和数据
    internal class EditItem : UIFold
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Buff_18", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/Item_9", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico3 = Main.Assets.Request<Texture2D>("Images/Item_15", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico4 = Main.Assets.Request<Texture2D>("Images/UI/Workshop/Tags", AssetRequestMode.ImmediateLoad);

        private Action OnDataUpdate = null;
        private SpawItemData data = null;
        private UIStackPanel ui_open = null;
        private EditItemType types = null;
        private UIState ui_close = null;
        private GetSetStringBool gss_exclude = null;
        private GetSetStringString gss_name = null;
        private GetSetStringInt gss_stack = null;
        private GetSetStringInt gss_cd = null;

        public EditItem(SpawItemData data, Action OnDataUpdate, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;

            gss_exclude = new GetSetStringBool(() => data.exclude, v => data.exclude = v);
            gss_name = new GetSetStringString(() => data.name, v => data.name = v);
            gss_stack = new GetSetStringInt(() => data.maxStack, v => data.maxStack = v);
            gss_cd = new GetSetStringInt(() => data.cd, v => data.cd = v);
        }

        public UIElement GetUI1<T>(string t1, Asset<Texture2D> ico, GetSetString<T> gss)
        {
            UIItemTextBoxUpdate<T> ui = new UIItemTextBoxUpdate<T>(gss, t1, -1, ico.Value);
            ui.Height.Set(25, 0);
            ui.tb.Width.Set(-25 - 2, 1);
            ui.MouseText = t1;

            return ui;
        }

        public override UIElement GetUIOpen()
        {
            if (ui_open != null) return ui_open;

            ui_open = new UIStackPanel();
            ui_open.Width.Precent = 1;
            ui_open.ItemMargin = 2;
            ui_open.IsAutoUpdateSize = true;

            UIItemSwitch test = new UIItemSwitch(null, "测试生成");
            test.OnValUpdate += v =>
            {
                if (v == false)
                {
                    Common.SpawItem.SpawDatas.Remove(data);
                    return;
                }

                if (Common.SpawItem.SpawDatas.Contains(data)) return;
                Common.SpawItem.SpawDatas.Add(data);
            };
            ui_open.Append(test);

            UIItemSwitchUpdate exclude = new UIItemSwitchUpdate(gss_exclude, ico1.Value);
            exclude.Height.Set(30, 0);
            exclude.MouseText = "排除类型";
            ui_open.Append(exclude);

            ui_open.Append(GetUI1("名称", ico4, gss_name));

            ui_open.Append(GetUI1("最大物品数量", ico2, gss_stack));

            ui_open.Append(GetUI1("生成间隔", ico3, gss_cd));

            UIStackPanel sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.Height.Pixels = 25;
            sp.ItemMargin = 4;
            sp.Horizontal = true;
            ui_open.Append(sp);

            UIImageButton del = new UIImageButton(sp.Height.Pixels - 2, "删除", "Images/UI/Cursor_6");
            del.VAlign = 0.5f;
            del.OnClick += () =>
            {
                EditData.instance.DelSpawItem(data);
                OnDataUpdate?.Invoke();
            };
            sp.Append(del);

            UIImageButton tp = new UIImageButton(sp.Height.Pixels - 2, "传送到此", "Images/UI/SpawnPoint");
            tp.VAlign = 0.5f;
            tp.OnClick += () =>
            {
                Vector2 pos = data.pos.ToWorldCoordinates();
                if (WorldGen.InWorld(data.pos.X, data.pos.Y) == false)
                {
                    Main.NewText($"超出世界:{pos.X},{pos.Y}");
                    return;
                }
                Main.LocalPlayer.Center = pos;
            };
            sp.Append(tp);

            UIImageButton addType = new UIImageButton(sp.Height.Pixels - 2, "添加物品类型,右键删除", "Images/Item_27");
            addType.VAlign = 0.5f;
            addType.OnClick += () => types.AddType();
            sp.Append(addType);

            types = new EditItemType(data);
            types.Width.Precent = 1;
            ui_open.Append(types);

            return ui_open;
        }

        public override void Open()
        {
            if (IsOpen) return;
            base.Open();

            types.UpdateData();
        }

        public override UIElement GetUIClose()
        {
            if (ui_close != null) return ui_close;

            ui_close = new UIState();
            ui_close.Height.Set(25, 0);

            Terraria.GameContent.UI.Elements.UIText name = new Terraria.GameContent.UI.Elements.UIText(string.Empty);
            name.Width.Pixels = 20;
            name.Height.Pixels = ui_close.Height.Pixels - 2;
            name.VAlign = 0.5f;
            name.TextOriginY = 0.5f;
            name.OnUpdate += _ => name.SetText(gss_name.Get() ?? string.Empty);
            ui_close.Append(name);

            UIImageButton del = new UIImageButton(ui_close.Height.Pixels - 2, "删除", "Images/UI/Cursor_6");
            del.HAlign = 1;
            del.VAlign = 0.5f;
            del.OnClick += () =>
            {
                EditData.instance.DelSpawItem(data);
                OnDataUpdate?.Invoke();
            };
            ui_close.Append(del);

            return ui_close;
        }
    }
}
