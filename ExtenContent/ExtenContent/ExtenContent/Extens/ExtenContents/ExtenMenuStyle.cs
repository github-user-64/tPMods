using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展菜单样式
    /// </summary>
    public abstract class ExtenMenuStyle : ExtenType
    {
        /// <summary/>
        public int Type { get; internal set; }
        /// <summary/>
        public virtual string DisplayName => null;
        /// <summary>
        /// 背景, 为<see langword="-1"/>则不变
        /// </summary>
        public virtual int Back => SurfaceBackgroundID.Forest1 - 1;
        /// <summary>
        /// 音乐, 为<see cref="MusicID.None"/>则不变
        /// </summary>
        public virtual int Music => MusicID.None;

        /// <summary/>
        public virtual void OnSelected() { }
        /// <summary/>
        public virtual void OnDeselected() { }
        /// <summary/>
        public virtual void Update() { }
    }
}
