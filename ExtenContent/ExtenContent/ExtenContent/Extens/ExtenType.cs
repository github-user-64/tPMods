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

        internal void SetAsset(IAssetRepository asset)
        {
            Asset = asset;
        }
    }
}
