using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;

namespace BedWars.Edit.UI.Elements
{
    internal class UIImageButton : UIImage
    {
        public Action OnClick = null;
        public string MouseText = null;

        public UIImageButton(float size, string mouseText, string image) :
            base(Main.Assets.Request<Texture2D>(image, AssetRequestMode.ImmediateLoad))
        {
            Width.Pixels = Height.Pixels = size;
            ScaleToFit = true;
            MouseText = mouseText;

            OnMouseOver += (e, s) =>
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
                Color = Color.White;
            };

            Color = Color.White * 0.5f;
            OnMouseOut += (e, s) => Color = Color.White * 0.5f;

            OnLeftClick += (e, s) =>
            {
                OnClick?.Invoke();
                SoundEngine.PlaySound(SoundID.MenuTick);
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering && MouseText != null) Main.instance.MouseText(MouseText);
        }
    }
}
