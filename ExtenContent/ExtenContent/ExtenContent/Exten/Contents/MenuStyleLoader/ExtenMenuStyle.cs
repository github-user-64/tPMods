using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;

namespace ExtenContent.Exten
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
        /// <summary>
        /// Logo, 为<see langword="null"/>则使用原版
        /// </summary>
        public virtual Asset<Texture2D> Logo => null;

        /// <summary/>
        public virtual void OnSelected() { }
        /// <summary/>
        public virtual void OnDeselected() { }
        /// <summary/>
        public virtual void Update() { }
        /// <summary>
        /// 绘制<see cref="Logo"/>前, 返回<see langword="false"/>则不绘制
        /// </summary>
        public virtual bool DrawLogoPrefix(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor) => true;
        /// <summary/>
        public virtual void DrawLogoPostfix(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor) { }
    }
}
