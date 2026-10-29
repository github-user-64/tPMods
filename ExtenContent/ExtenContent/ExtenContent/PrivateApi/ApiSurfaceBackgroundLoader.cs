using ExtenContent.Exten;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace ExtenContent.PrivateApi
{
    /// <summary/>
    public class ApiSurfaceBackgroundLoader
    {
        internal ApiSurfaceBackgroundLoader() { }

        /// <summary/>
        public bool UpdateBGVisibility_BackLayerPrefix(int? targetBiomeOverride, float? transitionAmountOverride)
        {
            return SurfaceBackgroundLoader.UpdateBGVisibility_BackLayerPrefix(targetBiomeOverride, transitionAmountOverride);
        }

        /// <summary/>
        public void DrawSurfaceBGPostfix(SpriteBatchBeginner parentSpriteBatchBeginner, float? verticalParallaxOverride)
        {
            SurfaceBackgroundLoader.DrawSurfaceBGPostfix(parentSpriteBatchBeginner, verticalParallaxOverride);
        }

        /// <summary/>
        public bool DrawSunAndMoon(Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
        {
            return SurfaceBackgroundLoader.DrawSunAndMoon(sceneArea, moonColor, sunColor, tempMushroomInfluence);
        }
    }
}
