using ExtenContent.Exten;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;

namespace NewContents.Back
{
    internal class Back1 : ExtenSurfaceBackground
    {
        protected float opacity = 1f;

        protected float GetV()
        {
            double time = Main.time;
            double timeMax = Main.dayTime ? Main.dayLength : Main.nightLength;
            double v = (double)Math.Min(1d / timeMax * time, 1d / timeMax * (timeMax - time));

            return (float)v;
        }

        public override void DrawSurfaceBG(SpriteBatch spriteBatch)
        {
            float surface = (float)Main.worldSurface;
            if (surface == 0f) surface = 1f;
            float y = ((0f - (Main.screenPosition.Y - 300f)) / (surface * 16f));

            int pushBGTopHack = -180;

            opacity = Main.bgAlphaFarBackLayer[Type];
            Color color;

            if (Main.dayTime != true)
            {
                float v = GetV();

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                GetC(ref color, v * 10f, v * 5f);
                DrawBack(spriteBatch, 327, 0.2f, y, pushBGTopHack, color);

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                GetC(ref color, v * 5f, v);
                DrawBack(spriteBatch, 326, 0.6f, y, pushBGTopHack + 100, color);

                color = Main.ColorOfSurfaceBackgroundsModified * (1f + v);
                GetC(ref color, v, v);
                DrawBack(spriteBatch, 325, 1.0f, y, pushBGTopHack, color);
            }
            else
            {
                color = Main.ColorOfSurfaceBackgroundsModified;
                DrawBack(spriteBatch, 327, 0.2f, y, pushBGTopHack, color);
                DrawBack(spriteBatch, 326, 0.6f, y, pushBGTopHack + 100, color);
                DrawBack(spriteBatch, 325, 1.0f, y, pushBGTopHack, color);
            }
        }

        protected void GetC(ref Color color, float v1, float v2)
        {
            color.R = (byte)MathHelper.Clamp(color.R * (1f + v1), byte.MinValue, byte.MaxValue);
            color.G = (byte)MathHelper.Clamp(color.G * (1f - v2), byte.MinValue, byte.MaxValue);
            color.B = (byte)MathHelper.Clamp(color.B * (1f - v2), byte.MinValue, byte.MaxValue);
        }

        public override bool DrawSunPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color)
        {
            color = Color.Red;

            DrawPlanet(spriteBatch, 1f - GetV());

            return true;
        }

        public override void DrawSunPostfix(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale, Color color)
        {
            var _sunflare1Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare1");
            var _sunflare2Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare2");

            Color c = Color.White;
            c.A = 0;
            c *= opacity;

            float sunsetVisibility = 1;
            float sunriseVisibility = 1;

            spriteBatch.Draw(_sunflare1Texture.Value, position, null, c * sunsetVisibility * 0.75f, -rotation, _sunflare1Texture.Size() / 2f, 3f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare1Texture.Value, position, null, c * sunsetVisibility * 0.35f, -rotation, _sunflare1Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, c * sunriseVisibility * 0.7f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, c * sunriseVisibility * 0.3f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sunflare2Texture.Value, position, null, c * sunriseVisibility * 0.2f * 0.5f, rotation, _sunflare2Texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }

        public override bool DrawMoonPrefix(SpriteBatch spriteBatch, ref Vector2 position, ref float rotation, ref float scale, ref Color color)
        {
            color = Color.OrangeRed;

            GetPlanetPos(out Vector2 ppos);
            float widht = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Planet").Width() / 2f;
            float distance = position.Distance(ppos);
            distance -= widht;

            if (distance < 0f)
            {
                color.R = (byte)MathHelper.Clamp(color.R * (1f + (1f / widht * distance)), byte.MinValue, byte.MaxValue);
                color.G = (byte)MathHelper.Clamp(color.G * (1f + (1f / widht * distance)), byte.MinValue, byte.MaxValue);
                color.B = (byte)MathHelper.Clamp(color.B * (1f + (1f / widht * distance)), byte.MinValue, byte.MaxValue);
            }

            rotation += MathHelper.TwoPi / 4f;

            //

            float v = GetV();

            Color c = Color.White * 0.8f;
            c *= v;

            spriteBatch.Draw(Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Background").Value,
                new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null,
                c * opacity);

            //

            DrawPlanet(spriteBatch, 1f);

            return true;
        }

        protected void GetPlanetPos(out Vector2 pos)
        {
            pos = Main.ScreenSize.ToVector2() / 2f + new Vector2(400, -300);
            float off = GetV() * 10f;
            if (Main.dayTime != true) off = -off;

            pos.Y += off;
        }

        protected void DrawPlanet(SpriteBatch spriteBatch, float v)
        {
            Texture2D p = Main.Assets.Request<Texture2D>("Images/Misc/SolarSky/Planet").Value;

            GetPlanetPos(out Vector2 pos);

            spriteBatch.Draw(p, pos, null, Color.White * v * opacity, 1f, p.Size() / 2f, 1f, SpriteEffects.None, 0);
        }

        protected void DrawBack(SpriteBatch spriteBatch, int index, float v, float backgroundTopMagicNumber, int pushBGTopHack, Color color)
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
                        color * opacity, 0f, default, bgScale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
