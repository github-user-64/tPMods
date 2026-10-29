using ExtenContent.Exten;
using Microsoft.Xna.Framework;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiPatchMainLoader
    {
        internal ApiPatchMainLoader() { }

        /// <summary/>
        public void UpdatePrefix(GameTime gameTime)
        {
            PatchMainLoader.UpdatePrefix(gameTime);
        }

        /// <summary/>
        public void UpdatePostfix(GameTime gameTime)
        {
            PatchMainLoader.UpdatePostfix(gameTime);
        }

        /// <summary/>
        public void DoUpdateInWorldPrefix()
        {
            PatchMainLoader.DoUpdateInWorldPrefix();
        }

        /// <summary/>
        public void DoUpdateInWorldPostfix()
        {
            PatchMainLoader.DoUpdateInWorldPostfix();
        }

        /// <summary/>
        public void GetPreferredBGStyleForPlayerPostfix(ref int bgStyle)
        {
            PatchMainLoader.GetPreferredBGStyleForPlayer(ref bgStyle);
        }

        /// <summary/>
        public bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            return PatchMainLoader.UpdateAudio_DecideOnNewMusicPrefix();
        }
    }
}
