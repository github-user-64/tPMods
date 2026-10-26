using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Terraria;
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

        /// <summary/>
        public bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            return MenuStyleLoader.UpdateAudio_DecideOnNewMusicPrefix();
        }

        /// <summary/>
        public void DrawLogo(bool playOldTile, bool boulderLogo, float logoRotation, float logoScale)
        {
            MenuStyleLoader.DrawLogo(playOldTile, boulderLogo, logoRotation, logoScale);
        }

        /// <summary/>
        public bool DrawSunAndMoon(Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
        {
            return MenuStyleLoader.DrawSunAndMoon(sceneArea, moonColor, sunColor, tempMushroomInfluence);
        }
    }
}
