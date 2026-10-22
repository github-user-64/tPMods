using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using Terraria.Audio;

namespace ExtenContent.Utils
{
    /// <inheritdoc/>
    public class MySoundStyle : SoundStyle
    {
        /// <inheritdoc/>
        public override bool IsTrackable => true;
        /// <inheritdoc/>
        public override int MaxTrackedInstances => _MaxTrackedInstances;
        /// <inheritdoc/>
        public SoundEffect se = null;
        private readonly int _MaxTrackedInstances = 0;

        /// <inheritdoc/>
        public MySoundStyle(SoundEffect se, int maxTrackedInstances = 1)
        {
            this.se = se;
            _MaxTrackedInstances = maxTrackedInstances;
        }

        /// <inheritdoc/>
        public MySoundStyle(string path, IAssetRepository asset, int maxTrackedInstances = 1) : this(
            asset.Request<SoundEffect>(path).Value, maxTrackedInstances)
        { }

        /// <inheritdoc/>
        public override SoundEffect GetRandomSound() => se;
    }
}
