using ReLogic.Content;
using tContentPatch.ModLoad;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展类型
    /// </summary>
    public abstract class ExtenType : IExtenType
    {
        /// <inheritdoc/>
        public ModObject Mod { get; internal set; } = null;
        /// <inheritdoc/>
        public string Name => GetType().Name;
        /// <inheritdoc/>
        public string FullName => $"{GetType().Namespace}.{Name}";
        /// <summary>
        /// 用于请求资源, 请求目录默认为<see cref="ModObject.modPath"/>
        /// </summary>
        public IAssetRepository Asset { get; protected set; } = null;
        /// <summary>
        /// 加载时
        /// </summary>
        public virtual void Load() { }
        /// <summary>
        /// 卸载时
        /// </summary>
        public virtual void Unload() { }
        /// <summary>
        /// 在所有东西加载完成后调用
        /// </summary>
        public virtual void SetStaticDefaults() { }

        internal void SetAsset(IAssetRepository asset)
        {
            Asset = asset;
        }
    }
}
