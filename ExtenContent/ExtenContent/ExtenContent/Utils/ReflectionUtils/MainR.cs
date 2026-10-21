using System;
using Terraria;
using Terraria.DataStructures;

namespace ExtenContent.Utils
{
    internal static partial class ReflectionUtils
    {
        public static class MainR
        {
            private static readonly Type type = typeof(Main);
            private static readonly R r = new R(type);

            public static float scAdj
            {
                get => r.Field<float>(nameof(scAdj), Main.instance);
                set => r.Field(nameof(scAdj), Main.instance, value);
            }
            public static float bgScale
            {
                get => r.Field<float>(nameof(bgScale), null);
                set => r.Field(nameof(bgScale), null, value);
            }
            public static int bgWidthScaled
            {
                get => r.Field<int>(nameof(bgWidthScaled), null);
                set => r.Field(nameof(bgWidthScaled), null, value);
            }
            public static float backgroundLayerTransitionSpeed
            {
                get => r.Field<float>(nameof(backgroundLayerTransitionSpeed), null);
                set => r.Field(nameof(backgroundLayerTransitionSpeed), null, value);
            }

            public static float ComputeScAdj(float screenY, float screenHeight) =>
                r.Method<float>(nameof(ComputeScAdj), null, screenY, screenHeight);

            public static void DrawBG_HandleBackgroundTransition(int newBackgroundStyle) =>
                r.Method<object>(nameof(DrawBG_HandleBackgroundTransition), Main.instance, newBackgroundStyle);

            public static void UpdateBGVisibility_BackLayer(int? targetBiomeOverride, float? transitionAmountOverride) =>
                r.Method<object>(nameof(UpdateBGVisibility_BackLayer), Main.instance, targetBiomeOverride, transitionAmountOverride);

            public static void UpdateBGVisibility_FrontLayer(int? targetBiomeOverride, float? transitionAmountOverride) =>
                r.Method<object>(nameof(UpdateBGVisibility_FrontLayer), Main.instance, targetBiomeOverride, transitionAmountOverride);

            public static void DrawSurfaceBG(SpriteBatchBeginner parentSpriteBatchBeginner, float? verticalParallaxOverride = null) =>
                r.Method<object>(nameof(DrawSurfaceBG), Main.instance, parentSpriteBatchBeginner, verticalParallaxOverride);

            public static void DrawUnderworldBackground(float zoomForPushUp = 1f) =>
                r.Method<object>(nameof(DrawUnderworldBackground), Main.instance, zoomForPushUp);

            public static void DrawBG_ModifyBGFarBackLayerAlpha(int desiredBG, int? desiredBG2 = null, float? transitionAmountOverride = null) =>
                r.Method<object>(nameof(DrawBG_ModifyBGFarBackLayerAlpha), Main.instance, desiredBG, desiredBG2, transitionAmountOverride);
        }
    }
}
