using ExtenContent.Extens;
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
    }
}
