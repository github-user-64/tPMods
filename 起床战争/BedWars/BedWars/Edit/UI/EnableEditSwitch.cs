using BedWars.Common;
using Microsoft.Xna.Framework.Graphics;
using PlayerAccount.Common.FunctionCommand;
using ReLogic.Content;
using tContentPatch;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace BedWars.Edit.UI
{
    internal class EnableEditSwitch : Mod
    {
        public static bool Enable = false;

        public override void Load()
        {
            GameInterface.OnInitUI += () =>
            {
                Asset<Texture2D> texture1 = Main.Assets.Request<Texture2D>("Images/UI/DisplaySlots_4", AssetRequestMode.ImmediateLoad);
                Asset<Texture2D> texture2 = Main.Assets.Request<Texture2D>("Images/UI/DisplaySlots_5", AssetRequestMode.ImmediateLoad);

                UIImageButton btn = new UIImageButton(texture1);
                GameInterface.UI.Append(btn);

                bool drag = false;

                btn.OnUpdate += _ =>
                {
                    btn.SetImage(Enable ? texture2 : texture1);//一直设置应该也没啥消耗

                    a1(ref btn.Left.Pixels, btn.Width.Pixels, Main.screenWidth);
                    a1(ref btn.Top.Pixels, btn.Height.Pixels, Main.screenHeight);
                };
                btn.OnLeftClick += (e, s) => Enable = !Enable;

                //物品栏旁
                float v = 20f + (10 * 56) * 0.85f;
                btn.Left.Pixels = v;
                btn.Top.Pixels = 20;
            };
        }

        private static void a1(ref float v, float v2, float max)
        {
            if (v < 0) v = 0;
            else if (v + v2 > max) v = max - v2;
        }
    }
}
