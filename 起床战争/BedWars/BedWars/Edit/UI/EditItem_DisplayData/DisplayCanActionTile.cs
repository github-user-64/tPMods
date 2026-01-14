using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tContentPatch.Content.UI.ModSet;
using Terraria;

namespace BedWars.Edit.UI.EditItem_DisplayData
{
    /// <summary>
    /// 绘制可交互图格
    /// </summary>
    internal class DisplayCanActionTile : UIItemSwitch
    {
        private static readonly Color drawc = Color.DarkBlue * 0.5f;

        public DisplayCanActionTile(string text) : base(null, text)
        {
            Common.GameInterface.OnDraw.Add(DrawTile);
        }

        private void DrawTile(SpriteBatch spriteBatch)
        {
            if (GetVal() == false) return;
            if (Init.Enable == false) return;
            MapData data = EditData.instance.Data;
            if (data == null) return;
            Rectangle rect = data.Info.rect;

            for (int y = 0; y < rect.Height; ++y)
            {
                for (int x = 0; x < rect.Width; ++x)
                {
                    if (data.Tile[y][x].canAction == false) continue;

                    Point pos = new Point(rect.X, rect.Y);
                    pos.X += x;
                    pos.Y += y;

                    Common.DrawUtils.Draw_rectangle(pos, pos, drawc, drawc);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering) Main.instance.MouseText("意味着这里的图格玩家可以放置破坏交互");
        }
    }
}
