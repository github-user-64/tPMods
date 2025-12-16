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
            private readonly List<Func<T, bool>> list = new List<Func<T, bool>>();

            /// <summary/>
            public bool Invoke(T args, Action isFalse)
            {
                foreach (Func<T, bool> i in list)
                {
                    if (i.Invoke(args) is true) continue;

                    isFalse?.Invoke();
                    return false;
                }

                return true;
            }
            /// <summary/>
            public static HandlerList<T> operator +(HandlerList<T> hand, Func<T, bool> handler)
            {
                if (hand == null) hand = new HandlerList<T>();
                hand.list.Add(handler);
                return hand;
            }
            /// <summary/>
            public static HandlerList<T> operator -(HandlerList<T> hand, Func<T, bool> handler)
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
        /// 能否切换pvp, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<TogglePVPEventArgs> OnCanTogglePVP = null;

        /// <summary>
        /// 能否切换队伍, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<ToggleTeamEventArgs> OnCanToggleTeam = null;

        /// <summary>
        /// 能否控制和移位, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<ControlsEventArgs> OnCanControls = null;

        /// <summary>
        /// 能否操作方块, 挖炸放置方块基本都在这, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<TileManipulationEventArgs> OnCanTileManipulation = null;

        /// <summary>
        /// 能否放置对象, 因该是有功能的方块(开关,熔炉,椅子,等,旗帜), 大部分多图格的方块是靠这个放置的, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<PlaceObjectEventArgs> OnCanPlaceObject = null;

        /// <summary>
        /// 能否放置实体方块, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<TileEntityPlacementEventArgs> OnCanTileEntityPlacement = null;

        /// <summary>
        /// 能否修改图格数据, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<SendTileSquareEventArgs> OnCanSendTileSquare = null;

        /// <summary>
        /// 能否在物品框放置物品, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<ItemFrameTryPlacingEventArgs> OnCanItemFrameTryPlacing = null;
    }
}
