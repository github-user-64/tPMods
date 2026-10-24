using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Readers;
using System;
using System.IO;

namespace ExtenContent.Utils.AssetUtils.Readers
{
    /// <summary/>
    public static class CompositeAssetRead<FileType, AssetType>
    {
        /// <summary>
        /// <typeparamref name="FileType"/>类型的文件读取为<typeparamref name="AssetType"/>类型的资源
        /// </summary>
        public static Func<Stream, AssetType> Read = null;
    }

    /// <summary/>
    public class CompositeAssetRead<FileType> : IAssetReader
    {
        /// <inheritdoc/>
        public AssetType FromStream<AssetType>(Stream stream) where AssetType : class
        {
            if (CompositeAssetRead<FileType, AssetType>.Read == null)
            {
                throw new Exception($"没有读取方式:文件读取类型{typeof(FileType)},资源类型{typeof(AssetType)}");
            }

            return CompositeAssetRead<FileType, AssetType>.Read(stream);
        }
    }

    /// <summary>
    /// 读取:wav<br/>
    /// 资源类型:<see cref="SoundEffect"/>
    /// </summary>
    public class WAVReader : CompositeAssetRead<WAVReader>
    {
        static WAVReader()
        {
            CompositeAssetRead<WAVReader, SoundEffect>.Read = SoundEffect.FromStream;
        }
    }
}
