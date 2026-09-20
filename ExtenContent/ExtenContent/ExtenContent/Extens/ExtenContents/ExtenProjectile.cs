using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展射弹
    /// </summary>
    public abstract class ExtenProjectile : ExtenType
    {
        /// <summary/>
        public Projectile Projectile { get; } = new Projectile();
        /// <summary>
        /// 对应的<see cref="Projectile.type"/>
        /// </summary>
        public int Type => Projectile.type;
        /// <summary>
        /// 图标位置
        /// </summary>
        public virtual string Texture { get; } = null;
        /// <summary/>
        public virtual LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", Name);

        /// <summary/>
        public virtual void SetDefault(Projectile proj) { }
        /// <summary/>
        public virtual void AI(Projectile proj) { }
        /// <summary/>
        public virtual void OnKill(Projectile proj) { }
        /// <summary/>
        public virtual bool? Colliding(Projectile proj, Rectangle myRect, Rectangle targetRect) => null;
        /// <summary/>
        public virtual void PostDraw(Projectile proj, Player player = null) { }
    }
}
