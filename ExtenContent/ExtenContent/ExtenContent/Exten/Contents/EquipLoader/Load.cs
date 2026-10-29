using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Initializers;

namespace ExtenContent.Exten
{
    public static partial class EquipLoader
    {
        private static void ContentInit()
        {
            LoaderUtils.ResetStaticMembers(typeof(ArmorIDs.Head.Sets));
            LoaderUtils.ResetStaticMembers(typeof(ArmorIDs.Body.Sets));
            LoaderUtils.ResetStaticMembers(typeof(ArmorIDs.Legs.Sets));
            LoaderUtils.ResetStaticMembers(typeof(ArmorIDs.Wing.Sets));

            Array.Resize(ref TextureAssets.ArmorHead, EquipCount[EquipType.Head]);

            Array.Resize(ref TextureAssets.ArmorBody, EquipCount[EquipType.Body]);
            Array.Resize(ref TextureAssets.ArmorBodyComposite, EquipCount[EquipType.Body]);
            Array.Resize(ref TextureAssets.FemaleBody, EquipCount[EquipType.Body]);
            Array.Resize(ref TextureAssets.ArmorArm, EquipCount[EquipType.Body]);

            Array.Resize(ref TextureAssets.ArmorLeg, EquipCount[EquipType.Legs]);

            Array.Resize(ref TextureAssets.Wings, EquipCount[EquipType.Wings]);

            Array.Resize(ref Item.headType, EquipCount[EquipType.Head]);
            Array.Resize(ref Item.bodyType, EquipCount[EquipType.Body]);
            Array.Resize(ref Item.legType, EquipCount[EquipType.Legs]);

            WingStatsInitializer.Load();
        }

        private static void ContentLoad()
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
                default: throw new Exception($"没有该纹理类型:{type}");
            }
        }
    }
}
