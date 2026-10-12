using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace ExtenContent.Extens
{
    public static partial class BuffLoader
    {
        private static void ResizeArrays()
        {
            LocalizedText[] _buffNameCache = (LocalizedText[])__buffNameCache.GetValue(null);
            LocalizedText[] _buffDescriptionCache = (LocalizedText[])__buffDescriptionCache.GetValue(null);

            Array.Resize(ref TextureAssets.Buff, BuffCount);

            Utils.Utils.ResetStaticMembers(typeof(BuffID));
            Utils.Utils.ResetStaticMembers(typeof(BuffID.Sets));

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

        private static void Setup()
        {
            LocalizedText[] _buffNameCache = (LocalizedText[])__buffNameCache.GetValue(null);
            LocalizedText[] _buffDescriptionCache = (LocalizedText[])__buffDescriptionCache.GetValue(null);

            for (int i = 0; i < buffs.Count; ++i)
            {
                ExtenBuff eb = buffs[i];
                eb.Type = BuffID.Count + i;

                _buffNameCache[eb.Type] = eb.DisplayName;
                _buffDescriptionCache[eb.Type] = eb.Description;

                if (Main.dedServ != true)
                {
                    TextureAssets.Buff[eb.Type] = eb.Asset.Request<Texture2D>(eb.Texture);
                }
                BuffID.Search.Add(eb.FullName, eb.Type);
            }

            __buffNameCache.SetValue(null, _buffNameCache);
            __buffDescriptionCache.SetValue(null, _buffDescriptionCache);
        }
    }
}
