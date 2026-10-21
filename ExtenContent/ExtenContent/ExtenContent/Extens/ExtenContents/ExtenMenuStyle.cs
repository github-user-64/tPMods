namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展菜单样式
    /// </summary>
    public abstract class ExtenMenuStyle : ExtenType
    {
        /// <summary/>
        public int Type { get; internal set; }
        /// <summary>
        /// 背景, 为<see langword="null"/>则不变
        /// </summary>
        public virtual ExtenSurfaceBackground Back => null;
        /// <summary/>
        public virtual string DisplayName => null;
    }
}
