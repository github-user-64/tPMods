using Terraria;
using Terraria.DataStructures;
using static ExtenContent.Utils.ReflectionUtils;

namespace ExtenContent.Exten
{
    public static partial class SurfaceBackgroundLoader
    {
        internal static bool UpdateBGVisibility_BackLayerPrefix(int? targetBiomeOverride, float? transitionAmountOverride)
        {
            int value = Main.bgStyle;
            if (IsExten(value) != true) return true;
            if (ExtenManag.IsLoaded != true) return true;

            if (targetBiomeOverride.HasValue) value = targetBiomeOverride.Value;

            MainR.DrawBG_ModifyBGFarBackLayerAlpha(value, null, transitionAmountOverride);

            return false;
        }

        internal static void DrawSurfaceBGPostfix(SpriteBatchBeginner parentSpriteBatchBeginner, float? verticalParallaxOverride)
        {
            if (Main.BackgroundEnabled != true) return;
            if (ExtenManag.IsLoaded != true) return;

            foreach (ExtenSurfaceBackground i in backs)
            {
                if (Main.bgAlphaFrontLayer[i.Type] <= 0) continue;

                i.DrawSurfaceBG(Main.spriteBatch);
            }
        }
    }
}
