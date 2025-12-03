using ModTool.AdditionalData;
using System.Collections.Generic;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 玩家标签
    /// </summary>
    public class PlayerTag : PlayerAdditionalData<Dictionary<string, string>>
    {
        /// <inheritdoc/>
        public override Dictionary<string, string> ConverterThrow(int index)
        {
            return new Dictionary<string, string>();
        }
    }
}
