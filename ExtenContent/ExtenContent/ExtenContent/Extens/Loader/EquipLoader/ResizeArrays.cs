using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Initializers;

namespace ExtenContent.Extens
{
    public static partial class EquipLoader
    {
        private static void ResizeArrays()
        {
            Array.Resize(ref TextureAssets.ArmorHead, HeadCount);

            Array.Resize(ref TextureAssets.ArmorBody, BodyCount);
            Array.Resize(ref TextureAssets.ArmorBodyComposite, BodyCount);
            Array.Resize(ref TextureAssets.FemaleBody, BodyCount);
            Array.Resize(ref TextureAssets.ArmorArm, BodyCount);

            Array.Resize(ref TextureAssets.ArmorLeg, LegsCount);

            Array.Resize(ref TextureAssets.Wings, WingCount);

            Utils.Utils.ResetStaticMembers(typeof(ArmorIDs.Head.Sets));
            Utils.Utils.ResetStaticMembers(typeof(ArmorIDs.Body.Sets));
            Utils.Utils.ResetStaticMembers(typeof(ArmorIDs.Legs.Sets));
            Utils.Utils.ResetStaticMembers(typeof(ArmorIDs.Wing.Sets));

            WingStatsInitializer.Load();

            Array.Resize(ref Item.headType, HeadCount);
            Array.Resize(ref Item.bodyType, BodyCount);
            Array.Resize(ref Item.legType, LegsCount);
        }

        private static void Setup()
        {
            Item item = new Item();
            for (int i = ItemID.Count; i < ItemLoader.ItemCount; ++i)
            {
                item.SetDefaults(i);
                if (item.headSlot > 0)
                {
                    Item.headType[item.headSlot] = item.type;
                }
                if (item.bodySlot > 0)
                {
                    Item.bodyType[item.bodySlot] = item.type;
                }
                if (item.legSlot > 0)
                {
                    Item.legType[item.legSlot] = item.type;
                }
            }

            foreach (ExtenEquip e in equips[EquipType.Body])
            {
                ArmorIDs.Body.Sets.UsesNewFramingCode[e.Slot] = true;
            }

            if (Main.dedServ) return;

            foreach (EquipType type in EquipTypes)
            {
                List<ExtenEquip> es = equips[type];

                foreach (ExtenEquip e in es)
                {
                    GetTextureArray(type)[e.Slot] = e.Asset.Request<Texture2D>(e.Texture);
                }
            }
        }

        private static Asset<Texture2D>[] GetTextureArray(EquipType type)
        {
            switch (type)
            {
                case EquipType.Head: return TextureAssets.ArmorHead;
                case EquipType.Body: return TextureAssets.ArmorBodyComposite;
                case EquipType.Legs: return TextureAssets.ArmorLeg;
                case EquipType.Wings: return TextureAssets.Wings;
                default: throw new Exception($"no {type}");
            }
        }
    }
}
