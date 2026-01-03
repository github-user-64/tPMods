using BedWars.BedWarsData;
using System;
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
                Reset();
            }
        }

        public static MapData mapData = null;
        /// <summary>
        /// 不要往里塞<see langword="null"/>
        /// </summary>
        public static readonly List<SpawItemData> SpawItem = new List<SpawItemData>();
        /// <summary>
        /// 维持时间, 小于0不维持
        /// </summary>
        public static double Time = -1;
        /// <summary>
        /// 是白天, 4:30到7:30
        /// </summary>
        public static bool DayTime = true;

        public static void Reset()
        {
            mapData = null;
            Time = -1;
            DayTime = true;
            SpawItem.Clear();
        }

        private static void DoUpdateInWorldPr()
        {
            try
            {
                if (GameAction.mapData is MapData mapData == false) return;//防止后面被设置为null

                if (Time > -1)
                {
                    Main.time = Time;
                    Main.dayTime = DayTime;
                }

                foreach (SpawItemData data in SpawItem)
                {
                    if (data.cd > 0 && Main.GameUpdateCount % data.cd != 0) continue;

                    data.Spaw(mapData);
                }
            }
            catch { }
        }
    }
}
