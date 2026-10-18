using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using tContentPatch.ModLoad;
using Terraria;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展管理
    /// </summary>
    public static partial class ExtenManag
    {
        private static class RegistFoo<T> where T : IExtenType
        {
            public static Action<T> Foo;
        }
        private readonly static List<Action> Loads = new List<Action>();
        private readonly static List<Action> Unloads = new List<Action>();

        private static readonly List<ExtenType> Extens = new List<ExtenType>();
        private static readonly List<IAssetRepository> Assets = new List<IAssetRepository>();

        private static bool IsLoad = false;
        private static bool IsInited = false;
        private static readonly object _lock = new object();

        static ExtenManag()
        {
            RegistLoader<ExtenItem>(ItemLoader.Register, ItemLoader.Load, ItemLoader.Unload);
            RegistLoader<ExtenEquip>(EquipLoader.Register, EquipLoader.Load, EquipLoader.Unload);
            RegistLoader<ExtenProjectile>(ProjectileLoader.Register, ProjectileLoader.Load, ProjectileLoader.Unload);
            RegistLoader<ExtenBuff>(BuffLoader.Register, BuffLoader.Load, BuffLoader.Unload);
            RegistLoader<EPatchPlayer>(PatchPlayerLoader.Register, PatchPlayerLoader.Load, PatchPlayerLoader.Unload);
            RegistLoader<EPatchRecipe>(PatchRecipeLoader.Register, PatchRecipeLoader.Load, PatchRecipeLoader.Unload);
            RegistLoader<EPatchArmorSetBonus>(ArmorSetBonusesLoader.Register, ArmorSetBonusesLoader.Load, ArmorSetBonusesLoader.Unload);
        }

        private static void RegistLoader<T>(Action<T> reg, Action load, Action unload) where T : ExtenType
        {
            RegistFoo<T>.Foo = reg;
            Loads.Add(load);
            Unloads.Add(unload);
        }

        internal static void Initialize(ModObject mo)
        {
            lock (_lock)
            {
                if (IsInited) return;
            }

            ThisMod.mo = mo;

            ModObject newmo = new ModObject(new ModConfig());
            newmo.info = new ModInfo();
            newmo.modPath = ThisMod.mo.modPath;
            newmo.info.name = ThisMod.mo.info.name;
            newmo.config.key = ThisMod.mo.config.key;
            newmo.assembly = Assembly.GetExecutingAssembly();

            IsInited = true;

            RegistMod(newmo);
        }

        internal static void Load()
        {
            lock (_lock)
            {
                if (IsLoad) return;
                IsLoad = true;
            }

            ThisMod.Log(out DateTime time, $"{nameof(ExtenManag)}:加载");

            Loads.ForEach(i => i());

            Extens.ForEach(i => i.SetStaticDefaults());

            ThisMod.Log($"{nameof(ExtenManag)}:加载完成", time);
        }

        internal static void Unload()
        {
            ThisMod.Log(out DateTime time);

            Unloads.ForEach(i => i());

            foreach (IAssetRepository asset in Assets) asset.Dispose();
            Assets.Clear();

            Extens.Clear();

            IsInited = false;
            IsLoad = false;

            ThisMod.Log($"{nameof(ExtenManag)}:卸载完成", time);
        }

        /// <summary>
        /// 注册模组中所有的扩展内容<br/>
        /// 应在<br/>
        /// <see cref="tContentPatch.Mod.Load(ModObject)"/>时<br/>
        /// <see cref="tContentPatch.Mod.Loaded"/>前<br/>
        /// 调用
        /// </summary>
        public static void RegistMod(ModObject mo)
        {
            if (IsInited != true) throw new Exception($"{nameof(ExtenManag)}:在初始化前不能注册");
            if (IsLoad) throw new Exception($"{nameof(ExtenManag)}:不可在加载后注册");

            ThisMod.Log(out DateTime time);

            IAssetRepository asset = null;
            if (Main.dedServ != true)
            {
                asset = Utils.ExtenAssetRepository.GetAssets(mo);
                Assets.Add(asset);
            }

            RegistExten<ExtenItem>(mo, asset);
            RegistExten<ExtenEquip>(mo, asset);
            RegistExten<ExtenProjectile>(mo, asset);
            RegistExten<ExtenBuff>(mo, asset);
            RegistExten<EPatchPlayer>(mo, asset);
            RegistExten<EPatchRecipe>(mo, asset);
            RegistExten<EPatchArmorSetBonus>(mo, asset);

            ThisMod.Log($"{nameof(ExtenManag)}:注册模组:[{mo.info.name ?? mo.config.key}]", time);
        }

        private static void RegistExten<T>(ModObject mo, IAssetRepository asset) where T : ExtenType
        {
            List<T> extens = LoaderUtils.CreateInstance<T>(mo.assembly);
            if (extens == null) return;

            foreach (T exten in extens)
            {
                exten.Mod = mo;
                if (exten.Asset == null) exten.SetAsset(asset);

                ExtenInstance.Register(exten);

                RegistFoo<T>.Foo(exten);

                Extens.Add(exten);
            }
        }
    }
}
