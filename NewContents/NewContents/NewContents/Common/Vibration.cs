using Microsoft.Xna.Framework;
using tContentPatch;
using Terraria.Audio;
using Terraria.ID;

namespace NewContents.Common
{
    internal class Vibration : PatchMain
    {
        protected static int time = 0;

        public static void App(Vector2 pos)
        {
            SoundEngine.PlaySound(SoundID.InstantThunder, pos, pitchOffset: -0.5f);
            time = 10;
        }

        public override void UpdatePrefix(GameTime gameTime)
        {
            if (time > 0) --time;
        }

        public override Vector2 PlayerFocusedScreenPosition(Vector2 origin, Vector2 modifi)
        {
            if (time < 1) return modifi;

            modifi.X += Utils.getRand(-16, 16);
            modifi.Y += Utils.getRand(-16, 16);

            return modifi;
        }
    }
}
