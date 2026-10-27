using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static class PatchMainLoader
    {
        private static readonly List<EPatchMain> patchs = new List<EPatchMain>();

        internal static void Load()
        {
            patchs.ForEach(i => i.Load());
        }

        internal static void Unload()
        {
            patchs.ForEach(i => i.Unload());
            patchs.Clear();
        }

        internal static void Register(EPatchMain patch)
        {
            patchs.Add(patch);
        }

        internal static void UpdatePrefix(GameTime gameTime)
        {
            foreach (EPatchMain i in patchs) i.UpdatePrefix(gameTime);
        }

        internal static void UpdatePostfix(GameTime gameTime)
        {
            foreach (EPatchMain i in patchs) i.UpdatePostfix(gameTime);
        }

        internal static void DoUpdateInWorldPrefix()
        {
            foreach (EPatchMain i in patchs) i.DoUpdateInWorldPrefix();
        }

        internal static void DoUpdateInWorldPostfix()
        {
            foreach (EPatchMain i in patchs) i.DoUpdateInWorldPostfix();
        }

        internal static void GetPreferredBGStyleForPlayer(ref int bgStyle)
        {
            if (ExtenManag.IsLoaded != true) return;

            foreach (EPatchMain i in patchs) i.GetPreferredBGStyleForPlayer(ref bgStyle);
        }

        internal static bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            if (ExtenManag.IsLoaded != true) return true;

            foreach (EPatchMain i in patchs)
            {
                if (i.UpdateAudio_DecideOnNewMusicPrefix() == false) return false;
            }

            return true;
        }
    }
}
