using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System;
using System.Linq;
using tContentPatch;
using tContentPatch.ModLoad;
using Terraria;
using Terraria.Audio;

namespace NewContents
{
    internal class ThisMod : Mod
    {
        public static ModObject mo { get; protected set; } = null;

        public ThisMod()
        {
            ModObject mo = ContentPatch.GetModObjects()?.FirstOrDefault(i => i.config.key == ExtenContent.ThisMod.ModKey);
            if (mo == null) throw new Exception($"找不到:{ExtenContent.ThisMod.ModName}");

            string targetVersion = "1.2";
            if (mo.config.version != targetVersion) throw new Exception($"NewContents的{ExtenContent.ThisMod.ModName}目标版本为:{targetVersion}");
        }

        public override void Load(ModObject mo)
        {
            ThisMod.mo = mo;

            RegistExten(ThisMod.mo);
        }

        protected static void RegistExten(ModObject mo)
        {
            ExtenManag.RegistMod(mo);
        }

        public class asd : EPatchPlayer
        {
            public static IAssetRepository a;
            public override void Load()
            {
                a = Asset;
            }
        }
        public class asd2 : PatchMain
        {
            public override void UpdatePostfix(GameTime gameTime)
            {
                if (Main.mouseRight)
                {
                    SoundEngine.PlayTrackedSound(new MySoundStyle("Assets/Sounds/Music1", asd.a, 3), Main.LocalPlayer.Center);
                }
            }
        }
    }
}
