using ExtenContent.Extens;

namespace ExtenContent.Utils
{
    /// <summary/>
    public static class TextureUtils
    {
        /// <summary/>
        public interface ITextureUtils : IExtenType { }

        /// <summary>
        /// 如果<paramref name="i"/>是MyMod.Content.Items.A1<br/>
        /// 那么返回"Content.Items.A1"
        /// </summary>
        public static string GetTexture(this ITextureUtils i)
        {
            char c = '/';

            string texture = i.FullName.Replace('.', c);

            int index = texture.IndexOf(c);
            texture = texture.Substring(index + 1);

            return texture;
        }
    }
}
