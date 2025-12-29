using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModTool.Common.UI;
using ReLogic.Content;
using Terraria;
using static PlayerAccount.Common.FunctionCommand.accAction;

namespace BedWars.Edit.UI.EditItem_EditData
{
    internal class TpMapPos : UIItemTextButton
    {
        private static Asset<Texture2D> ico1 = Main.Assets.Request<Texture2D>("Images/UI/SpawnPoint", AssetRequestMode.ImmediateLoad);

        public TpMapPos(string btnText, string text = null) : base(btnText, ico1.Value, text)
        {
            OnClick += () => Tp();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering == false) return;
            if (EditData.instance.DataInfo is MapInfoData info == false) return;

            Vector2 pos = info.pos.ToWorldCoordinates();
            pos += info.size.ToWorldCoordinates() / 2;

            Main.instance.MouseText($"{pos.X}, {pos.Y}");
        }

        private void Tp()
        {
            if (EditData.instance.DataInfo is MapInfoData info == false)
            {
                Main.NewText("数据为null");
                return;
            }

            Point point = info.pos;
            Vector2 pos = point.ToWorldCoordinates();
            pos += info.size.ToWorldCoordinates() / 2;

            if (WorldGen.InWorld(point.X, point.Y) == false)
            {
                Main.NewText($"超出世界:{pos.X},{pos.Y}");
                return;
            }

            Main.LocalPlayer.Center = pos;
        }
    }
}
