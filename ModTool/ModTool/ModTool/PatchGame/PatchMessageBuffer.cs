using HarmonyLib;
using System.Collections.Generic;
using Terraria;

namespace ModTool.PatchGame
{
    /// <summary>
    /// 修补<see cref="MessageBuffer"/>
    /// </summary>
    [HarmonyPatch(typeof(MessageBuffer))]
    public class PatchMessageBuffer
    {
        /// <summary/>
        public delegate void GetDataEvent(MessageBuffer This, int start, int length, int messageType);
        /// <summary>在收到数据后</summary>
        public static List<GetDataEvent> OnGetDataPo { get; } = new List<GetDataEvent>();

        [HarmonyPatch("GetData")]
        [HarmonyPostfix]
        internal static void GetData(MessageBuffer __instance, int start, int length, int messageType)
        {
            OnGetDataPo.RemoveAll(i => i == null);

            foreach (GetDataEvent i in OnGetDataPo)
            {
                __instance.reader.BaseStream.Position = start + 1;

                try
                {
                    i(__instance, start, length, messageType);
                }
                catch { }
            }
        }
    }
}
