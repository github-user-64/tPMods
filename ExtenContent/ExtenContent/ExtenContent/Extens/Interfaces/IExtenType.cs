using tContentPatch.ModLoad;

namespace ExtenContent.Extens
{
    /// <summary>
    /// 扩展类型
    /// </summary>
    public interface IExtenType
    {
        /// <summary>
        /// 来自哪个模组
        /// </summary>
        ModObject Mod { get; }
        /// <summary>
        /// 类名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 命名空间加类名
        /// </summary>
        string FullName { get; }
    }
}
