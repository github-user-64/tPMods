using System;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Initializers;

namespace ExtenContent.Extens
{
    public static partial class EquipLoad
    {
        private static void ResizeArrays()
        {
            Array.Resize(ref TextureAssets.Wings, WingCount);

            Utils.Utils.ResetStaticMembers(typeof(ArmorIDs.Wing.Sets));

            WingStatsInitializer.Load();
        }
    }
}
