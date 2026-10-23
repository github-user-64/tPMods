using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class MusicLoader
    {
        private static void ContentInit()
        {
            LoaderUtils.ResetStaticMembers(typeof(MusicID.Sets));

            LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
            if (legacyAudioSystem == null) return;

            Array.Resize(ref legacyAudioSystem.AudioTracks, MusicCount);
            Array.Resize(ref legacyAudioSystem.TrackLoopCounts, MusicCount);
            Array.Resize(ref legacyAudioSystem.PlayCallbacks, MusicCount);
            Array.Resize(ref Main.musicFade, MusicCount);
            Array.Resize(ref Main.musicNoCrossFade, MusicCount);

            Main.audioSystem = legacyAudioSystem;
        }

        private static void ContentLoad()
        {
            LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
            if (legacyAudioSystem == null) return;

            foreach (ExtenMusic i in musics)
            {
                string a = "F:\\F_MY1\\File\\RJWJ\\C#\\Projects\\tPlainModLoader\\tPlainModLoader\\tPlainModLoader\\bin\\Debug\\Mods\\NewContents";

                switch (i.FileType)
                {
                    case FileType.MP3: legacyAudioSystem.AudioTracks[i.Type] = i.Asset.Request<MP3AudioTrack>(i.Path).Value; break;
                    case FileType.WAV: legacyAudioSystem.AudioTracks[i.Type] = i.Asset.Request<WAVAudioTrack>(i.Path).Value; break;
                    //case FileType.WAV:
                    //    legacyAudioSystem.AudioTracks[i.Type] = new WAVAudioTrack(File.OpenRead(Path.Combine(a, i.Path) + ".wav"));
                    //    break;
                    default: throw new Exception($"没有该类型的加载方法:{i.FileType}");
                }
            }

            Main.audioSystem = legacyAudioSystem;
        }
    }
}
