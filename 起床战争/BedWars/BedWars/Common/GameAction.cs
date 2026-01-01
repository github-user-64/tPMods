using BedWars.BedWarsData;
using System.Collections.Generic;
using System.Diagnostics;
using tContentPatch;
using Terraria;

namespace BedWars.Common
{
    public static class GameAction
    {
        private class sad : PatchMain
        {
            public override void DoUpdateInWorldPrefix(Stopwatch sw)
            {
                DoUpdateInWorldPr();
            }

            public override void OnEnterWorldPrefix()
            {
                Time = -1;
                SpawItem.Clear();
            }
        }

        /// <summary>
        /// 不要往里塞<see langword="null"/>
        /// </summary>
        public static readonly List<SpawItemData> SpawItem = new List<SpawItemData>();
        /// <summary>
        /// 维持时间, 小于0不维持
        /// </summary>
        public static double Time = -1;

        private static void DoUpdateInWorldPr()
        {
            try
            {
                if (Time > -1)
                {
                    Main.time = Time;
                }

                foreach (SpawItemData data in SpawItem)
                {
                    if (data.cd > 0 && Main.GameUpdateCount % data.cd != 0) continue;

                    data.Spaw();
                }
            }
            catch { }
        }
    }
}
