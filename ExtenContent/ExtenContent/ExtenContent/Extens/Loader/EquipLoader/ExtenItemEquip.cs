namespace ExtenContent.Extens
{
    public static partial class EquipLoader
    {
        private class ExtenItemEquip : ExtenEquip
        {
            public ExtenItemEquip() { }
            public ExtenItemEquip(ExtenItem item, EquipType equipType)
            {
                _EquipType = equipType;

                _Texture = $"{item.Texture}_{_EquipType}";
            }

            protected EquipType _EquipType;
            public override EquipType EquipType => _EquipType;

            public string _Texture;
            public override string Texture => _Texture;

            public override bool CanLoad() => false;
        }
    }
}
