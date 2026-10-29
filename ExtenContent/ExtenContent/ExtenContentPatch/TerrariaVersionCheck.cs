using ExtenContent.Exten;
using ExtenContentPatch.PatchGame;
using Terraria;

namespace ExtenContentPatch
{
    /// <summary>
    /// 用于在游戏版本更新时<br/>
    /// 定位比较需要查看代码更改的地方<br/>
    /// <see cref="ItemLoader.SetDefaults(Item, int, Terraria.GameContent.Items.ItemVariant)"/>
    /// <see cref="ProjectileLoader.SetDefaultsPostfix(Projectile, int)"/>
    /// <see cref="MenuStyleLoader.DrawBGPrefix(Terraria.DataStructures.SpriteBatchBeginner)"/>
    /// <see cref="MenuStyleLoader.DrawLogo(bool, bool, float, float)"/>
    /// <see cref="MenuStyleLoader.DrawSunAndMoon(Main.SceneArea, Microsoft.Xna.Framework.Color, Microsoft.Xna.Framework.Color, float)"/>
    /// <see cref="PPlayer.ItemCheck_ManageRightClickFeaturesPostfix(Player)"/>
    /// <see cref="PPlayer.ItemCheck_ShootPrefix(Player, int, Item, int, bool)"/>
    /// <see cref="PMain.Transpiler_DrawMenu(System.Collections.Generic.IEnumerable{HarmonyLib.CodeInstruction})"/>
    /// </summary>
    internal enum TerrariaVersionCheck { }
}