using ExtenContentPatch.PatchGame;

namespace ExtenContentPatch
{
    /// <summary>
    /// 用于在游戏版本更新时<br/>
    /// 定位比较需要查看代码更改的地方<br/>
    /// <see cref="ItemLoader.SetDefaults(Terraria.Item, int, Terraria.GameContent.Items.ItemVariant)"/>
    /// <see cref="ProjectileLoader.SetDefaultsPostfix(Terraria.Projectile, int)"/>
    /// <see cref="PPlayer.ItemCheck_ManageRightClickFeaturesPostfix(Terraria.Player)"/>
    /// <see cref="PPlayer.ItemCheck_ShootPrefix(Terraria.Player, int, Terraria.Item, int, bool)"/>
    /// <see cref="MenuStyleLoader.DrawBGPrefix(Terraria.DataStructures.SpriteBatchBeginner)"/>
    /// </summary>
    internal enum TerrariaVersionCheck { }
}