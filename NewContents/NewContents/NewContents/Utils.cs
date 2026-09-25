using System;

namespace NewContents
{
    internal static class Utils
    {
        public static int getRand(int v, int v2)//v=1,v2=2,return 1
        {
            return Terraria.Main.rand.Next(Math.Min(v, v2), Math.Max(v, v2));
        }
    }
}
