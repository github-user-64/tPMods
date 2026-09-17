using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 装备加载
    /// </summary>
    public static partial class EquipLoader
    {
        /// <summary/>
        public static int WingCount { get; private set; } = ArmorIDs.Wing.Count;
        private static readonly Dictionary<EquipType, List<ExtenEquip>> equips = new Dictionary<EquipType, List<ExtenEquip>>()
        {
            { EquipType.Wings, new List<ExtenEquip>() },
        };

        internal static void Load()
        {
            ResizeArrays();
            Setup();

            foreach (KeyValuePair<EquipType, List<ExtenEquip>> ees in equips)
            {
                ees.Value.ForEach(i => i.Load());
            }
        }

        internal static void Unload()
        {
            foreach (KeyValuePair<EquipType, List<ExtenEquip>> ees in equips)
            {
                ees.Value.ForEach(i => i.Unload());
            }

            WingCount = ArmorIDs.Wing.Count;

            foreach (List<ExtenEquip> i in equips.Values) i.Clear();
        }

        internal static void Register(ExtenEquip equip)
        {
            equips[equip.EquipType].Add(equip);

            int slot = -1;

            switch (equip.EquipType)
            {
                case EquipType.Wings: slot = WingCount++; break;
            }

            equip.Slot = slot;
        }

        private static void Setup()
        {
            foreach (ExtenEquip i in equips[EquipType.Wings])
            {
                TextureAssets.Wings[i.Slot] = i.Asset.Request<Texture2D>(i.Texture);
            }
        }

        /// <summary>
        /// 获取<see cref="WingCount"/>对应的<see cref="ExtenEquip"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenEquip GetEquipWing(int slot)
        {
            if ((ArmorIDs.Wing.Count <= slot && slot < WingCount) != true) return null;

            return equips[EquipType.Wings][slot - ArmorIDs.Wing.Count];
        }
    }
}
