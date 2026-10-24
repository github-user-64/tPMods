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

            //MP3AudioTrack, WAVAudioTrack这些的Stream关闭后会不可用
            foreach (ExtenMusic i in musics)
            {
                string path = Path.Combine(i.Mod.modPath, i.Path);
                Stream stream = null;
                IAudioTrack audio = null;

                try
                {
                    switch (i.FileType)
                    {
                        case FileType.MP3: audio = new MP3AudioTrack(stream = File.OpenRead($"{Path.Combine(i.Mod.modPath, i.Path)}.mp3")); break;
                        case FileType.WAV: audio = new WAVAudioTrack(stream = File.OpenRead($"{Path.Combine(i.Mod.modPath, i.Path)}.wav")); break;
                        case FileType.OGG: audio = new OGGAudioTrack(stream = File.OpenRead($"{Path.Combine(i.Mod.modPath, i.Path)}.ogg")); break;
                        default: throw new Exception($"没有该类型的加载方法:{i.FileType}");
                    }
                }
                catch
                {
                    stream?.Close();
                    stream?.Dispose();
                    throw;
                }

                audios.Add(audio);

                legacyAudioSystem.AudioTracks[i.Type] = audio;
            }

            Main.audioSystem = legacyAudioSystem;
        }
    }
}
