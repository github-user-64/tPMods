using ExtenContent.Utils;
using Microsoft.Xna.Framework.Audio;
using Terraria;

namespace ExtenContent.Exten
{
    public static partial class SoundLoader
    {
        private static void ContentLoad()
        {
            if (Main.dedServ) return;

            foreach (ExtenSound i in sounds)
            {
                i.Sound = new MySoundStyle(i.Asset.Request<SoundEffect>(i.Path).Value, i.MaxTrackedInstances);
            }
        }
    }
}
