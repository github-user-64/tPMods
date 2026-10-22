using System;
using System.Collections.Generic;
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

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();
            ContentLoad();

            musics.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(MusicLoader)}:加载完成:新音乐:{MusicCount - MusicID.Count}个", time);
        }

        internal static void Unload()
        {
            musics.ForEach(i => i.Unload());

            MusicCount = MusicID.Count;
            musics.Clear();
        }

        internal static void Register(ExtenMusic music)
        {
            musics.Add(music);
            music.Type = MusicCount++;
        }

        /// <summary>
        /// 获取对应的<see cref="ExtenMusic"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenMusic GetStyle(int type)
        {
            if (TypeInRange(type) != true) return null;

            return musics[type - MusicID.Count];
        }

        /// <summary>
        /// 是否是<see cref="ExtenMusic"/>
        /// </summary>
        public static bool TypeInRange(int type)
        {
            return MusicID.Count <= type && type < MusicCount;
        }
    }
}
