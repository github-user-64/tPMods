using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using System;
using Terraria.Audio;

namespace ExtenContent.Utils
{
    /// <inheritdoc/>
    public class MySoundStyle : SoundStyle, IDisposable
    {
        /// <inheritdoc/>
        public override bool IsTrackable => true;
        /// <inheritdoc/>
        public override int MaxTrackedInstances => _MaxTrackedInstances;
        private readonly int _MaxTrackedInstances = 0;
        /// <summary/>
        public SoundEffect Sound = null;
        /// <summary/>
        public string Path = null;
        /// <summary/>
        public IAssetRepository Asset = null;

        /// <inheritdoc/>
        public MySoundStyle(SoundEffect se = null, int maxTrackedInstances = 1)
        {
            Sound = se;
            _MaxTrackedInstances = maxTrackedInstances;
        }

        /// <inheritdoc/>
        public MySoundStyle(string path, IAssetRepository asset, int maxTrackedInstances = 1) : this(null, maxTrackedInstances)
        {
            Path = path;
            Asset = asset;
        }

        /// <inheritdoc/>
        public override SoundEffect GetRandomSound()
        {
            if (Sound == null) Sound = Asset.Request<SoundEffect>(Path).Value;

            return Sound;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Asset = null;
            Path = null;
            Sound?.Dispose();
            Sound = null;
        }
    }
}
