using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class MusicLoader
    {
        /// <summary>
        /// 音乐数量
        /// </summary>
        public static int MusicCount { get; private set; } = MusicID.Count;
        private static readonly List<ExtenMusic> musics = new List<ExtenMusic>();
        private static readonly List<IAudioTrack> audios = new List<IAudioTrack>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();
            //服务端没有MP3AudioTrack, OGGAudioTrack
            if (Main.dedServ != true) ContentLoad();

            musics.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(MusicLoader)}:加载完成:新音乐:{MusicCount - MusicID.Count}个", time);
        }

        internal static void UnloadPrefix()
        {
            MusicCount = MusicID.Count;
        }

        internal static void Unload()
        {
            musics.ForEach(i => i.Unload());
            musics.Clear();

            ContentInit();

            foreach (IAudioTrack i in audios) i.Dispose();
            audios.Clear();
        }

        internal static void Register(ExtenMusic music)
        {
            musics.Add(music);
            music.Type = MusicCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenMusic"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenMusic GetExtenMusic(int type)
        {
            if (IsExten(type) != true) return null;

            return musics[type - MusicID.Count];
        }

        /// <summary>
        /// 是否是<see cref="ExtenMusic"/>
        /// </summary>
        public static bool IsExten(int type)
        {
            return MusicID.Count <= type && type < MusicCount;
        }
    }
}
