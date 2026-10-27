using ExtenContent.Extens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;

namespace NewContents.Back
{
    internal class Back1 : ExtenSurfaceBackground
    {
        public override void DrawSurfaceBG(SpriteBatch spriteBatch)
        {
            float surface = (float)Main.worldSurface;
            if (surface == 0f) surface = 1f;
            float y = ((0f - (Main.screenPosition.Y - 300f)) / (surface * 16f));

            int pushBGTopHack = -180;

            Color color;
            if (Main.dayTime != true)
            {
                double time = Main.time;
                double timeMax = Main.nightLength;
                float v = (float)Math.Min(1f / timeMax * time, 1f / timeMax * (timeMax - time));

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                asd(ref color, 1f + v * 16f, 1f - v * 16f, 1f - v * 16f);
                a(spriteBatch, 327, 0.2f, y, pushBGTopHack, color);

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                asd(ref color, 1f + v * 5f, 1f - v, 1f - v);
                a(spriteBatch, 326, 0.6f, y, pushBGTopHack + 100, color);

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                asd(ref color, 1f + v, 1f - v, 1f - v);
                a(spriteBatch, 325, 1.0f, y, pushBGTopHack, color);
            }
            else
            {
                color = Main.ColorOfSurfaceBackgroundsModified;
                a(spriteBatch, 327, 0.2f, y, pushBGTopHack, color);
                a(spriteBatch, 326, 0.6f, y, pushBGTopHack + 100, color);
                a(spriteBatch, 325, 1.0f, y, pushBGTopHack, color);
            }
        }

        protected void asd(ref Color color, float v1, float v2, float v3)
        {
            color.R = (byte)MathHelper.Clamp(color.R * v1, byte.MinValue, byte.MaxValue);
            color.G = (byte)MathHelper.Clamp(color.G * v2, byte.MinValue, byte.MaxValue);
            color.B = (byte)MathHelper.Clamp(color.B * v3, byte.MinValue, byte.MaxValue);
        }

        public override bool DrawSunPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color)
        {
            color = Color.Red;

            double time = Main.time;
            double timeMax = Main.dayLength;
            float v = (float)Math.Min(1f / timeMax * time, 1f / timeMax * (timeMax - time));

            Texture2D p = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Planet").Value;

            Vector2 pos = Main.ScreenSize.ToVector2() / 2f + new Vector2(400, -300);
            spriteBatch.Draw(p, pos, null, Color.White * (1 - v), 1f, p.Size() / 2f, 1f, SpriteEffects.None, 0);

            return true;
        }

        public override void DrawSunPostfix(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale, Color color)
        {
            var _sunflare1Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare1");
            var _sunflare2Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare2");

            Color value = new Color(255, 255, 255, 0);

            float sunsetVisibility = 1;
            float sunriseVisibility = 1;

            spriteBatch.Draw(_sunflare1Texture.Value, position, null, value * sunsetVisibility * 0.75f, -rotation, _sunflare1Texture.Size() / 2f, 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare1Texture.Value, position, null, value * sunsetVisibility * 0.35f, -rotation, _sunflare1Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, value * sunriseVisibility * 0.7f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, value * sunriseVisibility * 0.3f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, value * sunriseVisibility * 0.2f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }

        public override bool DrawMoonPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color)
        {
            color = Color.OrangeRed;

            double time = Main.time;
            double timeMax = Main.nightLength;
            float v = (float)Math.Min(1f / timeMax * time, 1f / timeMax * (timeMax - time));

            Color c = Color.White * 0.8f;
            c *= v;

            spriteBatch.Draw(Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Background").Value,
                new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null,
                c);

            //

            Texture2D p = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Planet").Value;

            Vector2 pos = Main.ScreenSize.ToVector2() / 2f + new Vector2(400, -300);
            spriteBatch.Draw(p, pos, null, Color.White, 1f, p.Size() / 2f, 1f, SpriteEffects.None, 0);

            return true;
        }

        protected void a(SpriteBatch spriteBatch, int index, float v, float backgroundTopMagicNumber, int pushBGTopHack, Color color)
        {
            Main.instance.LoadBackground(index);

            float scAdj = 0;
            float bgParallax = 0.49f;
            float bgScale = 2;
            int bgWidthScaled = (int)(Main.backgroundWidth[index] * bgScale);

            float bgTopY = backgroundTopMagicNumber * 2100f + 2000f + scAdj + pushBGTopHack;
            bgTopY *= v;

            int bgStartX = -(int)Math.IEEERemainder((double)Main.screenPosition.X * v * bgParallax, bgWidthScaled);
            bgStartX -= (int)(bgWidthScaled / 2f);

            if (Main.gameMenu)
            {
                bgTopY = 480 + pushBGTopHack;
                bgStartX -= 120;

                if (bgWidthScaled <= 0) return;
            }

            int bgLoops = Main.screenWidth / bgWidthScaled + 2;

            if (Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
            {
                for (int i = 0; i < bgLoops; ++i)
                {
                    spriteBatch.Draw(TextureAssets.Background[index].Value,
                        new Vector2(bgStartX + bgWidthScaled * i, bgTopY), null,
                        color, 0f, default, bgScale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
