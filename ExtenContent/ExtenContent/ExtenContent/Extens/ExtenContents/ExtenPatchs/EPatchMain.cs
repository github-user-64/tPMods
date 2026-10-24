using Microsoft.Xna.Framework;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary/>
    public abstract class EPatchMain : ExtenType
    {
        /// <summary/>
        public virtual void UpdatePrefix(GameTime gameTime) { }
        /// <summary/>
        public virtual void UpdatePostfix(GameTime gameTime) { }
        /// <summary/>
        public virtual void DoUpdateInWorldPrefix() { }
        /// <summary/>
        public virtual void DoUpdateInWorldPostfix() { }

        /// <summary>
        /// 修改背景, 游戏内有效
        /// </summary>
        public virtual void GetPreferredBGStyleForPlayer(ref int bgStyle) { }

        /// <summary>
        /// 修改声音, 游戏内有效, <see cref="Main.newMusic"/><br/>
        /// 如果其它<see cref="UpdateAudio_DecideOnNewMusicPrefix"/>先返回<see langword="false"/>剩下的则不被调用
        /// </summary>
        public virtual bool UpdateAudio_DecideOnNewMusicPrefix() => true;
    }
}
