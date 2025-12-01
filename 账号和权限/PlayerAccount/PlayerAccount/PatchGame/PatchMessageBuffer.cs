using HarmonyLib;
using Terraria;

namespace PlayerAccount.PatchGame
{
    /// <summary>
    /// 修补<see cref="MessageBuffer"/>
    /// </summary>
    [HarmonyPatch(typeof(MessageBuffer))]
    public class PatchMessageBuffer
    {
        /// <summary/>
        public delegate void GetDataEvent(MessageBuffer This, int start, int length, int messageType);
        /// <summary>
        /// 在收到数据后
        /// </summary>
        public static event GetDataEvent OnGetDataPo = null;

        [HarmonyPatch("GetData")]
        [HarmonyPostfix]
        internal static void GetData(MessageBuffer __instance, int start, int length, int messageType)
        {
            int num = start + 1;

            __instance.reader.BaseStream.Position = num;

            OnGetDataPo?.Invoke(__instance, start, length, messageType);
        }
    }
}
