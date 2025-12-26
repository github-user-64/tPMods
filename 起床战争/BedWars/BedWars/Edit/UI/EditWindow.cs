using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using tContentPatch.Content.UI;
using Terraria;
using Terraria.UI;

namespace BedWars.Edit.UI
{
    internal class EditWindow : UIWindow
    {
        private UIElement ui_con = null;

        public EditWindow(string title, int width, int height) : base(title, width, height)
        {
            UIWrapPanel2 ui_wp = new UIWrapPanel2();
            ui_wp.Width.Precent = 1;
            ui_wp.ItemMargin = 2;
            ui_wp.Append(BuildItem(new EditItems0(), "Images/Item_1344", "设置"));
            ui_wp.Append(BuildItem(new EditItems1(), "Images/Inventory_Tick_On", "显示数据"));
            ui_wp.Append(BuildItem(new EditItems2(), "Images/Item_27", "生成物品"));

            ui_con = new UIElement();
            ui_con.Width.Precent = 1;
            ui_con.VAlign = 1;
            ui_con.OnUpdate += _ =>
            {
                ui_con.Height.Set(-ui_wp.Height.Pixels - 2, 1);
            };

            Child.Append(ui_wp);
            Child.Append(ui_con);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering) Main.LocalPlayer.mouseInterface = true;

            base.Update(gameTime);
        }

        public UIElement BuildItem<T>(T uie, string ico, string mouseText) where T : UIElement, IPanel
        {
            Asset<Texture2D> texture1 = Main.Assets.Request<Texture2D>(ico, AssetRequestMode.ImmediateLoad);
            UIRadioButton rb = new UIRadioButton(texture1.Value, 20, 20);
            rb.MouseHoveringText = mouseText;
            rb.OnChecked += () =>
            {
                ui_con.RemoveAllChildren();
                ui_con.Append(uie);
                uie.OnOpen();
            };

            return rb;
        }
    }
}
