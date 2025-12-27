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
    // 实在想不到有啥好的方法同步ui和数据
    internal class EditItem : UIPanel
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Buff_18", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/Item_9", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico3 = Main.Assets.Request<Texture2D>("Images/Item_15", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico4 = Main.Assets.Request<Texture2D>("Images/UI/Workshop/Tags", AssetRequestMode.ImmediateLoad);
        
        private SpawItemData data = null;
        private UIStackPanel sp = null;
        private UIStackPanel sp2 = null;
        private UIWrapPanel2 types = null;

        public EditItem(SpawItemData data)
        {
            this.data = data;

            Width.Precent = 1;
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(43, 60, 120);

            SetPadding(6);

            sp = new UIStackPanel();
            sp.Width.Precent = 1;
            sp.IsAutoUpdateSize = true;
            Append(sp);

            #region
            sp2 = new UIStackPanel();
            sp2.Width.Precent = 1;
            sp2.ItemMargin = 2;
            sp2.IsAutoUpdateSize = true;
            sp.Append(sp2);

            UIItemSwitchUpdate exclude = new UIItemSwitchUpdate(
                () => data.exclude,
                v => data.exclude = v,
                ico1.Value);
            exclude.Height.Set(30, 0);
            exclude.MouseText = "排除类型";
            sp2.Append(exclude);

            sp2.Append(GetUI1("名称", ico4,
                () => data.name,
                v => data.name = v));

            sp2.Append(GetUI1("最大物品数量", ico2,
                () => data.maxStack.ToString(),
                v => { int.TryParse(v, out data.maxStack); }));

            sp2.Append(GetUI1("生成间隔", ico3,
                () => data.cd.ToString(),
                v => { int.TryParse(v, out data.cd); }));
            #endregion

            types = new UIWrapPanel2();
            types.Width.Precent = 1;
            sp.Append(types);
        }

        public UIElement GetUI1(string t1, Asset<Texture2D> ico, Func<string> GetV, Action<string> SetV)
        {
            UIItemTextBoxUpdate ui = new UIItemTextBoxUpdate(
                GetV,
                SetV,
                t1, -1, ico.Value);
            ui.Height.Set(25, 0);
            ui.tb.Width.Set(-25 - 2, 1);
            ui.MouseText = t1;

            return ui;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Height.Pixels = sp.Height.Pixels + PaddingTop + PaddingBottom;
        }
    }
}
