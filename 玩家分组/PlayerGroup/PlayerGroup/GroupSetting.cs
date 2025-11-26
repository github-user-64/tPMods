using System.Collections.Generic;
using System.IO;
using System.Linq;
using tContentPatch;
using tContentPatch.Utils;

namespace PlayerGroup
{
    /// <summary>
    /// :p
    /// </summary>
    public class GroupSetting : ModSetting
    {
        /// <inheritdoc/>
        public override bool HasUI => false;//不添加ui
        /// <summary>
        /// 分组数据, 建议使用<see cref="GroupHelp"/>中的方法修改分组
        /// </summary>
        public static List<GroupData> datas { get; internal set; } = null;

        /// <inheritdoc/>
        public override void Load(object v)
        {
            if (v is List<GroupData> list)
            {
                datas = list;
            }
            else
            {
                datas = new List<GroupData>();
            }
        }

        /// <inheritdoc/>
        public override object Read() => GroupHelp.Read();
    }
}
