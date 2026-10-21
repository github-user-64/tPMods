using ExtenContent.Extens;

namespace NewContents.MenuStyle
{
    internal class Style1 : ExtenMenuStyle
    {
        public override string DisplayName => "欸嘿";
        public override ExtenSurfaceBackground Back => ExtenInstance<Back.Back1>.Instance;
    }
}
