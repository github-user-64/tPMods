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
        /// <summary>在收到数据前</summary>
        public static List<GetDataEvent> OnGetData { get; } = new List<GetDataEvent>();
        /// <summary>在收到数据后</summary>
        public static List<GetDataEvent> OnGetDataPo { get; } = new List<GetDataEvent>();

        [HarmonyPatch("GetData")]
        [HarmonyPrefix]
        internal static void GetData(MessageBuffer __instance, int start, int length, int messageType)
        {
            messageType = __instance.readBuffer[start];

            if (__instance.reader == null)
            {
                __instance.ResetReader();
            }

            //

            OnGetData.RemoveAll(i => i == null);

            foreach (GetDataEvent i in OnGetData)
            {
                __instance.reader.BaseStream.Position = start + 1;

                try
                {
                    i(__instance, start, length, messageType);
                }
                catch { }
            }

            __instance.reader.BaseStream.Position = start + 1;
        }

        [HarmonyPatch("GetData")]
        [HarmonyPostfix]
        internal static void GetDataPo(MessageBuffer __instance, int start, int length, int messageType)
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

            __instance.reader.BaseStream.Position = start + 1;
        }
    }
}
