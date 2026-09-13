using ReLogic.Content;
using tContentPatch.ModLoad;

namespace ExtenContent.Extens
{
    public abstract class ExtenType : IExtenType
    {
        public ModObject Mod { get; internal set; } = null;
        public string Name => GetType().Name;
        public string FullName => $"{GetType().Namespace}.{Name}";
        public IAssetRepository Asset { get; protected set; } = null;

        internal void SetAsset(IAssetRepository asset)
        {
            Asset = asset;
        }
    }
}
