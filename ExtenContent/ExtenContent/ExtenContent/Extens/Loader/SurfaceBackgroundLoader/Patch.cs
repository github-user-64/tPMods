using Terraria;
using Terraria.DataStructures;
using static ExtenContent.Utils.ReflectionUtils;

namespace ExtenContent.Extens
{
    public static partial class SurfaceBackgroundLoader
    {
        internal static bool UpdateBGVisibility_BackLayerPrefix(int? targetBiomeOverride, float? transitionAmountOverride)
        {
            if (TypeInRange(Main.bgStyle) != true) return true;

            int value = Main.bgStyle;
            if (targetBiomeOverride.HasValue) value = targetBiomeOverride.Value;

            MainR.DrawBG_ModifyBGFarBackLayerAlpha(value, null, transitionAmountOverride);

            return false;
        }

        internal static void DrawSurfaceBGPostfix(SpriteBatchBeginner parentSpriteBatchBeginner, float? verticalParallaxOverride)
        {
            if (Main.BackgroundEnabled != true) return;

            foreach (ExtenSurfaceBackground i in backs)
            {
                if (Main.bgAlphaFrontLayer[i.Type] <= 0) continue;

                i.DrawSurfaceBG(Main.spriteBatch);
            }
        }
    }
}
