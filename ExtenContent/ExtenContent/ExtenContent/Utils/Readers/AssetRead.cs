using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Readers;
using System;
using System.IO;
using Terraria.Audio;

namespace ExtenContent.Utils.Readers
{
    internal sealed class AssetRead<T> : AssetRead
    {
        private AssetRead() { }
        public static Func<Stream, T> Read = null;
    }

    internal class AssetRead : IAssetReader
    {
        static AssetRead()
        {
            AssetRead<MP3AudioTrack>.Read = s => ReadNoClose(s, ss => new MP3AudioTrack(ss));//mp3
            AssetRead<WAVAudioTrack>.Read = s => new WAVAudioTrack(s);//wav
            AssetRead<SoundEffect>.Read = SoundEffect.FromStream;//wav
        }

        public T FromStream<T>(Stream stream) where T : class
        {
            if (AssetRead<T>.Read == null) throw new Exception($"没有读取方式:{typeof(T)}");

            return AssetRead<T>.Read(stream);
        }

        private static T ReadNoClose<T>(Stream stream, Func<Stream, T> fun)
        {
            MemoryStream copy = new MemoryStream();
            stream.CopyTo(copy);
            copy.Seek(0, SeekOrigin.Begin);

            return fun(copy);
        }
    }

    internal class MP3Reader : AssetRead { }
    internal class WAVReader : AssetRead { }
}
