using BedWars.Common.UI;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace BedWars.Edit.UI.Elements
{
    internal class UIImageButtonSwitchPos : UIImageButton, IEditControl
    {
        public Action<Point> OnSetPos = null;
        private SwitchPos sp = new SwitchPos();

        public UIImageButtonSwitchPos(int size, string text, string image) : base(size, text, image)
        {
            sp.OnSet += v => SetPos(v);
            sp.OnNoSet += () =>
            {
                CombatText.NewText(Main.LocalPlayer.getRect(), Color.Red, "取消选择", true, false);
            };

            Common.GameInterface.OnDraw.Add(_ =>
            {
                if (sp.Enable == false) return;

                DrawSwitchPos(sp.pos);
            });

            OnLeftClick += (e, s) => sp.Enable = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            sp.Update();
        }

        public virtual void SetPos(Point pos)
        {
            OnSetPos?.Invoke(pos);
        }

        public virtual void DrawSwitchPos(Point pos)
        {
            Common.DrawUtils.Draw_rectangle(pos, pos, Color.LawnGreen * 0.9f, Color.LawnGreen * 0.2f, 2);

            Utils.DrawBorderString(Main.spriteBatch, $"{pos.X},{pos.Y}", Main.MouseScreen + new Vector2(0, 22), Color.LawnGreen);
        }

        public void OnEditEnable() { }

        public void OnEditNoEnable()
        {
            sp.Enable = false;
        }
    }
}
