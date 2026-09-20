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

namespace ExtenContent.Utils
{
    internal class ExtenAssetRepository : IAssetRepository, IDisposable
    {
        public static IAssetRepository GetAssets(ModObject mo)
        {
            GameServiceContainer services = Main.instance.Services;

            //AssetReaderCollection assetReaderCollection = XnaExtensions.Get<AssetReaderCollection>(Main.instance.Services);
            AssetReaderCollection assetReaderCollection = new AssetReaderCollection();
            assetReaderCollection.RegisterReader(new XnbReader(Main.instance.Services), ".xnb");
            assetReaderCollection.RegisterReader(new PngReader(services.Get<IGraphicsDeviceService>().GraphicsDevice), ".png");

            AsyncAssetLoader asyncAssetLoader = new AsyncAssetLoader(assetReaderCollection, 20);
            //asyncAssetLoader.RequireTypeCreationOnTransfer(typeof(Texture2D));

            AssetRepository asset = new AssetRepository(new AssetLoader(assetReaderCollection), asyncAssetLoader);
            asset.SetSources(new IContentSource[]
            {
                new FileSystemContentSource(mo.modPath),
            });

            ExtenAssetRepository ear = new ExtenAssetRepository(asset);

            return ear;
        }

        protected AssetRepository asset = null;

        public int PendingAssets => asset.PendingAssets;
        public int TotalAssets => asset.TotalAssets;
        public int LoadedAssets => asset.LoadedAssets;

        public AssetValueUpdated AssetValueUpdatedHandler { get => asset.AssetValueUpdatedHandler; set => asset.AssetValueUpdatedHandler = value; }
        public FailedToLoadAssetCustomAction AssetLoadFailHandler { get => asset.AssetLoadFailHandler; set => asset.AssetLoadFailHandler = value; }
        public AssetWatcherValueUpdated AssetWatcherValueUpdatedHandler { get => asset.AssetWatcherValueUpdatedHandler; set => asset.AssetWatcherValueUpdatedHandler = value; }
        public AssetWatcherUpdateFailed AssetWatcherUpdateFailedHandler { get => asset.AssetWatcherUpdateFailedHandler; set => asset.AssetWatcherUpdateFailedHandler = value; }
        public ContentFileUpdated ContentFileUpdatedHandler { get => asset.ContentFileUpdatedHandler; set => asset.ContentFileUpdatedHandler = value; }

        public ExtenAssetRepository(AssetRepository asset)
        {
            this.asset = asset;
        }

        public void Dispose() => asset.Dispose();

        public void EnableAssetWatcher() => asset.EnableAssetWatcher();

        public Asset<T> Request<T>(string assetName, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) where T : class
        {
            string key = "Terraria/";

            if (assetName.Length < key.Length || assetName.Substring(0, key.Length) != key)
            {
                return asset.Request<T>(assetName, mode);
            }

            assetName = assetName.Substring(key.Length);

            return Main.Assets.Request<T>(assetName, mode);
        }

        public void SetSources(IEnumerable<IContentSource> sources, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) => asset.SetSources(sources, mode);

        public void TransferCompletedAssets() => asset.TransferCompletedAssets();
    }
}
