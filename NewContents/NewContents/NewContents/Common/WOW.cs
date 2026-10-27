using ExtenContent.Extens;
using Terraria;
using Terraria.ID;

namespace NewContents.Common
{
    internal class WOW : EPatchMain
    {
        public bool ok = false;

        public override void DoUpdateInWorldPrefix()
        {
            ok = false;
        }

        public override void GetPreferredBGStyleForPlayer(ref int bgStyle)
        {
            if (ok) bgStyle = ExtenManag.BackType<Back.Back1>();
        }

        public override bool UpdateAudio_DecideOnNewMusicPrefix()
        {
            if (ok) Main.newMusic = MusicID.OtherworldSnow;
            return !ok;
        }
    }
}
