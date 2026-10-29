using ExtenContent.Exten;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace NewContents.MenuStyle
{
    internal class Style1 : ExtenMenuStyle
    {
        public override string DisplayName => "欸嘿";
        public override int Back => ExtenManag.BackType<Back.Back1>();
        public override int Music => MusicID.OtherworldSnow;

        public override bool DrawLogoPrefix(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoRotation *= 500;

            return true;
        }
    }
}
