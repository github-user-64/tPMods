using BedWars.BedWarsData;
using System.Collections.Generic;
using System.Diagnostics;
using tContentPatch;
using Terraria;

namespace BedWars.Common
{
    public static class SpawItem
    {
        private class sad : PatchMain
        {
            public override void DoUpdateInWorldPrefix(Stopwatch sw)
            {
                DoUpdateInWorldPr();
            }

            public override void OnEnterWorldPrefix()
            {
                SpawDatas.Clear();
            }
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
