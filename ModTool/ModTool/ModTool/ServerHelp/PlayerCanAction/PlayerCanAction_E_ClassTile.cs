using ModTool.Utils.GetDataEventArgs;

namespace ModTool.ServerHelp
{
    public static partial class PlayerCanAction
    {
        #region 方块类
        /// <summary>
        /// 能否操作方块, 挖炸放置方块基本都在这, 物品框武器架拿物品也是, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<TileManipulationEventArgs> OnCanTileManipulation = null;

        /// <summary>
        /// 能否放置对象, 因该是有功能的方块(开关,熔炉,椅子,旗帜), 大部分多图格的方块是靠这个放置的, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<PlaceObjectEventArgs> OnCanPlaceObject = null;

        /// <summary>
        /// 能否放置实体方块, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<TileEntityPlacementEventArgs> OnCanTileEntityPlacement = null;

        /// <summary>
        /// 能否修改图格数据, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<AreaTileChangeEventArgs> OnCanAreaTileChange = null;

        /// <summary>
        /// 能否放置破坏箱子, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<ChestUpdatesEventArgs> OnCanChestUpdates = null;

        /// <summary>
        /// 能否点击开关, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<HitSwitchEventArgs> OnCanHitSwitch = null;

        /// <summary>
        /// 能否在物品框放置物品, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<ItemFrameTryPlacingEventArgs> OnCanItemFrameTryPlacing = null;

        /// <summary>
        /// 能否在武器架放置物品, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<WeaponsRackTryPlacingEventArgs> OnCanWeaponsRackTryPlacing = null;

        /// <summary>
        /// 能否在食物盘子放置物品, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<FoodPlatterTryPlacingEventArgs> OnCanFoodPlatterTryPlacing = null;

        /// <summary>
        /// 能否放置和收起液体, 液体炸弹属于射弹不受这个影响, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<LiquidUpdateEventArgs> OnCanLiquidUpdate = null;

        /// <summary>
        /// 能否放置去除方块油漆, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<SyncTilePaintOrCoatingEventArgs> OnCanSyncTilePaintOrCoating = null;

        /// <summary>
        /// 能否放置去除墙油漆, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<SyncWallPaintOrCoatingEventArgs> OnCanSyncWallPaintOrCoating = null;

        /// <summary>
        /// 能否编辑告示牌, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<OpenSignResponseEventArgs> OnCanOpenSignResponse = null;

        /// <summary>
        /// 能否上锁开锁, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>方块类
        /// </summary>
        public static HandlerList<LockAndUnlockEventArgs> OnCanLockAndUnlock = null;
        #endregion

        /// <summary>
        /// 批量注册, 能否操作方块
        /// </summary>
        public static void RegisterClassOnTile(HandlerList<ClassTileEventArgs>.Handler action)
        {
            if (action == null) return;

            OnCanTileManipulation += e => action(e);
            OnCanPlaceObject += e => action(e);
            OnCanTileEntityPlacement += e => action(e);
            OnCanAreaTileChange += e => action(e);
            OnCanChestUpdates += e => action(e);
            OnCanHitSwitch += e => action(e);
            OnCanItemFrameTryPlacing += e => action(e);
            OnCanWeaponsRackTryPlacing += e => action(e);
            OnCanFoodPlatterTryPlacing += e => action(e);
            OnCanLiquidUpdate += e => action(e);
            OnCanSyncTilePaintOrCoating += e => action(e);
            OnCanSyncWallPaintOrCoating += e => action(e);
            OnCanOpenSignResponse += e => action(e);
            OnCanLockAndUnlock += e => action(e);
        }
    }
}
