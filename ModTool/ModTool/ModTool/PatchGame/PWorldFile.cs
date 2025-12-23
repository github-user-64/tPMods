using Terraria.IO;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="WorldFile"/>
    /// </summary>
    public class PWorldFile : tContentPatch.PatchWorldFile
    {
        /// <summary/>
        public delegate void SaveWorldEvent(bool useCloudSaving, bool resetTime);
        /// <summary>
        /// 在保存世界前, 单人和服务端有效
        /// </summary>
        public static event SaveWorldEvent OnSaveWorldPr = null;
        /// <summary>
        /// 在保存世界后, 单人和服务端有效
        /// </summary>
        public static event SaveWorldEvent OnSaveWorldPo = null;

        /// <inheritdoc/>
        public override void SaveWorldPrefix(bool useCloudSaving, bool resetTime)
        {
            OnSaveWorldPr?.Invoke(useCloudSaving, resetTime);
        }

        /// <inheritdoc/>
        public override void SaveWorldPostfix(bool useCloudSaving, bool resetTime)
        {
            OnSaveWorldPo?.Invoke(useCloudSaving, resetTime);
        }
    }
}
