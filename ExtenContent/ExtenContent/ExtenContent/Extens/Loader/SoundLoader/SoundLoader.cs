using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;

namespace ExtenContent.Extens
{
    /// <summary/>
    public static partial class SoundLoader
    {
        private static readonly List<ExtenSound> sounds = new List<ExtenSound>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentLoad();

            sounds.ForEach(i => i.Load());

            ThisMod.Log($"{typeof(SoundLoader)}:加载完成:新声音:{sounds.Count}个", time);
        }

        internal static void UnloadPrefix() { }
        internal static void Unload()
        {
            sounds.ForEach(i =>
            {
                i.Unload();
                i.Dispose();
            });

            sounds.Clear();
        }

        internal static void Register(ExtenSound back)
        {
            sounds.Add(back);
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        public static void Play<T>(Vector2 pos) where T : ExtenSound
        {
            SoundStyle sound = ExtenInstance<T>.Instance?.Sound;
            if (sound == null) return;

            SoundEngine.PlayTrackedSound(sound, pos);
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        public static void Play<T>() where T : ExtenSound
        {
            SoundStyle sound = ExtenInstance<T>.Instance?.Sound;
            if (sound == null) return;

            SoundEngine.PlayTrackedSound(sound);
        }
    }
}
