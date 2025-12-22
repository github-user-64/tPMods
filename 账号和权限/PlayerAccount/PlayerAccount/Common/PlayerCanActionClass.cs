using ModTool.ServerHelp;
using ModTool.Utils.GetDataEventArgs;
using tContentPatch;

namespace PlayerAccount.Common
{
    /// <summary>
    /// 玩家能否交互
    /// </summary>
    public class PlayerCanActionClass : Mod
    {
        /// <summary>
        /// 可以操作方块类
        /// </summary>
        public static PlayerCanAction.HandlerList<ClassTileEventArgs> OnCanTile = null;

        /// <inheritdoc/>
        public override void Load()
        {
            PlayerCanAction.OnCanTileManipulation += CanTile;
            PlayerCanAction.OnCanPlaceObject += CanTile;
            PlayerCanAction.OnCanTileEntityPlacement += CanTile;
            PlayerCanAction.OnCanSendTileSquare += CanTile;
            PlayerCanAction.OnCanChestUpdates += CanTile;
            PlayerCanAction.OnCanHitSwitch += CanTile;
            PlayerCanAction.OnCanItemFrameTryPlacing += CanTile;
            PlayerCanAction.OnCanWeaponsRackTryPlacing += CanTile;
            PlayerCanAction.OnCanFoodPlatterTryPlacing += CanTile;
            PlayerCanAction.OnCanLiquidUpdate += CanTile;
            PlayerCanAction.OnCanPaintTile += CanTile;
            PlayerCanAction.OnCanPaintWall += CanTile;
            PlayerCanAction.OnCanEditSign += CanTile;
            PlayerCanAction.OnCanLockAndUnlock += CanTile;
        }

        private static bool CanTile(ClassTileEventArgs args)
        {
            return OnCanTile.Call(args);
        }
    }
}
