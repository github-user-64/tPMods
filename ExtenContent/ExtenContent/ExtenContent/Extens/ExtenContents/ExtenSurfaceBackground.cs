using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展背景
    /// </summary>
    public abstract class ExtenSurfaceBackground : ExtenType
    {
        /// <summary/>
        public int Type { get; internal set; }
        /// <summary>
        /// 太阳, 为<see langword="null"/>则使用原版
        /// </summary>
        public virtual Asset<Texture2D> Sun => null;
        /// <summary>
        /// 月亮, 为<see langword="null"/>则使用原版
        /// </summary>
        public virtual Asset<Texture2D> Moon => null;

        /// <summary/>
        public virtual void DrawSurfaceBG(SpriteBatch spriteBatch) { }
        /// <summary>
        /// 绘制<see cref="Sun"/>前, 返回<see langword="false"/>则不绘制
        /// </summary>
        public virtual bool DrawSunPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color) => true;
        /// <summary/>
        public virtual void DrawSunPostfix(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale, Color color) { }
        /// <summary>
        /// 绘制<see cref="Moon"/>前, 返回<see langword="false"/>则不绘制
        /// </summary>
        public virtual bool DrawMoonPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color) => true;
        /// <summary/>
        public virtual void DrawMoonPostfix(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale, Color color) { }
    }
}
