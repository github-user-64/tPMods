using Microsoft.Xna.Framework.Graphics;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展背景
    /// </summary>
    public abstract class ExtenSurfaceBackground : ExtenType
    {
        /// <summary/>
        public int Type { get; internal set; }

        /// <summary/>
        public virtual void DrawSurfaceBG(SpriteBatch spriteBatch) { }
    }
}
