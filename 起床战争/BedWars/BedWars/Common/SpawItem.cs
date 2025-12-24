using BedWars.BedWarsData;
using ModTool.PatchGame;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Common
{
    public static class SpawItem
    {
        

        internal static void Init()
        {
            PMain.OnDoUpdateInWorldPr += DoUpdateInWorldPr;
        }

        /// <summary>
        /// 不要往里塞<see langword="null"/>
        /// </summary>
        public static readonly List<SpawItemData> SpawDatas = new List<SpawItemData>();

        private static void DoUpdateInWorldPr()
        {
            try
            {
                foreach (SpawItemData data in SpawDatas)
                {
                    if (data.cd > 0 && Main.GameUpdateCount % data.cd != 0) continue;

                    data.Spaw();
                }
            }
            catch { }
        }
    }
}
