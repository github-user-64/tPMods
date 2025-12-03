using ModTool.AdditionalData;
using System.Collections.Generic;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 物品标签
    /// </summary>
    public class ItemTag : ItemAdditionalData<Dictionary<string, string>>
    {
        /// <inheritdoc/>
        public override Dictionary<string, string> ConverterThrow(int index)
        {
            return new Dictionary<string, string>();
        }
    }
}
