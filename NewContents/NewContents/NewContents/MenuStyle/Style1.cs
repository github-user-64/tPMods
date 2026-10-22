using ExtenContent.Extens;

namespace NewContents.MenuStyle
{
    internal class Style1 : ExtenMenuStyle
    {
        public override string DisplayName => "欸嘿";
        public override int Back => ExtenManag.BackType<Back.Back1>();
        public override int Music => ExtenManag.MusicType<Assets.Sounds.Music1>();
    }
}
