using ModTool.Utils.GetDataEventArgs;
using System;
using System.Collections.Generic;

namespace ModTool.ServerHelp
{
    public static partial class PlayerCanAction
    {
        /// <summary/>
        public class HandlerList<T> where T : GetDataEventArgs
        {
            /// <summary/>
            public delegate bool Handler(T e);
            private readonly List<Handler> list = new List<Handler>();

            /// <summary/>
            public bool Invoke(T args, Action isFalse)
            {
                foreach (Handler i in list)
                {
                    if (i.Invoke(args) is true) continue;

                    isFalse?.Invoke();
                    return false;
                }

                return true;
            }
            /// <summary/>
            public static HandlerList<T> operator +(HandlerList<T> hand, Handler handler)
            {
                if (hand == null) hand = new HandlerList<T>();
                hand.list.Add(handler);
                return hand;
            }
            /// <summary/>
            public static HandlerList<T> operator -(HandlerList<T> hand, Handler handler)
            {
                hand.list.Remove(handler);
                return hand;
            }
        }

        private static bool Call<T>(this HandlerList<T> h, T args, Action isFalse) where T : GetDataEventArgs
        {
            return h?.Invoke(args, isFalse) ?? true;
        }

        /// <summary>
        /// 能否生成射弹, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<SyncProjectileEventArgs> OnCanNewProjectile = null;

        /// <summary>
        /// 能否创建物品(丢出,开宝藏袋), 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<SyncItemEventArgs> OnCanNewItem = null;

        /// <summary>
        /// 能否切换pvp, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<TogglePVPEventArgs> OnCanTogglePVP = null;

        /// <summary>
        /// 能否切换队伍, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<ToggleTeamEventArgs> OnCanToggleTeam = null;

        /// <summary>
        /// 能否控制和移位, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>启用服务端角色时有效<see cref="Utils.ServerSideCharacter(bool)"/>
        /// </summary>
        public static HandlerList<ControlsEventArgs> OnCanControls = null;

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
        public static HandlerList<SendTileSquareEventArgs> OnCanSendTileSquare = null;

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
        /// 能否打开箱子, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<RequestChestOpenEventArgs> OnCanRequestChestOpen = null;

        /// <summary>
        /// 能否快速堆叠到箱子, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<QuickStackChestsEventArgs> OnCanQuickStackChests = null;

        /// <summary>
        /// 能否设置Buffs, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>启用服务端角色时有效<see cref="Utils.ServerSideCharacter(bool)"/>
        /// </summary>
        public static HandlerList<PlayerBuffsEventArgs> OnCanPlayerBuffs = null;
    }
}
