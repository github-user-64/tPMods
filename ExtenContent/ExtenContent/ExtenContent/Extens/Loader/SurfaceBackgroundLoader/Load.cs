using System;
using Terraria;
using Terraria.ID;

namespace ExtenContent.Extens
{
    public static partial class SurfaceBackgroundLoader
    {
        private static void ContentInit()
        {
            LoaderUtils.ResetStaticMembers(typeof(SurfaceBackgroundID.Sets));

            Array.Resize(ref Main.bgAlphaFrontLayer, BackCount);
            Array.Resize(ref Main.bgAlphaFarBackLayer, BackCount);
        }
    }
}
