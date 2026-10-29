using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace ExtenContent.Exten
{
    public static partial class BuffLoader
    {
        private static void ContentInit()
        {
            LocalizedText[] _buffNameCache = (LocalizedText[])__buffNameCache.GetValue(null);
            LocalizedText[] _buffDescriptionCache = (LocalizedText[])__buffDescriptionCache.GetValue(null);

            LoaderUtils.ResetStaticMembers(typeof(BuffID));
            LoaderUtils.ResetStaticMembers(typeof(BuffID.Sets));

            Array.Resize(ref TextureAssets.Buff, BuffCount);
            Array.Resize(ref Main.pvpBuff, BuffCount);
            Array.Resize(ref Main.persistentBuff, BuffCount);
            Array.Resize(ref Main.vanityPet, BuffCount);
            Array.Resize(ref Main.lightPet, BuffCount);
            Array.Resize(ref Main.meleeBuff, BuffCount);
            Array.Resize(ref Main.debuff, BuffCount);
            Array.Resize(ref Main.buffNoSave, BuffCount);
            Array.Resize(ref Main.buffNoTimeDisplay, BuffCount);
            Array.Resize(ref Main.buffDoubleApply, BuffCount);
            Array.Resize(ref Main.buffAlpha, BuffCount);
            Array.Resize(ref _buffNameCache, BuffCount);
            Array.Resize(ref _buffDescriptionCache, BuffCount);

            for (int i = BuffID.Count; i < BuffCount; ++i)
            {
                _buffNameCache[i] = LocalizedText.Empty;
                _buffDescriptionCache[i] = LocalizedText.Empty;
            }

            __buffNameCache.SetValue(null, _buffNameCache);
            __buffDescriptionCache.SetValue(null, _buffDescriptionCache);
        }

        private static void ContentLoad()
        {
            LocalizedText[] _buffNameCache = (LocalizedText[])__buffNameCache.GetValue(null);
            LocalizedText[] _buffDescriptionCache = (LocalizedText[])__buffDescriptionCache.GetValue(null);

            foreach (ExtenBuff i in buffs)
            {
                _buffNameCache[i.Type] = i.DisplayName;
                _buffDescriptionCache[i.Type] = i.Description;

                BuffID.Search.Add(i.FullName, i.Type);

                if (Main.dedServ) continue;

                TextureAssets.Buff[i.Type] = i.Asset.Request<Texture2D>(i.Texture);
            }

            __buffNameCache.SetValue(null, _buffNameCache);
            __buffDescriptionCache.SetValue(null, _buffDescriptionCache);
        }
    }
}
