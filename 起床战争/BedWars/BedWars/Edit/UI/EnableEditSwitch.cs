using BedWars.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using tContentPatch;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace BedWars.Edit.UI
{
    internal class EnableEditSwitch : Mod
    {
        public static UIElement Build()
        {
            Asset<Texture2D> texture1 = Main.Assets.Request<Texture2D>("Images/UI/DisplaySlots_4", AssetRequestMode.ImmediateLoad);
            Asset<Texture2D> texture2 = Main.Assets.Request<Texture2D>("Images/UI/DisplaySlots_5", AssetRequestMode.ImmediateLoad);

            UIImageButton btn = new UIImageButton(texture1);
            UIState uistate = new UIState();
            uistate.OnUpdate += _ =>
            {
                if (Main.netMode == 0 || Main.netMode == 1)
                {
                    if (uistate.Children.Count() < 1) uistate.Append(btn);
                    return;
                }
                uistate.RemoveAllChildren();
            };
            GameInterface.UI.Append(uistate);

            bool drag = false;
            Vector2 dragOff = Vector2.Zero;

            btn.OnUpdate += _ =>
            {
                btn.SetImage(Init.Enable ? texture2 : texture1);//一直设置应该也没啥消耗

                if (drag)
                {
                    Vector2 dp = Main.MouseScreen + dragOff;
                    btn.Left.Pixels = dp.X;
                    btn.Top.Pixels = dp.Y;

                    if (Main.mouseRight == false) drag = false;
                }

                a1(ref btn.Left.Pixels, btn.Width.Pixels, Main.screenWidth);
                a1(ref btn.Top.Pixels, btn.Height.Pixels, Main.screenHeight);

                if (btn.IsMouseHovering)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.instance.MouseText($"{(Init.Enable ? "禁用" : "启用")}地图编辑");
                }
            };
            btn.OnLeftClick += (e, s) =>
            {
                Init.Enable = !Init.Enable;
                SoundEngine.PlaySound(SoundID.MenuTick);
            };
            btn.OnRightMouseDown += (e, s) =>
            {
                dragOff = new Vector2(btn.Left.Pixels, btn.Top.Pixels) - Main.MouseScreen;
                drag = true;
            };

            //物品栏旁
            float v = 20f + (10 * 56) * 0.85f;
            btn.Left.Pixels = v;
            btn.Top.Pixels = 20;

            return uistate;
        }

        private static void a1(ref float v, float v2, float max)
        {
            if (v < 0) v = 0;
            else if (v + v2 > max) v = max - v2;
        }
    }
}
