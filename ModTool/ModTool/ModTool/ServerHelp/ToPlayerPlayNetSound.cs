using Microsoft.Xna.Framework;
using System.Collections.Generic;
using tContentPatch;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 播放声音, 查id:
    /// <para/><a href="https://terraria.wiki.gg/zh/wiki/%E5%A3%B0%E9%9F%B3_ID"/>
    /// </summary>
    public static class ToPlayerPlayNetSound
    {
        private static Dictionary<LegacySoundStyle, ushort> IndexBySound = null;

        private class initSoundID : Mod
        {
            public override void Load()
            {
                if (Main.dedServ == false) return;

                if (SoundID.SoundByName == null || SoundID.IndexByName == null || SoundID.SoundByIndex == null)
                {
                    SoundID.FillAccessMap();
                }

                IndexBySound = new Dictionary<LegacySoundStyle, ushort>();

                foreach (KeyValuePair<ushort, LegacySoundStyle> i in SoundID.SoundByIndex)
                {
                    IndexBySound.Add(i.Value, i.Key);
                }
            }
        }

        /// <summary>
        /// 到玩家
        /// </summary>
        public static void ToPlay(int clientId, LegacySoundStyle sound, Vector2 pos)
        {
            if (Main.dedServ == false) return;

            if (IndexBySound?.TryGetValue(sound, out ushort soundIndex) != true) return;

            NetMessage.PlayNetSound(new NetMessage.NetSoundInfo(pos, soundIndex, sound.Style), clientId);
        }

        /// <summary>
        /// 到玩家, x,y 为-1时声音无位置
        /// </summary>
        public static void ToPlay(int clientId, LegacySoundStyle sound, int x = -1, int y = -1)
        {
            ToPlay(clientId, sound, new Vector2(x, y));
        }

        /// <summary>
        /// 到全部玩家
        /// </summary>
        public static void ToPlayAll(LegacySoundStyle sound, Vector2 pos, int ignoreClient = -1)
        {
            if (Main.dedServ == false) return;

            if (IndexBySound?.TryGetValue(sound, out ushort soundIndex) != true) return;

            NetMessage.PlayNetSound(new NetMessage.NetSoundInfo(pos, soundIndex, sound.Style), -1, ignoreClient);
        }

        /// <summary>
        /// 到全部玩家, x,y 为-1时声音无位置
        /// </summary>
        public static void ToPlayAll(LegacySoundStyle sound, int x = -1, int y = -1, int ignoreClient = -1)
        {
            ToPlayAll(sound, new Vector2(x, y), ignoreClient);
        }
    }
}
