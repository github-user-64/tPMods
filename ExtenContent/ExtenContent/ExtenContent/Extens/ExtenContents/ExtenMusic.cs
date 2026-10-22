using ExtenContent.Utils;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展音乐
    /// </summary>
    public abstract class ExtenMusic : ExtenType, ClassPathUtils.IClassPathUtils
    {
        /// <summary/>
        public int Type { get; internal set; }
        /// <summary>
        /// 文件位置
        /// </summary>
        public virtual string Path => this.GetPath();
        /// <summary/>
        public virtual FileType FileType => FileType.MP3;
    }
}
