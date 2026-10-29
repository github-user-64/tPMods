using System;

namespace ExtenContent.Exten.Attributes
{
    /// <summary>
    /// 自动注册装备
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoloadEquip : Attribute
    {
        /// <summary/>
        public AutoloadEquip(params EquipType[] equipTypes) => this.equipTypes = equipTypes;

        /// <summary/>
        public readonly EquipType[] equipTypes;
    }
}
