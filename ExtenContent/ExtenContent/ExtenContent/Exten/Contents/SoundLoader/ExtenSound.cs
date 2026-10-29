using ExtenContent.Utils;
using System;
using Terraria.Audio;

namespace ExtenContent.Exten
{
    /// <summary>
    /// 扩展声音
    /// </summary>
    public abstract class ExtenSound : ExtenType, ClassPathUtils.IClassPathUtils, IDisposable
    {
        /// <summary>
        /// 声音
        /// </summary>
        public SoundStyle Sound { get; internal set; } = null;

        /// <summary>
        /// 文件位置
        /// </summary>
        public virtual string Path => this.GetPath();
        /// <summary/>
        public virtual int MaxTrackedInstances => 1;

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            (Sound as IDisposable)?.Dispose();
            Sound = null;
        }
    }
}
