using BedWars.BedWarsData;
using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace BedWars.Edit.UI.EditSpawItem
{
    internal class EditItemSP : UIPanel
    {
        private EditItem ei = null;

        public EditItemSP(SpawItemData data, Action<UIFold> OnOpen)
        {
            Width.Precent = 1;
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(43, 60, 120);
            SetPadding(6);

            ei = new EditItem(data, OnOpen);
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
        
        private SpawItemData data = null;
        private UIStackPanel sp = null;
        private UIWrapPanel2 types = null;
        private GetSetStringBool gss_exclude = null;
        private GetSetStringString gss_name = null;
        private GetSetStringInt gss_stack = null;
        private GetSetStringInt gss_cd = null;

        public EditItem(SpawItemData data, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.data = data;

            gss_exclude = new GetSetStringBool(() => data.exclude, v => data.exclude = v);
            gss_name = new GetSetStringString(() => data.name, v => data.name = v);
            gss_stack = new GetSetStringInt(() => data.maxStack, v => data.maxStack = v);
            gss_cd = new GetSetStringInt(() => data.cd, v => data.cd = v);

            #region open
            sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.ItemMargin = 2;
            sp.IsAutoUpdateSize = true;

            UIItemSwitchUpdate exclude = new UIItemSwitchUpdate(gss_exclude, ico1.Value);
            exclude.Height.Set(30, 0);
            exclude.MouseText = "排除类型";
            sp.Append(exclude);

            sp.Append(GetUI1("名称", ico4, gss_name));

            sp.Append(GetUI1("最大物品数量", ico2, gss_stack));

            sp.Append(GetUI1("生成间隔", ico3, gss_cd));
            #endregion

            types = new UIWrapPanel2();
            types.Width.Precent = 1;
            sp.Append(types);
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
            return sp;
        }
    }
}
