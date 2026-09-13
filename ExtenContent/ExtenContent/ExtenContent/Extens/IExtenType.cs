using tContentPatch.ModLoad;

namespace ExtenContent.Extens
{
    public interface IExtenType
    {
        ModObject Mod { get; }
        string Name { get; }
        string FullName { get; }
    }
}
