using BedWars.BedWarsData;
using System.Collections.Generic;
using System.Diagnostics;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace BedWars.Common
{
    public static class GameAction
    {
        private class gameAction : PatchMain
        {
            public override void Initialize()
            {
                ModTool.PatchGame.PNPC.OnCanSpawnNPC += () => CanSpawnNPC;
                ModTool.PatchGame.PPlayer.OnCanDropTombstone += (_1, _2, _3, _4) => CanDropTombstone;
            }

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
        /// <summary>
        /// 服务端同步时间间隔
        /// </summary>
        public static int SyncTimeCD = 0;
        /// <summary>
        /// 能否自然生成npc
        /// </summary>
        public static bool CanSpawnNPC = true;
        /// <summary>
        /// 能否掉落墓碑
        /// </summary>
        public static bool CanDropTombstone = true;

        public static void Reset()
        {
            mapData = null;
            SpawItem.Clear();
            Time = -1;
            DayTime = true;
            SyncTimeCD = 0;
            //CanSpawnNPC = true;
            //CanDropTombstone = true;
        }

        private static void DoUpdateInWorldPr()
        {
            try
            {
                if (GameAction.mapData is MapData mapData == false) return;//防止后面被设置为null

                UpdateTime();

                foreach (SpawItemData data in SpawItem)
                {
                    if (data.cd > 0 && Main.GameUpdateCount % data.cd != 0) continue;

                    data.Spaw(mapData);
                }
            }
            catch { }
        }

        private static void UpdateTime()
        {
            if (Time < 0) return;

            Main.time = Time;
            Main.dayTime = DayTime;

            if (Main.dedServ == false) return;
            
            if (SyncTimeCD < 1 || Main.GameUpdateCount % SyncTimeCD == 0)
            {
                NetMessage.TrySendData(MessageID.SetTime);
            }
        }
    }
}
