using ExtenContent.Extens;
using tContentPatch;
using Terraria;
using Terraria.IO;

namespace ExtenContent.PatchGame
{
    internal class PPlayer : PatchPlayer
    {
        public override void ItemCheck_ShootPostfix(Player This, Item item, int weaponDamage, bool withAudioVisualFeedback)
        {
            ItemLoad.ItemCheck_ShootPostfix(This, item, weaponDamage, withAudioVisualFeedback);
        }

        public override void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            ItemLoad.LoadPlayerPostfix(result, playerPath, cloudSave);
        }

        public override void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            ItemLoad.SavePlayerPrefix(playerFile, skipMapSave);
        }
    }
}
