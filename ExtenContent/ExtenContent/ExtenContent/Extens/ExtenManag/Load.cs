using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Readers;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using tContentPatch.ModLoad;
using Terraria;

namespace ExtenContent.Extens
{
    public static partial class ExtenManag
    {
        private static class RegistFoo<T> where T : IExtenType
        {
            public static Action<T> Foo;
        }

        private static readonly List<IAssetRepository> Assets = new List<IAssetRepository>();
        private static bool IsLoad = false;

        static ExtenManag()
        {
            RegistFoo<ExtenItem>.Foo = ItemLoad.Register;

            RegistMod(ThisMod.mo);
        }

        internal static void Load()
        {
            if (IsLoad) return;
            IsLoad = true;

            ItemLoad.Load();
            ExtenItemUnload.Load();
        }

        internal static void Unload()
        {
            ItemLoad.Unload();
            ExtenItemUnload.Unload();

            foreach (IAssetRepository asset in Assets) asset.Dispose();
            Assets.Clear();
        }

        /// <summary>
        /// 注册模组中所有的扩展内容<para/>
        /// 应在<see cref="tContentPatch.Mod.Loaded"/>前调用
        /// </summary>
        public static void RegistMod(ModObject mo)
        {
            if (IsLoad) throw new Exception("不可在加载后注册");

            IAssetRepository asset = GetAssets(mo);
            Assets.Add(asset);

            RegistExten<ExtenItem>(mo, asset);
        }

        private static void RegistExten<T>(ModObject mo, IAssetRepository asset) where T : ExtenType
        {
            List<T> extens = CreateInstance<T>(mo.assembly);
            if (extens == null) return;

            foreach (T exten in extens)
            {
                exten.Mod = mo;
                exten.SetAsset(asset);

                ExtenInstance.Register(exten);

                RegistFoo<T>.Foo(exten);
            }
        }

        private static List<targetType> CreateInstance<targetType>(Assembly assembly)
        {
            Type[] type = assembly.GetTypes();

            List<Type> types = type.ToList().FindAll(
                t => t.IsClass && t.IsAbstract == false &&
                typeof(targetType).IsAssignableFrom(t));

            if (types.Count < 1) return null;

            List<targetType> instance = new List<targetType>();

            foreach (Type t in types)
            {
                targetType obj = (targetType)Activator.CreateInstance(t);
                instance.Add(obj);
            }

            return instance;
        }

        private static IAssetRepository GetAssets(ModObject mo)
        {
            GameServiceContainer services = Main.instance.Services;

            //AssetReaderCollection assetReaderCollection = XnaExtensions.Get<AssetReaderCollection>(Main.instance.Services);
            AssetReaderCollection assetReaderCollection = new AssetReaderCollection();
            assetReaderCollection.RegisterReader(new XnbReader(Main.instance.Services), ".xnb");
            assetReaderCollection.RegisterReader(new PngReader(services.Get<IGraphicsDeviceService>().GraphicsDevice), ".png");

            AsyncAssetLoader asyncAssetLoader = new AsyncAssetLoader(assetReaderCollection, 20);
            //asyncAssetLoader.RequireTypeCreationOnTransfer(typeof(Texture2D));

            AssetRepository Asset = new AssetRepository(new AssetLoader(assetReaderCollection), asyncAssetLoader);
            Asset.SetSources(new IContentSource[]
            {
                new FileSystemContentSource(mo.modPath),
            });

            return Asset;
        }
    }
}
