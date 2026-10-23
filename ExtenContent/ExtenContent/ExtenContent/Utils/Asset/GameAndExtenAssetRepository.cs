using ExtenContent.Utils.AssetUtils.Readers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Readers;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using tContentPatch.ModLoad;
using Terraria;

namespace ExtenContent.Utils.AssetUtils
{
    /// <summary>
    /// 从游戏或扩展加载资源
    /// </summary>
    public class GameAndExtenAssetRepository : IAssetRepository, IDisposable
    {
        /// <summary>
        /// 获取从游戏或模组加载资源的<see cref="IAssetRepository"/>
        /// </summary>
        /// <param name="mo">从<see cref="ModObject.modPath"/>下加载资源</param>
        public static IAssetRepository GetAssets(ModObject mo)
        {
            GameServiceContainer services = Main.instance.Services;

            //AssetReaderCollection assetReaderCollection = XnaExtensions.Get<AssetReaderCollection>(Main.instance.Services);
            AssetReaderCollection assetReaderCollection = new AssetReaderCollection();
            assetReaderCollection.RegisterReader(new XnbReader(Main.instance.Services), ".xnb");
            assetReaderCollection.RegisterReader(new PngReader(services.Get<IGraphicsDeviceService>().GraphicsDevice), ".png");
            assetReaderCollection.RegisterReader(new WAVReader(), ".wav");

            AsyncAssetLoader asyncAssetLoader = new AsyncAssetLoader(assetReaderCollection, 20);
            //asyncAssetLoader.RequireTypeCreationOnTransfer(typeof(Texture2D));

            AssetRepository asset = new AssetRepository(new AssetLoader(assetReaderCollection), asyncAssetLoader);
            asset.SetSources(new IContentSource[]
            {
                new FileSystemContentSource(mo.modPath),
            });

            GameAndExtenAssetRepository ear = new GameAndExtenAssetRepository(asset);

            return ear;
        }

        /// <summary>
        /// 扩展的<see cref="AssetRepository"/>
        /// </summary>
        protected AssetRepository Asset = null;

        /// <inheritdoc/>
        public int PendingAssets => Asset.PendingAssets;
        /// <inheritdoc/>
        public int TotalAssets => Asset.TotalAssets;
        /// <inheritdoc/>
        public int LoadedAssets => Asset.LoadedAssets;

        /// <inheritdoc/>
        public AssetValueUpdated AssetValueUpdatedHandler { get => Asset.AssetValueUpdatedHandler; set => Asset.AssetValueUpdatedHandler = value; }
        /// <inheritdoc/>
        public FailedToLoadAssetCustomAction AssetLoadFailHandler { get => Asset.AssetLoadFailHandler; set => Asset.AssetLoadFailHandler = value; }
        /// <inheritdoc/>
        public AssetWatcherValueUpdated AssetWatcherValueUpdatedHandler { get => Asset.AssetWatcherValueUpdatedHandler; set => Asset.AssetWatcherValueUpdatedHandler = value; }
        /// <inheritdoc/>
        public AssetWatcherUpdateFailed AssetWatcherUpdateFailedHandler { get => Asset.AssetWatcherUpdateFailedHandler; set => Asset.AssetWatcherUpdateFailedHandler = value; }
        /// <inheritdoc/>
        public ContentFileUpdated ContentFileUpdatedHandler { get => Asset.ContentFileUpdatedHandler; set => Asset.ContentFileUpdatedHandler = value; }


        /// <param name="exten">扩展的<see cref="AssetRepository"/></param>
        public GameAndExtenAssetRepository(AssetRepository exten)
        {
            Asset = exten;
        }


        /// <summary>
        /// 从<see cref="Asset"/>加载<br/>
        /// 如果<paramref name="assetName"/>开头是<see langword="Terraria/"/>, 则从<see cref="Main.Assets"/>加载<br/>
        /// </summary>
        public Asset<T> Request<T>(string assetName, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) where T : class
        {
            string key = "Terraria/";

            if (assetName.Length < key.Length || assetName.Substring(0, key.Length) != key)
            {
                return Asset.Request<T>(assetName, mode);
            }

            assetName = assetName.Substring(key.Length);

            return Main.Assets.Request<T>(assetName, mode);
        }

        /// <inheritdoc/>
        public void Dispose() => Asset.Dispose();

        /// <inheritdoc/>
        public void EnableAssetWatcher() => Asset.EnableAssetWatcher();

        /// <inheritdoc/>
        public void SetSources(IEnumerable<IContentSource> sources, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) => Asset.SetSources(sources, mode);

        /// <inheritdoc/>
        public void TransferCompletedAssets() => Asset.TransferCompletedAssets();
    }
}
