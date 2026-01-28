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
            public bool Invoke(T args)
            {
                foreach (Handler i in list)
                {
                    if (i.Invoke(args) is true) continue;

                    return false;
                }

                return true;
            }
            /// <summary/>
            public static HandlerList<T> operator +(HandlerList<T> hand, Handler handler)
            {
                if (handler == null) return hand;
                if (hand == null) hand = new HandlerList<T>();
                hand.list.Add(handler);
                return hand;
            }
            /// <summary/>
            public static HandlerList<T> operator -(HandlerList<T> hand, Handler handler)
            {
                if (hand == null) return null;
                hand.list.Remove(handler);
                return hand;
            }
        }

        /// <summary/>
        public static bool Call<T>(this HandlerList<T> h, T args, Action ifFalse = null) where T : GetDataEventArgs
        {
            try
            {
                if (h == null) return true;
                if (h.Invoke(args) == true) return true;

                ifFalse?.Invoke();
            }
            catch { }

            return false;
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
        public static HandlerList<TeamChangeEventArgs> OnCanTeamChange = null;

        /// <summary>
        /// 能否控制和移位, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>启用服务端角色时有效<see cref="Utils.ServerSideCharacter(bool)"/>
        /// </summary>
        public static HandlerList<ControlsEventArgs> OnCanControls = null;

        /// <summary>
        /// 能否打开箱子, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>箱子物品类
        /// </summary>
        public static HandlerList<RequestChestOpenEventArgs> OnCanRequestChestOpen = null;

        /// <summary>
        /// 能否快速堆叠到箱子, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>箱子物品类
        /// </summary>
        public static HandlerList<QuickStackChestsEventArgs> OnCanQuickStackChests = null;

        /// <summary>
        /// 能否设置Buffs, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// <para/>启用服务端角色时有效<see cref="Utils.ServerSideCharacter(bool)"/>
        /// </summary>
        public static HandlerList<PlayerBuffsEventArgs> OnCanPlayerBuffs = null;

        /// <summary>
        /// 能否抓住动物, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<BugCatchingEventArgs> OnCanBugCatching = null;

        /// <summary>
        /// 能否释放动物, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<BugReleasingEventArgs> OnCanBugReleasing = null;

        /// <summary>
        /// 能否生成boss, 使用许可证, 开始事件, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<SpawnBossUseLicenseStartEventEventArgs> OnCanSpawnBossUseLicenseStartEvent = null;

        /// <summary>
        /// 能否传送(随机,魔法海螺,恶魔海螺,贝壳电话世界重生点), 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<RequestTeleportationByServerEventArgs> OnCanRequestTeleportationByServer = null;

        /// <summary>
        /// 能否传送实体, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<TeleportEntityEventArgs> OnCanTeleportEntity = null;

        /// <summary>
        /// 能否伤害npc, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<DamageNPCEventArgs> OnCanDamageNPC = null;

        /// <summary>
        /// 能否伤害玩家, 当有一个返回<see langword="false"/>则剩下的不会再执行
        /// </summary>
        public static HandlerList<PlayerHurtV2EventArgs> OnCanPlayerHurtV2 = null;
    }
}
