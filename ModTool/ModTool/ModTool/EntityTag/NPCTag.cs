using ModTool.AdditionalData;
using System.Collections.Generic;

namespace ModTool.EntityTag
{
    /// <summary>
    /// NPC标签
    /// </summary>
    public class NPCTag : NPCAdditionalData<Dictionary<string, string>>
    {
        /// <inheritdoc/>
        protected override Dictionary<string, string> ConverterThrow(int index)
        {
            return new Dictionary<string, string>();
        }
    }
}
