using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem
{
    internal abstract class UISwitchPos : UIItemSwitch
    {
        private SwitchPos sp = new SwitchPos();
        private bool isDraw = false;

        public UISwitchPos(string text) : base(null, text)
        {
            SetVal(false);

            OnValUpdate += v => sp.Enable = v;

            sp.OnSet += v => SetPos(v);
            sp.OnNoSet += () =>
            {
                CombatText.NewText(Main.LocalPlayer.getRect(), Color.Red, "取消选择", true, false);
            };

            Common.GameInterface.OnDraw.Add(_ =>
            {
                if (sp.Enable == false) return;
                if (isDraw == false) return;
                isDraw = false;

                DrawSwitchPos(sp.pos);
            });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            sp.Update();
            SetVal(sp.Enable);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            isDraw = true;
        }

        public virtual void SetPos(Point pos)
        {

        }

        public virtual void DrawSwitchPos(Point pos)
        {
            Common.DrawUtils.Draw_rectangle(pos, pos, Color.LawnGreen * 0.9f, Color.LawnGreen * 0.2f, 2);

            Utils.DrawBorderString(Main.spriteBatch, $"{pos.X},{pos.Y}", Main.MouseScreen + new Vector2(0, 20), Color.LawnGreen);
        }
    }
}
