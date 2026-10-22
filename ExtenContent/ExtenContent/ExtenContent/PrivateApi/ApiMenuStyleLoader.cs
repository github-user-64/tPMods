using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiMenuStyleLoader
    {
        internal ApiMenuStyleLoader() { }

        /// <summary/>
        public bool DrawBGPrefix(SpriteBatchBeginner parentSpriteBatchBeginner)
        {
            return MenuStyleLoader.DrawBGPrefix(parentSpriteBatchBeginner);
        }

        /// <summary/>
        public void DrawMenuPostfix(GameTime gameTime)
        {
            MenuStyleLoader.DrawMenuPostfix();
        }
    }
}
