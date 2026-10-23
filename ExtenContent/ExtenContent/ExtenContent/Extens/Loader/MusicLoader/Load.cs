using System;
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
            if (Main.dedServ) return;

            LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
            if (legacyAudioSystem == null) return;

            //MP3AudioTrack, WAVAudioTrack这些的Stream关闭后会不可用
            foreach (ExtenMusic i in musics)
            {
                string a = "F:\\F_MY1\\File\\RJWJ\\C#\\Projects\\tPlainModLoader\\tPlainModLoader\\tPlainModLoader\\bin\\Debug\\Mods\\NewContents";
                return;
                //直接用new MP3AudioTrack(File.OpenStream)加载, 然后卸载时在这释放
                //不然用Asset.Request估计Stream不会自动关闭, 这样不好管理
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
