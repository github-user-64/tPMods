using tContentPatch.ModLoad;

namespace ExtenContent
{
    /// <summary/>
    public class ThisMod
    {
        /// <summary/>
        public const string ModKey = "StaticTile.ExtenContent";
        internal static ModObject mo = null;

        /// <summary>
        /// 用于在游戏版本更新时<br/>
        /// 定位比较需要查看代码更改的地方
        /// </summary>
        internal abstract class TerrariaVersionCheck { }
    }
}
