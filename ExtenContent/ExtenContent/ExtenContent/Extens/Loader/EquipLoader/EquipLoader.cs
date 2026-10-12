using System;
using System.Collections.Generic;
using Terraria.ID;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 装备加载器
    /// </summary>
    public static partial class EquipLoader
    {
        /// <summary/>
        public static int HeadCount { get; private set; } = ArmorIDs.Head.Count;
        /// <summary/>
        public static int BodyCount { get; private set; } = ArmorIDs.Body.Count;
        /// <summary/>
        public static int LegsCount { get; private set; } = ArmorIDs.Legs.Count;
        /// <summary/>
        public static int WingCount { get; private set; } = ArmorIDs.Wing.Count;
        /// <summary/>
        public static readonly EquipType[] EquipTypes = (EquipType[])Enum.GetValues(typeof(EquipType));
        private static readonly Dictionary<EquipType, List<ExtenEquip>> equips = new Dictionary<EquipType, List<ExtenEquip>>()
        {
            { EquipType.Head, new List<ExtenEquip>() },
            { EquipType.Body, new List<ExtenEquip>() },
            { EquipType.Legs, new List<ExtenEquip>() },
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

            HeadCount = ArmorIDs.Head.Count;
            BodyCount = ArmorIDs.Body.Count;
            LegsCount = ArmorIDs.Legs.Count;
            WingCount = ArmorIDs.Wing.Count;

            foreach (List<ExtenEquip> i in equips.Values) i.Clear();

            ResizeArrays();
        }

        internal static void Register(ExtenEquip equip)
        {
            equips[equip.EquipType].Add(equip);

            int slot = -1;

            switch (equip.EquipType)
            {
                case EquipType.Head: slot = HeadCount++; break;
                case EquipType.Body: slot = BodyCount++; break;
                case EquipType.Legs: slot = LegsCount++; break;
                case EquipType.Wings: slot = WingCount++; break;
            }

            equip.Slot = slot;
        }

        /// <summary>
        /// <see cref="EquipType.Head"/>-><see cref="ArmorIDs.Head.Count"/>
        /// </summary>
        public static int GetEquipOriginCount(EquipType type)
        {
            switch (type)
            {
                case EquipType.Head: return ArmorIDs.Head.Count;
                case EquipType.Body: return ArmorIDs.Body.Count;
                case EquipType.Legs: return ArmorIDs.Legs.Count;
                case EquipType.Wings: return ArmorIDs.Wing.Count;
                default: return 0;
            }
        }

        /// <summary>
        /// <see cref="EquipType.Head"/>-><see cref="HeadCount"/>
        /// </summary>
        public static int GetEquipCount(EquipType type)
        {
            switch (type)
            {
                case EquipType.Head: return HeadCount;
                case EquipType.Body: return BodyCount;
                case EquipType.Legs: return LegsCount;
                case EquipType.Wings: return WingCount;
                default: return 0;
            }
        }

        /// <summary>
        /// 是否是<see cref="ExtenEquip"/>
        /// </summary>
        public static bool TypeInRange(EquipType type, int slot)
        {
            return GetEquipOriginCount(type) <= slot && slot < GetEquipCount(type);
        }

        /// <summary>
        /// 获取<see cref="ExtenEquip"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenEquip GetEquip(EquipType type, int slot)
        {
            if (TypeInRange(type, slot) != true) return null;

            return equips[type][slot - GetEquipOriginCount(type)];
        }
    }
}
