using ModTool.AdditionalData;
using System.Collections.Generic;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 射弹标签
    /// </summary>
    public class ProjectileTag : ProjectileAdditionalData<Dictionary<string, string>>
    {
        /// <inheritdoc/>
        protected override Dictionary<string, string> ConverterThrow(int index)
        {
            return new Dictionary<string, string>();
        }
    }
}
