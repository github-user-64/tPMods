using BedWars.BedWarsData;
using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using tContentPatch.Content.UI;
using tContentPatch.Content.UI.ModSet;
using Terraria;
using Terraria.UI;

namespace BedWars.Edit.UI.EditSpawItem
{
    // 实在想不到有啥好的方法同步ui和数据
    internal class EditItem : UIFold
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/Buff_18", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico2 = Main.Assets.Request<Texture2D>("Images/Item_9", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico3 = Main.Assets.Request<Texture2D>("Images/Item_15", AssetRequestMode.ImmediateLoad);
        private static Asset<Texture2D> ico4 = Main.Assets.Request<Texture2D>("Images/UI/Workshop/Tags", AssetRequestMode.ImmediateLoad);

        private Action OnDataUpdate = null;
        private MapData mapData = null;
        private SpawItemData data = null;
        private GetSetStringBool gss_exclude = null;
        private GetSetStringString gss_name = null;
        private GetSetStringInt gss_stack = null;
        private GetSetStringInt gss_cd = null;
        //
        private UIStackPanel ui_open = null;
        private EditItemType types = null;
        private UIState ui_close = null;

        public EditItem(MapData mapData, SpawItemData data, Action OnDataUpdate, Action<UIFold> OnOpen) : base(OnOpen)
        {
            this.mapData = mapData;
            this.data = data;
            this.OnDataUpdate = OnDataUpdate;

            gss_exclude = new GetSetStringBool(() => data.exclude, v => data.exclude = v);
            gss_name = new GetSetStringString(() => data.name, v => data.name = v);
            gss_stack = new GetSetStringInt(() => data.maxStack, v => data.maxStack = v);
            gss_cd = new GetSetStringInt(() => data.cd, v => data.cd = v);

            Width.Precent = 1;
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
                    Common.GameAction.SpawItem.Remove(data);
                    return;
                }

                if (Common.GameAction.SpawItem.Contains(data)) return;
                Common.GameAction.SpawItem.Add(data);
            };
            ui_open.Append(test);

            UIItemSwitchUpdate exclude = new UIItemSwitchUpdate(gss_exclude, ico1.Value, "排除类型");
            exclude.Height.Set(30, 0);
            exclude.MouseText = "排除类型";
            ui_open.Append(exclude);

            ui_open.Append(Build1.ItemTextBoxUpdate("名称", ico4, gss_name));

            ui_open.Append(Build1.ItemTextBoxUpdate("最大物品数量", ico2, gss_stack));

            ui_open.Append(Build1.ItemTextBoxUpdate("生成间隔", ico3, gss_cd));

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
                EditData.instance.SpawItemDel(data);
                OnDataUpdate?.Invoke();
            };
            sp.Append(del);

            UIImageButtonSwitchPos setPos = new UIImageButtonSwitchPos((int)sp.Height.Pixels, "生成位置,右键传送", "Images/UI/SpawnPoint");
            setPos.OnSetPos = v =>
            {
                string ex = EditData.instance.SpawItemSetPos(data, v);
                if (ex != null) Main.NewText(ex);
            };
            setPos.OnRightClick += (e, s) =>
            {
                EditData.instance.Tp(data.pos);
            };
            sp.Append(setPos);

            UIImageButton addType = new UIImageButton(sp.Height.Pixels, "添加物品类型,右键删除", "Images/Item_27");
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
            if (ui_close == null) ui_close = Build1.FoldCloseUI(gss_name, () =>
            {
                EditData.instance.SpawItemDel(data);
                OnDataUpdate?.Invoke();
            }, SetItemTip);

            return ui_close;
        }

        public void SetItemTip()
        {
            List<string> ss = new List<string>();
            int index = -1;
            int oneLen = 10;

            for (int i = 0; i < data.types.Count; ++i)
            {
                if (i % oneLen == 0)
                {
                    ss.Add(string.Empty);
                    ++index;
                }
                else if (i + 1 >= oneLen * 2)
                {
                    ss[index] += "...";
                    break;
                }

                ss[index] += $"[i:{data.types[i]}]";
            }

            if (index < 0) return;
            tContentPatch.Content.DrawTip.SetDraw(ss.ToArray());
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            Common.GameAction.SpawItem.Remove(data);
        }
    }
}
