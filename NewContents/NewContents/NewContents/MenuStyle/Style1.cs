using ExtenContent.Extens;

namespace NewContents.MenuStyle
{
    internal class Style1 : ExtenMenuStyle
    {
        public override string DisplayName => "超级新1";
        public override ExtenSurfaceBackground Back => ExtenInstance<Back1>.Instance;
    }
}
