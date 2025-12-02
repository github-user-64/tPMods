using System.Collections.Generic;
using tContentPatch;

namespace PlayerTag
{
    /// <summary>
    /// :p
    /// </summary>
    public class TagSetting : ModSetting
    {
        /// <inheritdoc/>
        public override bool HasUI => false;//不添加ui
        /// <summary>
        /// 标签数据, 建议使用<see cref="TagHelp"/>中的方法修改数据
        /// </summary>
        public static List<TagData> datas { get; internal set; } = null;

        /// <inheritdoc/>
        public override void Load(object v)
        {
            if (v is List<TagData> list)
            {
                list.CheckTagData();
                datas = list;
            }
            else
            {
                datas = new List<TagData>();
            }
        }

        /// <inheritdoc/>
        public override object Read() => TagHelp.Read(ThisMod.FileTag);
    }
}
