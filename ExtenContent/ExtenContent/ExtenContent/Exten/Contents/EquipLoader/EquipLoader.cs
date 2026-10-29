using ExtenContent.Exten.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Exten
{
    /// <summary>
    /// 装备加载器
    /// </summary>
    public static partial class EquipLoader
    {
        /// <summary/>
        public static readonly EquipType[] EquipTypes = (EquipType[])Enum.GetValues(typeof(EquipType));
        /// <summary/>
        public static int HeadCount => EquipCount[EquipType.Head];
        /// <summary/>
        public static int BodyCount => EquipCount[EquipType.Body];
        /// <summary/>
        public static int LegsCount => EquipCount[EquipType.Legs];
        /// <summary/>
        public static int WingCount => EquipCount[EquipType.Wings];

        private static readonly Dictionary<EquipType, int> EquipCount = EquipTypes.ToDictionary(i => i, i => GetEquipOriginCount(i));
        private static readonly Dictionary<EquipType, List<ExtenEquip>> equips = EquipTypes.ToDictionary(i => i, i => new List<ExtenEquip>());

        private static readonly Dictionary<int, Dictionary<EquipType, int>> itemToSlot = new Dictionary<int, Dictionary<EquipType, int>>();

        internal static void Load()
        {
            ThisMod.Log(out DateTime time);

            ContentInit();
            ContentLoad();

            foreach (KeyValuePair<EquipType, List<ExtenEquip>> ees in equips)
            {
                ees.Value.ForEach(i => i.Load());
            }

            foreach (EquipType i in EquipTypes)
            {
                ThisMod.Log($"{typeof(EquipLoader)}:加载完成:新{i}:{GetEquipCount(i) - GetEquipOriginCount(i)}个", time);
            }
        }

        internal static void Unload()
        {
            foreach (EquipType i in EquipTypes)
            {
                EquipCount[i] = GetEquipOriginCount(i);
            }

            foreach (KeyValuePair<EquipType, List<ExtenEquip>> ees in equips)
            {
                ees.Value.ForEach(i => i.Unload());
                ees.Value.Clear();
            }

            itemToSlot.Clear();

            ContentInit();
        }

        internal static void Register(ExtenEquip equip)
        {
            equips[equip.EquipType].Add(equip);

            equip.Slot = EquipCount[equip.EquipType]++;
        }

        /// <summary>
        /// 根据<paramref name="item"/>的<see cref="AutoloadEquip"/>注册
        /// </summary>
        internal static void RegisterItem(ExtenItem item)
        {
            AutoloadEquip al = item.GetType().GetCustomAttribute<AutoloadEquip>();
            if (al?.equipTypes == null) return;

            foreach (EquipType i in al.equipTypes)
            {
                ExtenEquip e = new ExtenItemEquip(item, i);

                Register(e);

                if (itemToSlot.ContainsKey(item.Type) != true) itemToSlot[item.Type] = new Dictionary<EquipType, int>();
                itemToSlot[item.Type][i] = e.Slot;
            }
        }

        internal static void SetSlot(Item item)
        {
            if (ExtenManag.IsExtenItem(item.type) != true) return;

            if (itemToSlot.TryGetValue(item.type, out Dictionary<EquipType, int> v) != true) return;

            foreach (KeyValuePair<EquipType, int> i in v)
            {
                switch (i.Key)
                {
                    case EquipType.Head: item.headSlot = i.Value; break;
                    case EquipType.Body: item.bodySlot = i.Value; break;
                    case EquipType.Legs: item.legSlot = i.Value; break;
                    case EquipType.Wings: item.wingSlot = (sbyte)i.Value; break;
                }
            }
        }

        /// <summary>
        /// 获取物品的slot
        /// </summary>
        public static int GetItemSlot(EquipType type, Item item)
        {
            switch (type)
            {
                case EquipType.Head: return item.headSlot;
                case EquipType.Body: return item.bodySlot;
                case EquipType.Legs: return item.legSlot;
                case EquipType.Wings: return item.wingSlot;
                default: return -1;
            }
        }

        /// <summary>
        /// 获取原版装备数量
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
        /// 获取装备数量
        /// </summary>
        public static int GetEquipCount(EquipType type)
        {
            return EquipCount[type];
        }

        /// <summary>
        /// <see cref="ArmorIDs"/>是否是<see cref="ExtenEquip"/>
        /// </summary>
        public static bool IsExten(EquipType type, int slot)
        {
            return GetEquipOriginCount(type) <= slot && slot < GetEquipCount(type);
        }

        /// <summary>
        /// 获取<see cref="ExtenEquip"/>, 不存在返回<see langword="null"/>
        /// </summary>
        public static ExtenEquip GetExtenEquip(EquipType type, int slot)
        {
            if (IsExten(type, slot) != true) return null;

            return equips[type][slot - GetEquipOriginCount(type)];
        }
    }
}
