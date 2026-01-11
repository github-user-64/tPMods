using BedWars.Common.UI;
using BedWars.Edit.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.UI;

namespace BedWars.Edit.UI.EditWindow
{
    internal class EditWindow : UIWindowEditControl
    {
        private UIElement ui_con = null;

        public EditWindow(UIElement P, string title, int width, int height) : base(P, title, width, height)
        {
            UIRadioButton oneRb = null;

            UIWrapPanel2 ui_wp = new UIWrapPanel2();
            ui_wp.Width.Precent = 1;
            ui_wp.ItemMargin = 2;
            ui_wp.Append(oneRb = BuildPanel(new EditPanel0(), "Images/UI/Camera_6", "文件"));
            ui_wp.Append(BuildPanel(new EditPanel1(), "Images/Inventory_Tick_On", "显示数据"));
            ui_wp.Append(BuildPanel(new EditPanel2(), "Images/Item_1344", "设置"));
            ui_wp.Append(BuildPanel(new EditPanel3(), "Images/Item_2", "图格数据"));
            ui_wp.Append(BuildPanel(new EditPanel4(), "Images/Item_27", "编辑生成物品"));
            ui_wp.Append(BuildPanel(new EditPanel5(), "Images/House_Banner_1", "编辑队伍(必须至少1个队伍)"));
            ui_wp.Append(BuildPanel(new EditPanel6(), "Images/UI/Cursor_7", "编辑背包"));
            ui_wp.Append(BuildPanel(new EditPanel7(), "Images/UI/Cursor_10", "编辑商店"));

            ui_con = new UIElement();
            ui_con.Width.Precent = 1;
            ui_con.VAlign = 1;
            ui_con.OnUpdate += _ =>
            {
                ui_con.Height.Set(-ui_wp.Height.Pixels - 2, 1);
            };

            Child.Append(ui_wp);
            Child.Append(ui_con);

            oneRb.IsChecked = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering) Main.LocalPlayer.mouseInterface = true;

            base.Update(gameTime);
        }

        public UIRadioButton BuildPanel(UIElement ui, string ico, string mouseText)
        {
            Asset<Texture2D> texture1 = Main.Assets.Request<Texture2D>(ico, AssetRequestMode.ImmediateLoad);
            UIRadioButton rb = new UIRadioButton(texture1.Value, 20, 20);
            rb.MouseHoveringText = mouseText;
            rb.OnChecked += () =>
            {
                foreach (UIElement i in ui_con.Children) i.Deactivate();
                ui_con.RemoveAllChildren();

                ui_con.Append(ui);
                ui.Activate();
            };

            return rb;
        }

        public override void Open(UIElement windowParent)
        {
            base.Open(windowParent);
            if (Init.Enable == false) Init.Enable = true;
        }

        public override void Close()
        {
            base.Close();
            if (Init.Enable == true) Init.Enable = false;
        }
    }
}
