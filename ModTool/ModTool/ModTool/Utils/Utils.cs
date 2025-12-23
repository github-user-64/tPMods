using System;
using Terraria;

namespace ModTool.Utils
{
    /// <summary>
    /// 工具
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 获取随机数<see langword="int"/>,
        /// </summary>
        /// <returns>v1=1,v2=2,return 1</returns>
        public static int GetRand(int v1, int v2)
        {
            return Main.rand.Next(Math.Min(v1, v2), Math.Max(v1, v2));
        }

        /// <summary>
        /// 获取随机数<see langword="float"/>,
        /// </summary>
        /// <returns>0.9-0.1</returns>
        public static float GetRandFloat()
        {
            return Main.rand.NextFloat();
        }
    }
}
