using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;

namespace NewContents.Back
{
    internal class Back1 : ExtenSurfaceBackground
    {
        private static float intensity = 0.6f;
        private static float lifeIntensity = 1f;
        private static float specialColorLerp = 0f;
        private static Color? specialColor = null;
        private static int delay = 0;
        private static readonly int[] xPos = new int[50];
        private static readonly int[] yPos = new int[50];
        public static int fadeIn = 0;

        public override void DrawSurfaceBG(SpriteBatch spriteBatch)
        {
            fadeIn++;
            float opacity = 0f;
            Color color = ColorToUse(ref opacity);
            spriteBatch.Draw(Asset.Request<Texture2D>("MutantSky").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color * opacity);
            int num = delay - 1;
            delay = num;
            bool flag = num < 0;
            if (flag)
            {
                delay = Main.rand.Next(5 + (int)(85f * (1f - lifeIntensity)));
                for (int i = 0; i < 50; i++)
                {
                    xPos[i] = Main.rand.Next(Main.screenWidth);
                    yPos[i] = Main.rand.Next(Main.screenHeight);
                }
            }
            for (int j = 0; j < 50; j++)
            {
                int width = Main.rand.Next(3, 251);
                spriteBatch.Draw(Asset.Request<Texture2D>("MutantStatic").Value, new Rectangle(xPos[j] - width / 2, yPos[j], width, 3), color * 1f * 0.75f);
            }
        }

        private Color ColorToUse(ref float opacity)
        {
            Color color = new Color(51, 255, 191);
            opacity = intensity * 0.5f + lifeIntensity * 0.5f;
            opacity *= Math.Min((float)fadeIn / 60f, 1f);
            opacity *= Main.bgAlphaFarBackLayer[Type];
            bool flag = specialColorLerp > 0f && specialColor != null;
            if (flag)
            {
                color = Color.Lerp(color, specialColor.Value, specialColorLerp);
                bool flag2 = specialColor == Color.Black;
                if (flag2)
                {
                    opacity = Math.Min(1f, opacity + Math.Min(intensity, lifeIntensity) * 0.5f);
                }
            }
            return color;
        }
    }
}
